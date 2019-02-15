using CookItApp.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class MomentoDiaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public MomentoDiaDataBaseController()
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

        public MomentoDia Obtener(int id)
        {
            lock (locker)
            {
                if (id == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<MomentoDia>().FirstOrDefault(m => m._IdMomentoDia == id);
                }
            }
        }


        public int GuardarList(List<MomentoDia> obj)
        {

            lock (locker)
            {

                var ret = this.BorrarTodo();
                if (obj != null)
                {
                    return database.InsertAll(obj);
                }
                else
                {
                    return 0;
                }

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
