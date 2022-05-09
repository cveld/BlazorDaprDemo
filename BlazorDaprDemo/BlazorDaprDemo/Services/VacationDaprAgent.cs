using Dapr.Client;
using System.Text.Json;
using VacationModels;

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

        public VacationDaprAgent(DaprClient client)
        {
            this.client = client;
        }

        public async Task<Vacation[]?> GetVacationsAsync()
        {
            var result = await this.client.InvokeMethodAsync<Vacation[]>(HttpMethod.Get, "vacationapi", "vacations");
            return result;
        }
    }
}
