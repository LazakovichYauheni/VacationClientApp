using FlexiMvvm.Commands;
using FlexiMvvm.ViewModels;
using Newtonsoft.Json;
using Services.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using vacation.core.Contracts;
using vacation.core.Messages.Models;
using vacation.core.Navigation;
using Vacations.Models.Models;

namespace vacation.core.ViewModels
{
    public class MainListViewModel : LifecycleViewModel, IMessageHandler<EmptyMessage>
    {
        private readonly IVacationProvider _vacationProvider;
        private readonly IMessageRoot _messageRoot;
        private readonly INavigationService _navigationService;

        public ObservableCollection<VacationModel> VacationsList { get; } = new ObservableCollection<VacationModel>();

        public MainListViewModel(IVacationProvider vacationProvider, INavigationService navigationService, IMessageRoot messageRoot)
        {
            _navigationService = navigationService;
            _vacationProvider = vacationProvider;
            _messageRoot = messageRoot;
            _messageRoot.Subscribe(this);
            InitDataAsync();
        }

        public Command<VacationModel> ItemSelectedCommand => CommandProvider.Get<VacationModel>(ItemSelected);

        public Command CreateVacationCommand => CommandProvider.Get(CreateVacation);

        public Command SortByAll => CommandProvider.Get(RaiseVacationList);

        public Command SortByApproved => CommandProvider.GetForAsync(SortByApprovedVacationsAsync);

        public Command SortByClosed => CommandProvider.GetForAsync(SortByClosedVacationsAsync);

        public Command<int> DeleteItemCommand => CommandProvider.GetForAsync<int>(DeleteItemAsync);

        public void CreateVacation()
        {
            var model = new VacationModel
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Created = DateTime.Now,
                CreatedBy = "SomeOne",
                VacationType = 1,
                VacationStatus = 2
            };
            _navigationService.NavigateToDetailsScreen(this, new VacationParameters(JsonConvert.SerializeObject(model)));
        }

        public void ItemSelected(VacationModel item)
        {
            _navigationService.NavigateToDetailsScreen(this, new VacationParameters(JsonConvert.SerializeObject(item)));
        }

        public void Handle(EmptyMessage message)
        {
            RaiseVacationList();
        }

        public async Task SortByApprovedVacationsAsync()
        {
            await SortByVacationStatusAsync((int)VacationStatuses.Approved);
        }

        public async Task SortByClosedVacationsAsync()
        {
            await SortByVacationStatusAsync((int)VacationStatuses.Closed);
        }

        public async Task DeleteItemAsync(int id)
        {
            var deletedItemId = VacationsList.ElementAt(id - 1).Id;
            await _vacationProvider.DeleteVacationsAsync(deletedItemId);
            RaiseVacationList();
        }

        private async void InitDataAsync()
        {
            foreach (var vacation in await _vacationProvider.GetVacationsAsync())
            {
                VacationsList.Add(vacation);
            }
        }
        private async Task SortByVacationStatusAsync(int vacationStatus)
        {
            VacationsList.Clear();

            var vacations = await _vacationProvider.GetVacationsAsync();

            foreach (var vacation in vacations.Where(x => x.VacationStatus == vacationStatus))
            {
                VacationsList.Add(vacation);
            }
        }
        private void RaiseVacationList()
        {
            VacationsList.Clear();

            InitDataAsync();
        }
    }
}
