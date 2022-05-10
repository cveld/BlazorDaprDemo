using BlazorDaprDemo.Services;
using FavoritesAPI.Models;

namespace BlazorDaprDemo.State
{
    public class FavoritesState
    {
        public FavoritesState(FavoritesAgent favoritesAgent)
        {
            this.favoritesAgent = favoritesAgent;            
        }

        public Dictionary<int, Favorite> favorites = new Dictionary<int, Favorite>();
        private readonly FavoritesAgent favoritesAgent;

        public async Task GetFavorites(string user)
        {
            var list = await favoritesAgent.GetFavorites(user);
            favorites = list.ToDictionary(keySelector: m => m.VacationId, elementSelector: m => m) ?? new Dictionary<int, Favorite>();
            NotifyStateChanged();            
        }



        public async Task AddFavorite(string user, int vacationid)
        {
            await favoritesAgent.AddFavorite(user, vacationid);
            if (favorites.GetValueOrDefault(vacationid) == null)
            {
                favorites.Add(vacationid, new Favorite { VacationId = vacationid });
            }
            NotifyStateChanged();
        }

        public async Task RemoveFavorite(string user, int vacationid)
        {
            await favoritesAgent.RemoveFavorite(user, vacationid);
            if (favorites.GetValueOrDefault(vacationid) != null)
            {
                favorites.Remove(vacationid);
            }
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
