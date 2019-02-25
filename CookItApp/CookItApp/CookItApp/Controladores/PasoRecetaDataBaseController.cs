using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class PasoRecetaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public PasoRecetaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<PasoReceta>();
        }

        public List<PasoReceta> ObtenerList(int IdReceta)
        {
            lock (locker)
            {
                if (database.Table<PasoReceta>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    List<PasoReceta> pasoRecetas  = database.Table<PasoReceta>().Where(pr => pr._IdReceta == IdReceta).ToList();
                    return pasoRecetas;
                }
            }
        }

        public int GuardarList(List<PasoReceta> obj)
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

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<PasoReceta>();
            }
        }
    }
}
