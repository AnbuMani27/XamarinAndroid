using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Login
{
    [Activity(Label = "NewUserActivity")]
    public class NewUserActivity : Activity
    {

        EditText txtusername;
        EditText txtPassword;
        Button btncreate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Reg);
            txtusername = FindViewById<EditText>(Resource.Id.txtsaveusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtsavepassword);
            btncreate = FindViewById<Button>(Resource.Id.btnsavecreate);
            btncreate.Click += Btncreate_Click;

        }

        private async void Btncreate_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.username = txtusername.Text;
            log.password = txtPassword.Text;
            HttpClient client = new HttpClient();
            string url = "http://xamarinlogin.azurewebsites.net/api/Login/";
            var uri = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            var json = JsonConvert.SerializeObject(log);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
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
    }
}