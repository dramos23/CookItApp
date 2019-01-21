using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class ComentarioRecetaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public ComentarioRecetaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<ComentarioReceta>();
        }

        public List<ComentarioReceta> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<ComentarioReceta>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<ComentarioReceta>().ToList();
                }
            }
        }

        public int GuardarList(List<ComentarioReceta> obj)
        {

            lock (locker)
            {

                int ret = BorrarTodo();
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
                return database.DeleteAll<ComentarioReceta>();
            }
        }
    }
}
