using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace ListviewItems
{
    [Activity(Label = "ListviewItems", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private ListView listnames;
        private List<string> itemlist;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            listnames = FindViewById<ListView>(Resource.Id.listView1);

            itemlist = new List<string>();
            itemlist.Add("Item 0");
            itemlist.Add("Item 1");
            itemlist.Add("Item 2");
            itemlist.Add("Item 3");
            itemlist.Add("Item 4");
            itemlist.Add("Item 5");
            itemlist.Add("Item 6");

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,itemlist);

            listnames.Adapter=adapter;

            listnames.ItemClick += Listnames_ItemClick;
        }

        private void Listnames_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, e.Position.ToString(), ToastLength.Long).Show();
        }
    }
}

