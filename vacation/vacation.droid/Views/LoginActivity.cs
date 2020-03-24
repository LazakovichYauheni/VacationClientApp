using Android.App;
using Android.OS;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using vacation.core.ViewModels;
using vacation.droid.Converters;

namespace vacation.droid.Activities
{
    [Activity(Label = "LoginActivity", Theme = "@/style/AppTheme.NoActionBar", MainLauncher = false)]
    public class LoginActivity : BindableAppCompatActivity<LoginViewModel>
    {
        private EditText _login;
        private EditText _password;
        private Button _signIn;
        private TextView _textView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_layout);

            _login = FindViewById<EditText>(Resource.Id.userName);
            _password = FindViewById<EditText>(Resource.Id.password);
            _signIn = FindViewById<Button>(Resource.Id.button);
            _textView = FindViewById<TextView>(Resource.Id.errorList);
        }

        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(_login).For(v => v.TextAndTextChangedBinding()).To(vm => vm.Login);
            bindingSet.Bind(_password).For(v => v.TextAndTextChangedBinding()).To(vm => vm.Password);
            bindingSet.Bind(_signIn).For(v => v.ClickBinding()).To(vm => vm.SignInCommand);
            bindingSet.Bind(_textView).For(v => v.VisibilityBinding()).To(vm => vm.IsAuthorizationFailed).WithConversion<BoolToVisibilityValueConverter>();
        }
    }
}