using System.Collections.Generic;
using System.Threading.Tasks;
using Vacations.Models.Models;

namespace Services.Contracts
{
    public interface IVacationProvider
    {
       
        Task UpdateVacationsAsync(VacationModel vacUpd);
        Task DeleteVacationsAsync(string vacationId);
        Task<List<VacationModel>> GetVacationsAsync();
    }
}
