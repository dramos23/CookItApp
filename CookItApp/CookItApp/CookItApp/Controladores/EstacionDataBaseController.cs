using CookItApp.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class EstacionDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public EstacionDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Estacion>();

        }

        public List<Estacion> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<Estacion>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Estacion>().ToList();
                }
            }
        }


        public int GuardarList(List<Estacion> Estacions)
        {

            lock (locker)
            {

                var ret = this.BorrarTodo();
                return database.InsertAll(Estacions);

            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<Estacion>();
            }
        }
    }
}

