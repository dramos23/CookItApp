using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class IngredienteDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public IngredienteDataBaseController()
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

        public int GuardarList(List<MomentoDia> obj)
        {

            lock (locker)
            {

                var ret = BorrarTodo();
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
