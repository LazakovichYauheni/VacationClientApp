using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using vacation.core.ViewModels;
using vacation.droid.Adapters;
using Android.Support.V7.Widget.Helper;
using vacation.droid.CustomBinding;

namespace vacation.droid.Activities
{
    [Activity(Label = "MainListActivity", Theme = "@/style/My", MainLauncher = false)]
    public class MainListActivity : BindableAppCompatActivity<MainListViewModel>
    {
        private RecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        private VacationsAdapter _adapter;
        private FloatingActionButton _fabButton;
        private TextView _sortByAll;
        private TextView _sortByApproved;
        private TextView _sortByClosed;
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;
        private MyActionBarDrawerToggle _drawerToggle;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainlist_layout);

            _sortByAll = FindViewById<TextView>(Resource.Id.sort_all);
            _sortByApproved = FindViewById<TextView>(Resource.Id.sort_approved);
            _sortByClosed = FindViewById<TextView>(Resource.Id.sort_closed);

            _fabButton = FindViewById<FloatingActionButton>(Resource.Id.fab);

            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            _layoutManager = new LinearLayoutManager(this);
            _recyclerView.SetLayoutManager(_layoutManager);
            _adapter = new VacationsAdapter(_recyclerView);
            _recyclerView.SetAdapter(_adapter);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(true);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            var swipeHandler = new SwipeToDeleteCallback(0, ItemTouchHelper.Left, this, _adapter);
            var itemTouchHelper = new ItemTouchHelper(swipeHandler);
            itemTouchHelper.AttachToRecyclerView(_recyclerView);

            _drawerToggle = new MyActionBarDrawerToggle(
                this,
                _drawerLayout,
                Resource.String.openDrawer,
                Resource.String.closeDrawer
                );
            _drawerLayout.SetDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();
            
            _sortByApproved.Click += (s, e) =>
            {
                _drawerLayout.CloseDrawers();
            };
            _sortByClosed.Click += (s, e) =>
            {
                _drawerLayout.CloseDrawers();
            };
            _sortByAll.Click += (s, e) =>
            {
                _drawerLayout.CloseDrawers();
            };
        }
        
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            _drawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        public override void Bind(BindingSet<MainListViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(_adapter).For(v => v.ItemsBinding()).To(vm => vm.VacationsList);
            bindingSet.Bind(_adapter).For(v => v.ItemClickedBinding()).To(vm => vm.ItemSelectedCommand);
            bindingSet.Bind(_adapter).For(v => v.SetItemSwipedBinding()).To(vm => vm.DeleteItemCommand);

            bindingSet.Bind(_fabButton).For(v => v.ClickBinding()).To(vm => vm.CreateVacationCommand);

            bindingSet.Bind(_sortByAll).For(v => v.ClickBinding()).To(vm => vm.SortByAll);
            bindingSet.Bind(_sortByApproved).For(v => v.ClickBinding()).To(vm => vm.SortByApproved);
            bindingSet.Bind(_sortByClosed).For(v => v.ClickBinding()).To(vm => vm.SortByClosed);
        }
    }
}