using Android.App;
using Android.OS;
using FlexiMvvm.Ioc;
using FlexiMvvm.ViewModels;
using FlexiMvvm.Views;
using Services.Contracts;
using Services.Services;
using vacation.Droid.Navigation;
using vacation.core.Contracts;
using vacation.core.Messages;
using vacation.core.Navigation;
using vacation.core.ViewModels;

namespace vacation.droid.Activities
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : AppCompatActivity<SplashViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            InitApp();
            base.OnCreate(savedInstanceState);
        }

        private void InitApp()
        {
            var container = new SimpleIoc();

            container.Register<INavigationService>(() => new AppNavigationService());
            container.Register<IVacationClientService>(() => new VacationClientService(),Reuse.Singleton);
            container.Register<IMessageRoot>(() => new MessageRoot(),Reuse.Singleton);
            container.Register<IVacationProvider>(() => new VacationProvider(container.Get<IVacationClientService>()),Reuse.Singleton);

            container.Register(() => new SplashViewModel(container.Get<INavigationService>(), container.Get<IVacationClientService>()));
            container.Register(() => new LoginViewModel(container.Get<IVacationClientService>(), container.Get<INavigationService>()));
            container.Register(() => new MainListViewModel(container.Get<IVacationProvider>(), container.Get<INavigationService>(), container.Get<IMessageRoot>()));
            container.Register(() => new DetailScreenViewModel(container.Get<INavigationService>(), container.Get<IVacationProvider>(), container.Get<IMessageRoot>()));

            LifecycleViewModelProvider.SetFactory(
                new DefaultLifecycleViewModelFactory(container));
        }
    }
}