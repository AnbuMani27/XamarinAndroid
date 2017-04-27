using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;

namespace Animation
{
    [Activity(Label = "Animation", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AnimationDrawable Animation;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Load the animation from resources
            Animation = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.animation);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView.SetImageDrawable(Animation);

        }
    }
}

