using BlazorDaprDemo.Entities;
using System.Text.Json;

namespace BlazorDaprDemo.Services
{
    public class VacationTyeAgent: IVacationAgent
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;

        public VacationTyeAgent(HttpClient client)
        {
            this.client = client;
        }

        public async Task<VacationModel[]?> GetVacationsAsync()
        {
            var responseMessage = await this.client.GetAsync("/vacations");
            if (responseMessage.IsSuccessStatusCode)
            {
                var stream = await responseMessage.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<VacationModel[]>(stream, options);
            }

            throw new Exception("Vacations could not be retrieved");
        }
    }
}
