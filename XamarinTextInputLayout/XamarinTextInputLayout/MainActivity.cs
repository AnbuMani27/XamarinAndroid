using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;

namespace XamarinTextInputLayout
{
    [Activity(Label = "XamarinTextInputLayout", Theme = "@style/Theme.DesignDemo",MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        Spinner spinner;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
                        
            SetContentView(Resource.Layout.Main);

           //TextInputLayout
            TextInputLayout txtlayoutusername = FindViewById<TextInputLayout>(Resource.Id.textuernameInputLayout);
            TextInputLayout textpasswordInputLayout = FindViewById<TextInputLayout>(Resource.Id.textpasswordInputLayout);
            EditText txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            EditText txtpassword  = FindViewById<EditText>(Resource.Id.txtpassword);

            Button btnclick = FindViewById<Button>(Resource.Id.btnclick);
             
            //Spinner
            spinner = FindViewById<Spinner>(Resource.Id.ddlmechservicetype);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.pro_Languages, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;


            //Button Click
            btnclick.Click += (sender, e) =>
            {
               //Showing Notifications

                Toast.MakeText(this, txtusername.Text, ToastLength.Short).Show();
                Toast.MakeText(this, txtpassword.Text, ToastLength.Short).Show();
                Toast.MakeText(this, spinner.SelectedItemPosition.ToString(), ToastLength.Short).Show();
                
            };


        }
    }
}

