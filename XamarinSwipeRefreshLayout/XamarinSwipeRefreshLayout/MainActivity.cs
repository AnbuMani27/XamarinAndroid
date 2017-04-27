using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using Java.Lang;

namespace XamarinSwipeRefreshLayout
{
    [Activity(Label = "XamarinSwipeRefreshLayout", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private SwipeRefreshLayout refreshLayout;
        private ListView listitem;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            listitem = FindViewById<ListView>(Resource.Id.listView1);

            List<string> items = new List<string>();
            items.Add("C#");
            items.Add("ASP.NET");
            items.Add("JAVA");
            items.Add("MVC");
            items.Add("Swift");
            items.Add("JQuery");
            items.Add("ASP");
            items.Add("JSP");
            items.Add("C");
            items.Add("PHP");
            items.Add("Ruby");
            
            ArrayAdapter<string> liststring = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, items);
            listitem.Adapter = liststring;
                     
            refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            refreshLayout.SetColorSchemeColors(Color.Red, Color.Green, Color.Blue, Color.Yellow);

            refreshLayout.Refresh += RefreshLayout_Refresh;
        }

        private void RefreshLayout_Refresh(object sender, EventArgs e)
        {
            //Data Refresh Place

            BackgroundWorker work = new BackgroundWorker();
            work.DoWork += Work_DoWork;
            work.RunWorkerCompleted += Work_RunWorkerCompleted;
            work.RunWorkerAsync();
        }

       
        private void Work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshLayout.Refreshing = false;

        }

        private void Work_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);

        }
    }
}

