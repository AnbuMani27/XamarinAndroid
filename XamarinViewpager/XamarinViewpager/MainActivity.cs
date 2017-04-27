using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.Design.Widget;

using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using System.Collections.Generic;

using V7Toolbar = Android.Support.V7.Widget.Toolbar;


namespace XamarinViewpager
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Activity(Label = "XamarinViewpager", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity
    {

        ViewPager viewpager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);      
            SetContentView(Resource.Layout.Main);
            
            viewpager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.SetIcon(Resource.Drawable.Icon);
       
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);

           
         setupViewPager(viewpager);                
          

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += (sender, e) => {
                Snackbar.Make(fab, "Here's a snackbar!", Snackbar.LengthLong).SetAction("Action",
                    v => Console.WriteLine("Action handler")).Show();
            };

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewpager);
        }
        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new TabFragment1(), "First Fragment");
            adapter.AddFragment(new TabFragment2(), "Second Fragment");
                       viewPager.Adapter = adapter;
            viewpager.Adapter.NotifyDataSetChanged();
            //viewpager.OffscreenPageLimit(4);


        }
       
    }
    class Adapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();


        public Adapter(V4FragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(V4Fragment fragment, String title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);


        }

        public override V4Fragment GetItem(int position)
        {
            return fragments[position];

        }

        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }

       
    }

}

