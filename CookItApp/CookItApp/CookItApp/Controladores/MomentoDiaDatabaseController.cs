using CookItApp.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class MomentoDiaDatabaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public MomentoDiaDatabaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<MomentoDia>();

        }

        public List<MomentoDia> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<MomentoDia>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<MomentoDia>().ToList();
                }
            }
        }


        public int GuardarList(List<MomentoDia> momentoDias)
        {

            lock (locker)
            {

                var ret = this.BorrarTodo();                
                return database.InsertAll(momentoDias);
                
            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<MomentoDia>();
            }
        }
    }
}
