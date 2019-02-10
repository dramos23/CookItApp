using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            database.DropTable<Reto>();
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
                    return database.Table<Reto>().OrderBy(r => r._Fecha).ToList();
                    
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
                    return database.Table<Reto>().FirstOrDefault(r => r._IdReto == reto._IdReto);
                }
            }
        }

        public int RetosActivos()
        {
            lock (locker)
            {
                return database.Table<Reto>().Where(n => n._IdEstadoReto < 5).Count();
            }
        }


        public int GuardarList(List<Reto> obj)
        {

            lock (locker)
            {

                if (obj.Count > 0)
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

        public bool Existe(Reto obj)
        {

            lock (locker)
            {
                var reto = database.Table<Reto>().FirstOrDefault(r => r._IdReto == obj._IdReto);
                return reto != null ? true : false;
            }
        }

        public int Guardar(Reto obj)
        {

            lock (locker)
            {

                if (obj != null)
                {
                    Reto reto = Obtener(obj);

                    if (reto != null)
                    {
                        return database.Update(obj);
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

        public int Modificar(Reto reto)
        {
            lock (locker)
            {
                if (database.Table<Reto>().Count() != 0)
                {
                    Borrar(reto);
                    return Guardar(reto);
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

        public int Borrar(Reto reto) {

            lock (locker)
            {
                try
                {
                    var ret = database.Delete<Reto>(reto._IdReto);
                    return ret;

                }catch(SQLiteException ex)
                {
                    Debug.Print(ex.Message);
                    return 0;
                }
            }
        }
    }
}
