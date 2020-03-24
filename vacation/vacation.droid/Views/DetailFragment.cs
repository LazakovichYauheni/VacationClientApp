using System;
using Android.OS;
using Android.Views;

namespace vacation.droid.Activities
{
    public class DetailFragment : Android.Support.V4.App.Fragment
    {
        private readonly Func<LayoutInflater, ViewGroup, Bundle, View> _view;
        private ViewGroup _viewGroup;

        public DetailFragment(Func<LayoutInflater, ViewGroup, Bundle, View> view)
        {
            _view = view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            _viewGroup = container;
            return _view(inflater, container, savedInstanceState);
        }
    }
}