using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

namespace CheckNetwork
{
    [Activity(Label = "CheckNetwork", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
         protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {

                Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)GetSystemService(ConnectivityService);
                Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
                bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
                if (isOnline == false)
                {

                    Toast.MakeText(this, "Network Not Enable", ToastLength.Long).Show();
                }
                else
                {

                    LocationManager mlocManager = (LocationManager)GetSystemService(LocationService); ;
                    bool enabled = mlocManager.IsProviderEnabled(LocationManager.GpsProvider);
                    if (enabled == false)
                    {
                        Toast.MakeText(this, "GPS Not Enable", ToastLength.Long).Show();
                    }
                }

            };
        }
    }
}

