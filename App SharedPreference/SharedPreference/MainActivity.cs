using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace SharedPreference
{
    [Activity(Label = "SharedPreference", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnsave;
        Button btnget;
        Button btnreset;
        EditText txtname;
        TextView txtview;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            btnsave = FindViewById<Button>(Resource.Id.btnsave);
            btnget = FindViewById<Button>(Resource.Id.btnget);
            btnreset = FindViewById<Button>(Resource.Id.btnreset);
            txtname = FindViewById<EditText>(Resource.Id.txtsave);
            txtview = FindViewById<TextView>(Resource.Id.txtgetname);

            btnsave.Click += Btnsave_Click;
            btnget.Click += Btnget_Click;
            btnreset.Click += Btnreset_Click;


        }
       
        private void Btnsave_Click(object sender, EventArgs e)
        {
            //Save Sessionvalue

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            ap.saveAccessKey(txtname.Text);
            txtname.Text = "";
        }
      

        private void Btnget_Click(object sender, EventArgs e)
        {
            //Get Sessionvalue
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);
            string key = ap.getAccessKey();
            txtview.Text = key.ToString();
        }

        private void Btnreset_Click(object sender, EventArgs e)
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editer = pref.Edit();
            editer.Remove("PREFERENCE_ACCESS_KEY").Commit();  ////Remove Spec key values
        }

    }
}

