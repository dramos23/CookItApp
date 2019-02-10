using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class EstadoRetoDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public EstadoRetoDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<EstadoReto>();

        }

        public List<EstadoReto> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<EstadoReto>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<EstadoReto>().ToList();
                }
            }
        }

        public EstadoReto Obtener(int Id)
        {
            lock (locker)
            {
                if (database.Table<EstadoReto>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<EstadoReto>().FirstOrDefault(er => er._IdEstadoReto == Id);
                }
            }
        }


        public int GuardarList(List<EstadoReto> obj)
        {

            lock (locker)
            {
                
                if (obj != null)
                {
                    BorrarTodo();
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
                return database.DeleteAll<EstadoReto>();
            }
        }
    }
}
