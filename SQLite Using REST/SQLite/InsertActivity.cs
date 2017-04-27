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
using System.IO;

namespace SQLite
{
    [Activity(Label = "InsertActivity")]
    public class InsertActivity : Activity
    {
        Button btncreate;
        EditText txtname;
        EditText txtnickname;
        EditText txtdept;
        EditText txtplace;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Insert);
            txtname = FindViewById<EditText>(Resource.Id.txtname);
            txtnickname = FindViewById<EditText>(Resource.Id.txtnickname);
            txtdept = FindViewById<EditText>(Resource.Id.txtdept);
            txtplace = FindViewById<EditText>(Resource.Id.txtplace);

            btncreate = FindViewById<Button>(Resource.Id.btnsave);
            btncreate.Click += Btncreate_Click;
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "student.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<StudentTable>();
                StudentTable tbl = new StudentTable();
                tbl.StudentName = txtname.Text;
                tbl.NickName = txtnickname.Text;
                tbl.Dept = txtdept.Text;
                tbl.Place = txtplace.Text;
                db.Insert(tbl);
               
                clear();
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        void clear()
        {
            txtname.Text = "";
            txtnickname.Text = "";
            txtdept.Text = "";
            txtplace.Text = "";
            
        }
    }
}