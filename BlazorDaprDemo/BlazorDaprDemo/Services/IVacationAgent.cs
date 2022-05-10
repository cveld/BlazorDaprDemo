using BlazorDaprDemo.Entities;

namespace BlazorDaprDemo.Services
{
    public interface IVacationAgent
    {
        public Task<VacationModel[]?> GetVacationsAsync();
    }
}
