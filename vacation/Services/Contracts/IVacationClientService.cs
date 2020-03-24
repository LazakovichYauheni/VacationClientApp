using Services.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vacations.Models.Models;

namespace Services.Contracts
{
    public interface IVacationClientService
    {
        Task<bool> TrySetCredentialsAsync(string login, string password);
        Task<List<VacationDataModel>> GetVacationsAsync();
        Task CreateVacationsAsync(VacationModel vacationModel);
        Task UpdateVacationsAsync(VacationModel vacations);
        Task DeleteVacationsAsync(string vacation);
    }
}
