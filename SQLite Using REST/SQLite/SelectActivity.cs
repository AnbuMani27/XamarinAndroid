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
    [Activity(Label = "SelectActivity")] 
    public class SelectActivity : Activity
    {
        Button btn_select;
        EditText txt_id;
        TextView txt_name;
        TextView txt_nickname;
        TextView txt_dept;
        TextView txt_place;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Select);
            txt_name = FindViewById<TextView>(Resource.Id.txt_studentname);
            txt_nickname = FindViewById<TextView>(Resource.Id.txt_nickname);
            txt_dept = FindViewById<TextView>(Resource.Id.txt_dept);
            txt_place = FindViewById<TextView>(Resource.Id.txt_place);
            txt_id  = FindViewById<EditText>(Resource.Id.txtid);
            btn_select = FindViewById<Button>(Resource.Id.btn_select);
            btn_select.Click += Btn_select_Click;
        }
       private void Btn_select_Click(object sender, EventArgs e)
        {
            clear();
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "student.db3"); //Call Database  
            var db = new SQLiteConnection(dpPath);
            var data = db.Table<StudentTable>(); //Call Table  
            int idvalue = Convert.ToInt32(txt_id.Text);
            var data1=(from values in data
             where values.id== idvalue
             select new StudentTable
             {
                 StudentName = values.StudentName,
                 NickName = values.NickName,
                 Dept = values.Dept,
                 Place = values.Place

             }).ToList<StudentTable>();

            if(data1.Count>0)
            {
                foreach (var val in data1)
                {

                    txt_name.Text = val.StudentName;
                    txt_nickname.Text = val.NickName;
                    txt_dept.Text = val.Dept;
                    txt_place.Text = val.Place;
                }

            }
            else
            {
               
                Toast.MakeText(this, "Student Data Not Available", ToastLength.Short).Show();
            }
            
        }

        void clear()
        {
            txt_name.Text = "";
            txt_nickname.Text = "";
            txt_dept.Text = "";
            txt_place.Text = "";
        }
    }
}