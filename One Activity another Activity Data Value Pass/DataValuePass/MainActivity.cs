using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace DataValuePass
{
    [Activity(Label = "DataValuePass", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        EditText txtdatapass;
        Button btnsend;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnsend = FindViewById<Button>(Resource.Id.btnsend);
            txtdatapass = FindViewById<EditText>(Resource.Id.txtpassvalue);
            btnsend.Click += Button_Click;

          
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SecondActivity));
            intent.PutExtra("DATA_PASS", txtdatapass.Text);
            this.StartActivity(intent);
        }
    }
}

