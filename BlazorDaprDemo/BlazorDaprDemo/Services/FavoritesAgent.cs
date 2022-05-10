using BlazorDaprDemo.Const;
using FavoritesAPI.Models;

namespace BlazorDaprDemo.Services
{
    public class FavoritesAgent
    {
        public FavoritesAgent(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(ApplicationConsts.FavoritesApiDaprAppId);
        }

        public HttpClient httpClient;

        public async Task<List<Favorite>?> GetFavorites(string user)
        {
            return await httpClient.GetFromJsonAsync<List<Favorite>>($"favorites/{user}");
        }

        public async Task AddFavorite(string user, int vacationid)
        {
            await httpClient.PostAsync($"favorites/{user}/add/{vacationid}", null);
        }

        public async Task RemoveFavorite(string user, int vacationid)
        {
            await httpClient.PostAsync($"favorites/{user}/remove/{vacationid}", null);
        }        
    }
}
