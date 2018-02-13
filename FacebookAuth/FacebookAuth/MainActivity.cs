using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Java.Lang;
using System;
using Xamarin.Facebook.Login.Widget;
using System.Collections.Generic;
using Android.Content.PM;
using Java.Security;

namespace FacebookAuth
{
    [Activity(Label = "FacebookAuth", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity,IFacebookCallback
    {

        private ICallbackManager mFBCallManager;
        private MyProfileTracker mprofileTracker;

        private TextView TxtFirstName;
        private TextView TxtLastName;
        private TextView TxtName;
        private ProfilePictureView mprofile;
        LoginButton BtnFBLogin;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

           

            //PackageInfo info = PackageManager.GetPackageInfo();
            //      "Package name",  //Replace your package name here
            //      PackageManager.GET_SIGNATURES);
            //for (Signature signature : info.signatures)
            //{
            //    MessageDigest md = MessageDigest.getInstance("SHA");
            //    md.update(signature.toByteArray());
            //    Log.e("KeyHash:", Base64.encodeToString(md.digest(), Base64.DEFAULT));
            //}

            FacebookSdk.SdkInitialize(this.ApplicationContext);
            mprofileTracker = new MyProfileTracker();
            mprofileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;
            mprofileTracker.StartTracking();
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            BtnFBLogin = FindViewById<LoginButton>(Resource.Id.fblogin);
            TxtFirstName = FindViewById<TextView>(Resource.Id.TxtFirstname);
            TxtLastName = FindViewById<TextView>(Resource.Id.TxtLastName);
            TxtName = FindViewById<TextView>(Resource.Id.TxtName);
            mprofile = FindViewById<ProfilePictureView>(Resource.Id.ImgPro);

            BtnFBLogin.SetReadPermissions(new List<string> { "user_friends", "public_profile" });
            mFBCallManager = CallbackManagerFactory.Create();
            BtnFBLogin.RegisterCallback(mFBCallManager, this);
        }
        public void OnCancel()
        { 
        } 
        public void OnError(FacebookException p0)
        {
        } 
        public void OnSuccess(Java.Lang.Object p0)
        { 
        }
        void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    TxtFirstName.Text = e.mProfile.FirstName;
                    TxtLastName.Text = e.mProfile.LastName;
                    TxtName.Text = e.mProfile.Name;
                    mprofile.ProfileId = e.mProfile.Id; 
                }
                catch(Java.Lang.Exception ex)
                {

                } 
            }

            else
            { 
                TxtFirstName.Text = "First Name";
                TxtLastName.Text = "Last Name";
                TxtName.Text = "Name";
                mprofile.ProfileId = null;
            }
        } 
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            mFBCallManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        public class MyProfileTracker : ProfileTracker
        {
            public event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged;

            protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
            {
                if (mOnProfileChanged != null)
                {
                    mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
                }
            }
        }
        public class OnProfileChangedEventArgs : EventArgs
        {
            public Profile mProfile;

            public OnProfileChangedEventArgs(Profile profile) { mProfile = profile; }
        }
    }
}

