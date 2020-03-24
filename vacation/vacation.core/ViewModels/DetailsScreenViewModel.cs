using FlexiMvvm.Commands;
using FlexiMvvm.ViewModels;
using Newtonsoft.Json;
using Services.Contracts;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using vacation.core.Contracts;
using vacation.core.Messages.Models;
using vacation.core.Navigation;
using Vacations.Models.Models;

namespace vacation.core.ViewModels
{
    public class DetailScreenViewModel : LifecycleViewModel<VacationParameters>
    {
        private readonly INavigationService _navigationService;
        private readonly IVacationProvider _vacationProvider;
        private readonly IMessageRoot _messageRoot;
        private VacationModel _vacationData;

        public VacationModel VacationData
        {
            get => _vacationData;
            set => SetValue(ref _vacationData, value);
        }

        public DetailScreenViewModel(INavigationService navigationService, IVacationProvider vacationProvider, IMessageRoot messageRoot)
        {
            _navigationService = navigationService;
            _vacationProvider = vacationProvider;
            _messageRoot = messageRoot;
        }

        public ObservableCollection<VacationStatusModel> Statuses { get; } = new ObservableCollection<VacationStatusModel>();
        public Command SaveCommand => CommandProvider.GetForAsync(() => SaveVacationsAsync(VacationData));
        public Command<VacationStatusModel> SelectStatusCommand => CommandProvider.Get<VacationStatusModel>(SelectStatus);

        public override void Initialize(VacationParameters parameters, bool recreated)
        {
            base.Initialize(parameters, recreated);

            Statuses.Add(new VacationStatusModel
            {
                Title = VacationStatuses.Approved.ToString()
            });
            Statuses.Add(new VacationStatusModel
            {
                Title = VacationStatuses.Closed.ToString()
            });

            var param = JsonConvert.DeserializeObject<VacationModel>(parameters.Model);
            VacationData = param ?? new VacationModel();

            Statuses.FirstOrDefault(st => st.Title == VacationData.VacationStatusTitle).IsSelected = true;

            VacationData.CurrentImageOnPage = param?.VacationType - 1 ?? 0;
        }

        private async Task SaveVacationsAsync(VacationModel vacationModel)
        {
            await _vacationProvider.UpdateVacationsAsync(vacationModel);
            _messageRoot.Raise(new EmptyMessage());
            _navigationService.NavigateBack(this);
        }

        private void SelectStatus(VacationStatusModel item)
        {
            foreach (var status in Statuses)
            {
                status.IsSelected = false;
            }

            item.IsSelected = true;

            if (item.Title == "Approved")
            {
                _vacationData.VacationStatus = 2;
            }
            else
            {
                _vacationData.VacationStatus = 4;
            }
        }
    }
}
