using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class RetoDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public RetoDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Reto>();
        }

        public List<Reto> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<Reto>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Reto>().ToList();
                }
            }
        }

        public Reto Obtener(Reto reto)
        {
            lock (locker)
            {
                if (database.Table<Reto>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Reto>().FirstOrDefault(r => r._EmailUsuOri == reto._EmailUsuOri && 
                                                                      r._EmialUsuDes == reto._EmialUsuDes && 
                                                                      r._RecetaId == reto._RecetaId && 
                                                                      r._Cumplido == reto._Cumplido);
                }
            }
        }

        public int GuardarList(List<Reto> obj)
        {

            lock (locker)
            {

                if (obj.Count > 0)
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
                return database.DeleteAll<Reto>();
            }
        }
    }
}
