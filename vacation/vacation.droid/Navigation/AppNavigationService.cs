using Android.Content;
using FlexiMvvm.Navigation;
using FlexiMvvm.ViewModels;
using vacation.core.Navigation;
using vacation.core.ViewModels;
using vacation.droid.Activities;

namespace vacation.Droid.Navigation
{
    public class AppNavigationService : NavigationService, INavigationService
    {
        public void NavigateToLogin(SplashViewModel from)
        {
            var splashActivity = NavigationViewProvider.GetActivity<SplashScreenActivity, SplashViewModel>(from);

            var intent = new Intent(splashActivity, typeof(LoginActivity));
            intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.ClearTop | ActivityFlags.NewTask);
            splashActivity.StartActivity(intent);
        }
        public void NavigateToMainList(SplashViewModel from)
        {
            var splashActivity = NavigationViewProvider.GetActivity<SplashScreenActivity, SplashViewModel>(from);

            var intent = new Intent(splashActivity, typeof(MainListActivity));
            intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.ClearTop | ActivityFlags.NewTask);
            splashActivity.StartActivity(intent);
        }

        public void NavigateToMainList(LoginViewModel from)
        {
            var loginActivity = NavigationViewProvider.GetActivity<LoginActivity, LoginViewModel>(from);

            var intent = new Intent(loginActivity, typeof(MainListActivity));
            intent.AddFlags(ActivityFlags.ClearTask | ActivityFlags.ClearTop | ActivityFlags.NewTask);
            loginActivity.StartActivity(intent);
        }

        public void NavigateToDetailsScreen(ILifecycleViewModel from, VacationParameters parameters)
        {
            var view = NavigationViewProvider.Get(from);
            Navigate<DetailsScreenActivity, VacationParameters>(view, parameters);
        }

        public void NavigateBack(DetailScreenViewModel from)
        {
            var view = NavigationViewProvider.Get(from);
            NavigateBack(view);
        }
    }
}