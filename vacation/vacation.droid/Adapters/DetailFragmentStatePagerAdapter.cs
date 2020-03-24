using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using vacation.droid.Activities;
using Fragment = Android.Support.V4.App.Fragment;

namespace vacation.droid.Adapters
{
    public class DetailFragmentStatePagerAdapter : FragmentStatePagerAdapter
    {
        private readonly List<Fragment> fragmentList = new List<Fragment>();

        public DetailFragmentStatePagerAdapter(FragmentManager fragmentManager)
            : base(fragmentManager)
        {
        }

        public override int Count
        {
            get { return fragmentList.Count; }
        }

        public override Fragment GetItem(int position)
        {
            return fragmentList[position];
        }

        public void AddFragmentView(Func<LayoutInflater, ViewGroup, Bundle, View> fragmentView)
        {
            fragmentList.Add(new DetailFragment(fragmentView));
        }
    }
}