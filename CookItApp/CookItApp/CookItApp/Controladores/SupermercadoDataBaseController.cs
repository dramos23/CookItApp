using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class SupermercadoDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public SupermercadoDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Supermercado>();

        }

        public List<Supermercado> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<Supermercado>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Supermercado>().ToList();
                }
            }
        }


        public int GuardarList(List<Supermercado> obj)
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
                return database.DeleteAll<Supermercado>();
            }
        }
    }
}
