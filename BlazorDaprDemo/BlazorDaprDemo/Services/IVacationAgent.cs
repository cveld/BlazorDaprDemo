using VacationModels;

namespace BlazorDaprDemo.Services
{
    public interface IVacationAgent
    {
        public Task<Vacation[]?> GetVacationsAsync();
    }
}
