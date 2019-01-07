using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CookItApp.iOS.Data;
using Foundation;
using SQLite;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_IOS))]

namespace CookItApp.iOS.Data
{
    public class SQLite_IOS : ISQLIte
    {
        public SQLite_IOS() { }
        public SQLiteConnection GetConnection()
        {
            var fileName = "CookItBD.db3";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var con = new SQLiteConnection(path);
            return con;
        }
    }
}