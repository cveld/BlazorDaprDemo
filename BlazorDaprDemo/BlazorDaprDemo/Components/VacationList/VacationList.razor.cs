using BlazorInfrastructure;
using BlazorInfrastructure.CrossCircuitCommunication;
using BlazorServerSide.Data;
using BlazorServerSide.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDaprDemo.Components.VacationList
{
    public partial class VacationList
    {
        const string FAVORITECLICKEDEVENTID = "FavoriteClicked";
        const string BOOKEDEVENTID = "Booked";

        protected Dictionary<int, VacationModel> vacations;

        [Inject]
        public CrossCircuitCommunication crossCircuitCommunication { get; set; }

        [Inject]
        public VacationContext context { get; set; }

        [CascadingParameter]
        protected string CurrentUser { get; set; }
      
        List<(HashSet<Action<MessagePayload>>, Action<MessagePayload>)> subscriptions = new List<(HashSet<Action<MessagePayload>>, Action<MessagePayload>)>();

        protected override void OnInitialized()
        {            
            vacations = new Dictionary<int, VacationModel>();
            foreach (var vacation in context.Vacations)
            {
                vacations.Add(vacation.ID, vacation);         

                // Register FavoriteClicked event handler                
                RegisterCallback(FAVORITECLICKEDEVENTID, vacation.ID, (payload) => FavoriteClickedEventHandler((FavoriteClickedEventModel)payload.Message, RemoteTrigger: true));

                // Register Booked event handler                               
                RegisterCallback(BOOKEDEVENTID, vacation.ID, (payload) => BookedEventHandler((BookedEventModel)payload.Message, RemoteTrigger: true));
            }
            CurrentUser = "dummy";
        }

        void RegisterCallback(string eventID, int VacationID, Action<MessagePayload> callback)
        {            
            var hashset = crossCircuitCommunication.GetCallbacksHashSet(eventID, VacationID);
            hashset.Add(callback);
            subscriptions.Add((hashset, callback));
        }

        protected bool VacationLiked(VacationModel vacation)
        {
            var user = vacation.Likes?.Where(u => u.Name == CurrentUser).FirstOrDefault();
            return user != null;
        }

        async protected void OnFavoriteClicked(VacationModel vacationModel)
        {
            var message = new FavoriteClickedEventModel
            {
                VacationId = vacationModel.ID,
                Liked = !VacationLiked(vacationModel),
                User = CurrentUser
            };
            
            FavoriteClickedEventHandler(message, false);
            await crossCircuitCommunication.Dispatch(FAVORITECLICKEDEVENTID, vacationModel.ID, message);
        }

        void FavoriteClickedEventHandler(FavoriteClickedEventModel favoriteClickedModel, bool RemoteTrigger)
        {
            var vacation = vacations[favoriteClickedModel.VacationId];
            var currentstate = VacationLiked(vacation);
            var desiredstate = favoriteClickedModel.Liked;
            if (currentstate != desiredstate)
            {
                var user = vacation.Likes?.Where(u => u.Name == favoriteClickedModel.User).FirstOrDefault();

                if (!desiredstate)
                {
                    vacation.Likes.Remove(user);
                }
                else
                {
                    if (vacation.Likes == null)
                    {
                        vacation.Likes = new HashSet<User>();
                    }
                    vacation.Likes.Add(new User
                    {
                        Name = CurrentUser
                    });
                }

                if (RemoteTrigger)
                {
                    // Message is coming from external source; Blazor state needs to get a kick 
                    base.InvokeAsync(StateHasChanged);
                }
                else
                {
                    // Method is directly called due to the user's action, persist the change:
                    context.SaveChanges();
                }
            }
        }

        public async Task OnBookedAsync(object obj)
        {
            BookedEventModel bookedEvent = obj as BookedEventModel;
            BookedEventHandler(bookedEvent, false);
            await crossCircuitCommunication.Dispatch(BOOKEDEVENTID, bookedEvent.VacationID, bookedEvent);
        }

        public void BookedEventHandler(BookedEventModel bookedEvent, bool RemoteTrigger)
        {
            var vacation = vacations[bookedEvent.VacationID];
            var currentstate = VacationBooked(vacation);
            var desiredstate = true;

            if (currentstate != desiredstate)
            {
                vacation.Booked = desiredstate;
                if (RemoteTrigger)
                {
                    // Message is coming from external source; Blazor state needs to get a kick 
                    base.InvokeAsync(StateHasChanged);
                }
                else
                {
                    // Method is directly called due to the user's action, persist the change:
                    // commented out as there is currently no undo booking UI
                    // context.SaveChanges();
                }
            }
        }

        private bool VacationBooked(VacationModel vacation)
        {
            return vacation.Booked;
        }

        public void Dispose()
        {
            foreach ((var hashSet, var action) in subscriptions)
            {
                hashSet.Remove(action);
            }
        }
    }
}
