using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Linq;

namespace SMS
{
    [Activity(Label = "SMS", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btncontact;
        Button btnsmssend;
        EditText txtmobilenum;
        EditText txtbodymsg;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            txtbodymsg = FindViewById<EditText>(Resource.Id.txtbodymsg);
            txtmobilenum = FindViewById<EditText>(Resource.Id.txtmobilenum);
            btncontact = FindViewById<Button>(Resource.Id.btncontact);
            btnsmssend = FindViewById<Button>(Resource.Id.btnsmssend);
            btncontact.Click += Btncontact_Click;
            btnsmssend.Click += Btnsmssend_Click;

        }

        private void Btnsmssend_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtbodymsg.Text))
                {

                    Toast.MakeText(this, "SMS Text Body Is Empty", ToastLength.Long).Show();
                }
                else if (string.IsNullOrWhiteSpace(txtmobilenum.Text))
                {

                    Toast.MakeText(this, "Enter Mobile Number", ToastLength.Long).Show();
                }

                else
                {

                    Toast.MakeText(this, "Sending SMS... Please wait...", ToastLength.Short).Show();
                    //Send SMS!
                    var smsMgr = Android.Telephony.SmsManager.Default;
                    smsMgr.SendTextMessage(txtmobilenum.Text, null, txtbodymsg.Text, null, null);

                    Toast.MakeText(this, "SMS successfully sent", ToastLength.Short).Show();

                    txtbodymsg.Text = "";
                    txtmobilenum.Text = "";


                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();

            }
        }

        private void Btncontact_Click(object sender, EventArgs e)
        {
            var contactPickerIntent = new Intent(Intent.ActionPick,
                Android.Provider.ContactsContract.Contacts.ContentUri);
            StartActivityForResult(contactPickerIntent, 101);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

            if (requestCode == 101 && resultCode == Result.Ok)
            {
                //Ensure we have data returned
                if (data == null || data.Data == null)
                    return;
                var addressBook = new Xamarin.Contacts.AddressBook(this);
                addressBook.PreferContactAggregation = false;
                //Load the contact via the android contact id
                // in the last segment of the Uri returned by the 
                // android contact picker
                var contact = addressBook.Load(data.Data.LastPathSegment);
                //Use linq to find a mobile number
                var mobile = (from p in contact.Phones
                              where
                p.Type == Xamarin.Contacts.PhoneType.Mobile
                              select p.Number).FirstOrDefault();
                //See if the contact has a mobile number
                if (string.IsNullOrEmpty(mobile))
                {
                    Toast.MakeText(this, "No Mobile Number for contact!",
                        ToastLength.Short).Show();
                    return;
                }
                txtmobilenum.Text = mobile;
            }

        }
    }
}

