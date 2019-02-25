using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class RecetaFavoritaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public RecetaFavoritaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<RecetaFavorita>();
        }

        public List<RecetaFavorita> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<RecetaFavorita>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<RecetaFavorita>().OrderBy(f => f._FechaHora).ToList();
                }
            }
        }

        public int GuardarList(List<RecetaFavorita> obj)
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

        public int Guardar(RecetaFavorita obj)
        {

            lock (locker)
            {

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
                return database.DeleteAll<RecetaFavorita>();
            }
        }

        public int Borrar(RecetaFavorita obj)
        {
            lock (locker)
            {
                return database.Delete<RecetaFavorita>(obj._IdReceta);
            }
        }

        public RecetaFavorita Obtener(int idReceta)
        {
            lock (locker) {

                return database.Table<RecetaFavorita>().FirstOrDefault(x => x._IdReceta == idReceta);

            }
        }
    }
}
