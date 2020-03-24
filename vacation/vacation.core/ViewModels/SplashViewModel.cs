using FlexiMvvm.ViewModels;
using Services.Contracts;
using System.Threading.Tasks;
using vacation.core.Navigation;
using Xamarin.Essentials;

namespace vacation.core.ViewModels
{
    public class SplashViewModel : LifecycleViewModel
    {
        private INavigationService _navigationService;
        private IVacationClientService _vacationClientService;

        public SplashViewModel(INavigationService navigationService, IVacationClientService vacationClientService)
        {
            _navigationService = navigationService;
            _vacationClientService = vacationClientService;
        }

        public override async void Initialize(bool recreated)
        {
            base.Initialize(recreated);

            if (!await IsUserAuthorizedAsync())
            {
                _navigationService.NavigateToLogin(this);
            }
            else
            {
                _navigationService.NavigateToMainList(this);
            }
        }

        private async Task<bool> IsUserAuthorizedAsync()
        {
            return !string.IsNullOrEmpty(await SecureStorage.GetAsync("Login")) && !string.IsNullOrEmpty(await SecureStorage.GetAsync("Password")) &&
                await _vacationClientService.TrySetCredentialsAsync(await SecureStorage.GetAsync("Login"), await SecureStorage.GetAsync("Password"));
        }
    }
}
