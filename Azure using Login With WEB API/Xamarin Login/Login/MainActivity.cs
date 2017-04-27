using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Login
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            // Get our button from the layout resource,
            // and attach an event to it
            btnsign = FindViewById<Button>(Resource.Id.btnloglogin);
            btncreate = FindViewById<Button>(Resource.Id.btnlogreg);
            txtusername = FindViewById<EditText>(Resource.Id.txtlogusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtlogpassword);
            btncreate.Click += Btncreate_Click;
            btnsign.Click += Btnsign_Click;
        }

        private async void Btnsign_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("http://xamarinlogin.azurewebsites.net/api/Login?username=" + txtusername.Text + "&password=" + txtPassword.Text));
            HttpResponseMessage response; ;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             response = await client.GetAsync(uri);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted)
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
            }
            else
            {
                var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1] { '"' });
                Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
            }
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(NewUserActivity));
        }
    }
}

