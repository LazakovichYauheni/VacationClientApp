using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using System;
using vacation.core.ViewModels;
using vacation.droid.Adapters;
using vacation.droid.CustomBinding;
using static Android.App.DatePickerDialog;

namespace vacation.droid.Activities
{
    [Activity(Label = "DetailsScreenActivity", Theme = "@/style/AppTheme.NoActionBar", MainLauncher = false)]
    public class DetailsScreenActivity : BindableAppCompatActivity<DetailScreenViewModel, VacationParameters>, IOnDateSetListener
    {
        private const string StartDatePicker = "startDatePicker";
        private const string EndDatePicker = "endDatePicker";
        private TextView _startVac;
        private TextView _startDateMonth;
        private TextView _startDateYear;
        private TextView _endVac;
        private TextView _endDateMonth;
        private TextView _endDateYear;
        private RecyclerView _recyclerView;
        private RadioButtonAdapter _adapter;
        private LinearLayoutManager _linearLayoutManager;
        private DatePickerDialog _startDatePickerDialog;
        private DatePickerDialog _endDatePickerDialog;
        private Button _saveButton;
        private ViewPager _viewPager;

        public event EventHandler StartDateTimeChanged;
        public event EventHandler EndDateTimeChanged;

        public DateTime StartDatePickerTime { get; set; }
        public DateTime EndDatePickerTime { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.detail_layout);

            _startDatePickerDialog = new DatePickerDialog(this);
            _startDatePickerDialog.DatePicker.Tag = StartDatePicker;
            _startDatePickerDialog.SetOnDateSetListener(this);

            _endDatePickerDialog = new DatePickerDialog(this);
            _endDatePickerDialog.DatePicker.Tag = EndDatePicker;
            _endDatePickerDialog.SetOnDateSetListener(this);

            _startVac = FindViewById<TextView>(Resource.Id.dayStart);
            _startDateMonth = FindViewById<TextView>(Resource.Id.monthStart);
            _startDateYear = FindViewById<TextView>(Resource.Id.yearStart);

            _endVac = FindViewById<TextView>(Resource.Id.dayEnd);
            _endDateMonth = FindViewById<TextView>(Resource.Id.monthEnd);
            _endDateYear = FindViewById<TextView>(Resource.Id.yearEnd);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewDetail);
            _adapter = new RadioButtonAdapter(_recyclerView);
            _recyclerView.SetAdapter(_adapter);

            _linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            _recyclerView.SetLayoutManager(_linearLayoutManager);
            _recyclerView.StopNestedScroll();

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            DetailFragmentStatePagerAdapter detailFragmentPagerAdapter = new DetailFragmentStatePagerAdapter(SupportFragmentManager);

            detailFragmentPagerAdapter.AddFragmentView((arg1, arg2, arg3) =>
            {
                return SetViewPagerFragment(arg1, arg2, "Regular", Resource.Drawable.Icon_Request_Green);
            });
            detailFragmentPagerAdapter.AddFragmentView((arg1, arg2, arg3) =>
            {
                return SetViewPagerFragment(arg1, arg2, "Sick", Resource.Drawable.Icon_Request_Plum);
            });
            detailFragmentPagerAdapter.AddFragmentView((arg1, arg2, arg3) =>
            {
                return SetViewPagerFragment(arg1, arg2, "Exceptional", Resource.Drawable.Icon_Request_Gray);
            });
            detailFragmentPagerAdapter.AddFragmentView((arg1, arg2, arg3) =>
            {
                return SetViewPagerFragment(arg1, arg2, "LeaveWithoutPay", Resource.Drawable.Icon_Request_Dark);
            });
            detailFragmentPagerAdapter.AddFragmentView((arg1, arg2, arg3) =>
            {
                return SetViewPagerFragment(arg1, arg2, "OverTime", Resource.Drawable.Icon_Request_Blue);
            });

            _viewPager.Adapter = detailFragmentPagerAdapter;

            var dots = FindViewById<TabLayout>(Resource.Id.dot);
            dots.SetupWithViewPager(_viewPager, true);

            var startDateEditText = FindViewById<RelativeLayout>(Resource.Id.relative1);
            startDateEditText.Click += (s, e) =>
            {
                _startDatePickerDialog.UpdateDate(StartDatePickerTime.Year, StartDatePickerTime.Month - 1, StartDatePickerTime.Day);
                _startDatePickerDialog.Show();
            };

            var endDateEditText = FindViewById<RelativeLayout>(Resource.Id.relative2);
            endDateEditText.Click += (s, e) =>
            {
                _endDatePickerDialog.UpdateDate(EndDatePickerTime.Year, EndDatePickerTime.Month - 1, EndDatePickerTime.Day);
                _endDatePickerDialog.Show();
            };
        }

        public override void Bind(BindingSet<DetailScreenViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(this).For(v => v.SetStartDatePickerDialogTwoWayBinding()).To(vm => vm.VacationData.StartDate);
            bindingSet.Bind(this).For(v => v.SetEndDatePickerDialogTwoWayBinding()).To(vm => vm.VacationData.EndDate);

            bindingSet.Bind(_viewPager).For(v => v.SetCurrentItemAndPageSelectedBinding()).To(vm => vm.VacationData.CurrentImageOnPage);

            bindingSet.Bind(_startVac).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateDayStart);
            bindingSet.Bind(_startDateMonth).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateMonthStart);
            bindingSet.Bind(_startDateYear).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateYearStart);

            bindingSet.Bind(_endVac).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateDayEnd);
            bindingSet.Bind(_endDateMonth).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateMonthEnd);
            bindingSet.Bind(_endDateYear).For(v => v.TextAndTextChangedBinding()).To(vm => vm.VacationData.DateYearEnd);


            bindingSet.Bind(_adapter).For(v => v.ItemsBinding()).To(vm => vm.Statuses);
            bindingSet.Bind(_adapter).For(v => v.ItemClickedBinding()).To(vm => vm.SelectStatusCommand);

            bindingSet.Bind(_saveButton).For(v => v.ClickBinding()).To(vm => vm.SaveCommand);
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            var monthText = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month + 1);

            if (view.Tag == _startDatePickerDialog.DatePicker.Tag)
            {
                FindViewById<TextView>(Resource.Id.dayStart).Text = dayOfMonth.ToString();
                FindViewById<TextView>(Resource.Id.monthStart).Text = monthText;
                FindViewById<TextView>(Resource.Id.yearStart).Text = year.ToString();

                StartDatePickerTime = new DateTime(year, month + 1, dayOfMonth);
                StartDateTimeChanged?.Invoke(this, EventArgs.Empty);
            }

            if (view.Tag == _endDatePickerDialog.DatePicker.Tag)
            {
                FindViewById<TextView>(Resource.Id.dayEnd).Text = dayOfMonth.ToString();
                FindViewById<TextView>(Resource.Id.monthEnd).Text = monthText;
                FindViewById<TextView>(Resource.Id.yearEnd).Text = year.ToString();

                EndDatePickerTime = new DateTime(year, month + 1, dayOfMonth);
                EndDateTimeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private View SetViewPagerFragment(LayoutInflater inflater, ViewGroup viewGroup, string title, int image)
        {
            var view = inflater.Inflate(Resource.Layout.ImageViewLayout, viewGroup, false);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            imageView.SetImageResource(image);
            var vacationType = view.FindViewById<TextView>(Resource.Id.vacationType);
            vacationType.Text = title;

            return view;
        }
    }
}