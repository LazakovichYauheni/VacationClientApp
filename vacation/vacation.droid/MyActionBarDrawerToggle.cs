using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Views;

namespace vacation.droid
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
        private AppCompatActivity _hostActivity;
        private int _openedResource;
        private int _closedResource;

        public MyActionBarDrawerToggle(AppCompatActivity hostActivity, DrawerLayout drawerLayout, int openedResource, int closedResource)
            : base(hostActivity, drawerLayout, openedResource, closedResource)
        {
            _hostActivity = hostActivity;
            _openedResource = openedResource;
            _closedResource = closedResource;
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            _hostActivity.SupportActionBar.SetTitle(_openedResource);
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            _hostActivity.SupportActionBar.SetTitle(_closedResource);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}