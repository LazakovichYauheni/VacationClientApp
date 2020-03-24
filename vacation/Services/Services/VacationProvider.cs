using Services.Contracts;
using Services.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacations.Models.Models;

namespace Services.Services
{
    public class VacationProvider : IVacationProvider
    {
        private readonly IVacationClientService _vacationClientService;

        private List<VacationModel> _vacationsList = new List<VacationModel>();

        public VacationProvider(IVacationClientService vacationClientService)
        {
            _vacationClientService = vacationClientService;
        }

        public async Task<List<VacationModel>> GetVacationsAsync()
        {
            if (!_vacationsList.Any())
            {
                await InitVacationsAsync();
            }

            return _vacationsList;
        }

        public async Task UpdateVacationsAsync(VacationModel vacationModel)
        {
            var item = _vacationsList.FirstOrDefault(v => v.Id == vacationModel.Id);

            if (item != null)
            {
                _vacationsList.Remove(item);
                _vacationsList.Add(vacationModel);
                await _vacationClientService.UpdateVacationsAsync(vacationModel);
                _vacationsList.Clear();
                await InitVacationsAsync();
            }
            else
            {
                _vacationsList.Add(vacationModel);

                await _vacationClientService.CreateVacationsAsync(vacationModel);
            }
        }

        public async Task DeleteVacationsAsync(string vacationId)
        {
            var item = _vacationsList.FirstOrDefault(v => v.Id == vacationId);

            if (item != null)
            {
                _vacationsList.Remove(item);
                await _vacationClientService.DeleteVacationsAsync(vacationId);
            }
        }

        private async Task InitVacationsAsync()
        {
            var vacations = await _vacationClientService.GetVacationsAsync();

            if (vacations != null)
            {
                foreach (var vac in vacations)
                {
                    _vacationsList.Add(GetVacationModel(vac));
                }
            }
        }

        private VacationModel GetVacationModel(VacationDataModel vacationDataModel)
        {
            return new VacationModel
            {
                Id = vacationDataModel.Id,
                StartDate = vacationDataModel.Start,
                EndDate = vacationDataModel.End,
                VacationType = vacationDataModel.VacationType,
                VacationStatus = vacationDataModel.VacationStatus,
                Created = vacationDataModel.Created,
                CreatedBy = vacationDataModel.CreatedBy
            };
        }
    }
}
