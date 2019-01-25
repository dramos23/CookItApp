using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class HistorialRecetaDatabaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public HistorialRecetaDatabaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<HistorialReceta>();
        }

        public List<HistorialReceta> ObtenerList(Usuario usuario)
        {
            lock (locker)
            {
                if (database.Table<HistorialReceta>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<HistorialReceta>().ToList();
                }
            }
        }

        public int GuardarList(List<HistorialReceta> obj)
        {

            lock (locker)
            {
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


        public int Guardar(HistorialReceta obj)
        {

            lock (locker)
            {

                int ret = BorrarTodo();
                if (obj != null)
                {
                    return database.Insert(obj);
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
                return database.DeleteAll<HistorialReceta>();
            }
        }
    }
}
