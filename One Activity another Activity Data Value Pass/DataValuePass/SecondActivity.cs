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

namespace DataValuePass
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {

        Button btngetvalue;
        TextView txtgevalue;
        string txtvalues;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Secondpage);

            txtvalues = Intent.GetStringExtra("DATA_PASS");
            btngetvalue = FindViewById<Button>(Resource.Id.btngetvalue);
            txtgevalue = FindViewById<TextView>(Resource.Id.txtgetvalue);
            btngetvalue.Click += Btngetvalue_Click; ;

        }

        private void Btngetvalue_Click(object sender, EventArgs e)
        {
            txtgevalue.Text = txtvalues;
        }
    }
}