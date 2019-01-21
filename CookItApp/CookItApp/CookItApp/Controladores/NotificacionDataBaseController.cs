using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class NotificacionDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public NotificacionDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Notificacion>();
        }

        public List<Notificacion> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<Notificacion>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Notificacion>().ToList();
                }
            }
        }

        public int GuardarList(List<Notificacion> obj)
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
                return database.DeleteAll<Notificacion>();
            }
        }
    }
}
