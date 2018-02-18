using Android.App;
using Android.Widget;
using Android.OS; 
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Util;
using Android.Graphics;
using System.Net;
using System; 
using Android.Content; 
 
using Java.IO;


namespace GSignIn
{
    [Activity(Label = "GSignIn", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {

        GoogleApiClient mGoogleApiClient;
        private ConnectionResult mConnectionResult;
        SignInButton mGsignBtn;
        TextView TxtName, TxtGender;
        ImageView ImgProfile;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        private bool mInfoPopulated;
        public string TAG { get; private set; }      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            mGsignBtn = FindViewById<SignInButton>(Resource.Id.sign_in_button);
            TxtGender = FindViewById<TextView>(Resource.Id.TxtGender);  
            TxtName = FindViewById<TextView>(Resource.Id.TxtName); 
             ImgProfile = FindViewById<ImageView>(Resource.Id.ImgProfile);
            mGsignBtn.Click += MGsignBtn_Click;
            GoogleApiClient.Builder builder = new GoogleApiClient.Builder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);
            //Build our IGoogleApiClient
            mGoogleApiClient = builder.Build();
        }

        private void MGsignBtn_Click(object sender, EventArgs e)
        {
            if (!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                ResolveSignInError();

            }
            else if (mGoogleApiClient.IsConnected)
            {
               
                 
                    PlusClass.AccountApi.ClearDefaultAccount(mGoogleApiClient);
                    mGoogleApiClient.Disconnect();
                 
            }
        }
        private void ResolveSignInError()
        {
            if (mGoogleApiClient.IsConnecting)
            {
                return;
            }
            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch (Android.Content.IntentSender.SendIntentException io)
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }

            }
        }
        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
            {
                mGoogleApiClient.Disconnect();
            }
        }


        public void OnConnected(Bundle connectionHint)
        {
            var person = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
            var name = string.Empty;
            if (person != null)

            {
                TxtName.Text = person.DisplayName;
                TxtGender.Text= person.Nickname;
                var Img = person.Image.Url;
                var imageBitmap = GetImageBitmapFromUrl(Img.Remove(Img.Length-5));
                if(imageBitmap!=null)
                ImgProfile.SetImageBitmap(imageBitmap);
        
                 
            }


        }
        private Bitmap GetImageBitmapFromUrl(String url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

                return imageBitmap;
            }
            catch (IOException e)
            {
               // Log.Debuge(TAG, "Error getting bitmap", e);
            }
            return null;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);

            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }
                mIntentInProgress = false;

                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }

            }
        }
        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                //Store the ConnectionResult so that we can use it later when the user clicks 'sign-in;
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    //The user has already clicked 'sign-in' so we attempt to resolve all
                    //errors until the user is signed in, or the cancel
                    ResolveSignInError();
                }
            }
        }

        public void OnConnectionSuspended(int cause)
        {

        } 

    }
}

