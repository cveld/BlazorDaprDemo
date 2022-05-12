using BlazorDaprDemo.Const;
using BlazorDaprDemo.Entities;
using Dapr.Client;
using System.Text.Json;

namespace BlazorDaprDemo.Services
{
    public class VacationDaprAgent: IVacationAgent
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly DaprClient client;
        private readonly HttpClient httpClient;

        public VacationDaprAgent(DaprClient client, IHttpClientFactory httpClientFactory)
        {
            this.client = client;
            this.httpClient = httpClientFactory.CreateClient(ApplicationConsts.VacationApiDaprAppId);
        }

        public async Task<VacationModel[]?> GetVacationsAsync()
        {
            //var result = await this.client.InvokeMethodAsync<VacationModel[]>(HttpMethod.Get, "vacationapi", "vacations");
            var result = await this.httpClient.GetFromJsonAsync<VacationModel[]>("vacations");
            return result;
        }
    }
}
