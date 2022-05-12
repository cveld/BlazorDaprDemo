using Dapr;
using Dapr.Client;
using FavoritesAPI.Const;
using FavoritesAPI.Models;
using System.Linq;

namespace FavoritesAPI
{
    public class FavoritesDaprAgent
    {
        private readonly DaprClient daprClient;

        public string StoreKey(string user)
        {
            return $"favorites:{user}";
        }

        public FavoritesDaprAgent(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }

        public async Task<StateEntry<List<Favorite>>> GetFavorites(string user)
        {
            var result = await daprClient.GetStateEntryAsync<List<Favorite>>(ApplicationConsts.StoreName, StoreKey(user));            
            result.Value ??= new List<Favorite>();
            return result;
        }

        public async Task AddFavorite(string user, int vacationid)
        {
            var state = await GetFavorites(user);
            if (state.Value.Any(x => x.VacationId == vacationid)) {
                return;
            }
            state.Value.Add(new Favorite { VacationId = vacationid });
            await state.SaveAsync();
        }

        public async Task RemoveFavorite(string user, int vacationid)
        {
            var state = await GetFavorites(user);
            var first = state.Value.FirstOrDefault(x => x.VacationId == vacationid);
            if (first != null)
            {
                state.Value.Remove(first);
                await state.SaveAsync();
            }
            return;            
        }

        public async Task ClearFavorites(string user)
        {
            await daprClient.DeleteStateAsync(ApplicationConsts.StoreName, StoreKey(user));

        }

    }
}
