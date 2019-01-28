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


        public Notificacion Obtener(Notificacion obj)
        {
            lock (locker)
            {
                if (database.Table<Notificacion>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Notificacion>().FirstOrDefault(n => n._NotificacionId == obj._NotificacionId);
                }
            }
        }

        public int GuardarList(List<Notificacion> obj)
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

        public int Modificar(Notificacion obj)
        {

            lock (locker)
            {
                if (obj != null)
                {
                    var notificacion = Obtener(obj);


                    if (notificacion != null)
                    {
                        database.Delete<Notificacion>(notificacion._NotificacionId);
                        return database.Insert(obj);
                    }
                    else
                    {
                        return database.Insert(obj);
                    }
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
