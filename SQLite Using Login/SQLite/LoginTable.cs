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

namespace SQLite
{
    public class LoginTable
    {

        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]
        public string username { get; set; }
        [MaxLength(15)]
        public string password { get; set; }
       
    }
}