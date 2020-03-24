using FlexiMvvm.Commands;
using FlexiMvvm.ViewModels;
using Services.Contracts;
using System.Threading.Tasks;
using vacation.core.Navigation;

namespace vacation.core.ViewModels
{
    public class LoginViewModel : LifecycleViewModel
    {
        private readonly IVacationClientService _vacationClientService;
        private readonly INavigationService _navigationService;
        private string _password = string.Empty;
        private string _login = string.Empty;
        private bool _textView;

        public string Password
        {
            get => _password;
            set => SetValue(ref _password, value);
        }

        public string Login
        {
            get => _login;
            set => SetValue(ref _login, value);
        }

        public bool IsAuthorizationFailed
        {
            get => _textView;
            set => SetValue(ref _textView, value);
        }

        public LoginViewModel(IVacationClientService vacationClientService, INavigationService navigationService)
        {
            _vacationClientService = vacationClientService;
            _navigationService = navigationService;
        }

        public Command SignInCommand => CommandProvider.GetForAsync(() => SignInAsync());

        private async Task<bool> SignInAsync()
        {
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && await _vacationClientService.TrySetCredentialsAsync(Login, Password))
            {
                _navigationService.NavigateToMainList(this);
            }
            else
            {
                IsAuthorizationFailed = true;
            }

            return true;
        }
    }
}
