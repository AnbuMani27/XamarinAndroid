using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;


namespace CustomActionBarMenuCreation
{
    [Activity(Label = "Menu Creation", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
    

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            Button btnpopupmenu = FindViewById<Button>(Resource.Id.btnpopupmenu);
            btnpopupmenu.Click+= (s, arg) => {
                PopupMenu menu = new PopupMenu(this, btnpopupmenu);
                menu.Inflate(Resource.Menu.popup_menu);
                menu.Show();
            };
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.option_menu, menu);
            return true;

        }

    }
}

