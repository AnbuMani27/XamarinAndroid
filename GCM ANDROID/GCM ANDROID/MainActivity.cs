using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using Android.Gms.Common;

using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace GCM_ANDROID
{
    [Activity(Label = "GCM ANDROID", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        TextView txtmsg;
        Button btnsend;
        public const string API_KEY = "YOUR API KEY";
        //public const string MESSAGE;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            txtmsg = FindViewById<TextView>(Resource.Id.txtmsg);
            btnsend = FindViewById<Button>(Resource.Id.btnsend);

            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }

            btnsend.Click += (object sender, EventArgs e) =>
            {
                OnButtonClicked(sender, e);
            };


        }
        public async void OnButtonClicked(object sender, EventArgs args)
        {

            var MESSAGE = txtmsg.Text;
            var jGcmData = new JObject();
            var jData = new JObject();
            jData.Add("message", MESSAGE);
            jGcmData.Add("to", "/topics/global");
            jGcmData.Add("data", jData); string a;
            var url = new Uri("https://gcm-http.googleapis.com/gcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + API_KEY);

                    Task.WaitAll(client.PostAsync(url, new StringContent(jGcmData.ToString(), Encoding.Default, "application/json"))
                             .ContinueWith(response =>
                             {
                                 Console.WriteLine(response);
                                 Console.WriteLine("Message sent: check the client device notification tray.");
                             }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send GCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }

        }


        public bool IsPlayServicesAvailable()
        {
            int resultCode = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GooglePlayServicesUtil.IsUserRecoverableError(resultCode))
                    txtmsg.Text = GooglePlayServicesUtil.GetErrorString(resultCode);
                else
                {
                    Toast.MakeText(this, "Sorry, this device is not supported",ToastLength.Short).Show();
                    Finish();
                }
                return false;
            }
            else
            {
                Toast.MakeText(this, "Google Play Services is available.", ToastLength.Short).Show();
                return true;
            }
        }





    }
}

