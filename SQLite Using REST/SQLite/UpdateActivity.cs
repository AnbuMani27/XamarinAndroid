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
    [Activity(Label = "UpdateActivity")]
    public class UpdateActivity : Activity
    {

        Button btn_get;
        Button btn_update;

        EditText txt_id;
       EditText txt_updatename;
       EditText txt_updatenickname;
       EditText txt_updatedept;
        EditText txt_updateplace;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Update);

            txt_updatename = FindViewById<EditText>(Resource.Id.txt_updatename);
            txt_updatenickname = FindViewById<EditText>(Resource.Id.txt_updatenickname);
            txt_updatedept = FindViewById<EditText>(Resource.Id.txt_updatedept);
            txt_updateplace = FindViewById<EditText>(Resource.Id.txt_updateplace);


            txt_id = FindViewById<EditText>(Resource.Id.txt_update_id);
            btn_get = FindViewById<Button>(Resource.Id.btnget);
            btn_update = FindViewById<Button>(Resource.Id.btn_update);
            btn_get.Click += Btn_get_Click;
            btn_update.Click += Btn_update_Click;
           
        }

       

        private void Btn_get_Click(object sender, EventArgs e)
        {
            //clear();
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "student.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<StudentTable>(); //Call Table  
           int idvalue = Convert.ToInt32(txt_id.Text);

            var data1 = (from values in data
                         where values.id == idvalue
                         select new StudentTable
                         {
                             StudentName = values.StudentName,
                             NickName = values.NickName,
                             Dept = values.Dept,
                             Place = values.Place

                         }).ToList<StudentTable>();

            if (data1.Count > 0)
            {
                foreach (var val in data1)
                {
                    txt_updatename.Text = val.StudentName;
                    txt_updatenickname.Text = val.NickName;
                    txt_updatedept.Text = val.Dept;
                    txt_updateplace.Text = val.Place;
                }
      }
            else
            {

                Toast.MakeText(this, "Student Data Not Available", ToastLength.Short).Show();
            }

        }

        private void Btn_update_Click(object sender, EventArgs e)
        {


            try
            {

                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "student.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<StudentTable>();
                int idvalue = Convert.ToInt32(txt_id.Text);

                var data1 = (from values in data
                             where values.id == idvalue
                             select values).Single();
                data1.StudentName = txt_updatename.Text;
                data1.NickName = txt_updatenickname.Text;
                data1.Dept = txt_updatedept.Text;
                data1.Place = txt_updateplace.Text;
                db.Update(data1);
                Toast.MakeText(this, "Updated Successfully", ToastLength.Short).Show();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();

            }

        }
    }
}