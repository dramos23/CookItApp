using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CookItApp.Droid.Data;
using SQLite;


[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]


namespace CookItApp.Droid.Data
{
    public class SQLite_Android : ISQLIte
    {

        public SQLite_Android() { }

        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "CookItAppBD3.db3";
            string docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(docPath, sqliteFileName);
            var con = new SQLiteConnection(path);
            return con;
        }


    }
}