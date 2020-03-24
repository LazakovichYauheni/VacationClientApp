using FlexiMvvm.ViewModels;
using vacation.core.ViewModels;

namespace vacation.core.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin(SplashViewModel from);
        void NavigateToMainList(SplashViewModel from);
        void NavigateToMainList(LoginViewModel from);
        void NavigateToDetailsScreen(ILifecycleViewModel from, VacationParameters parameters);
        void NavigateBack(DetailScreenViewModel from);
    }
}
