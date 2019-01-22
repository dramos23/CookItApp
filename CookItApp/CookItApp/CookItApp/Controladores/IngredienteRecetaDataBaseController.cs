using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class IngredienteRecetaDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public IngredienteRecetaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<IngredienteReceta>();
        }

        public List<IngredienteReceta> ObtenerList(int IdReceta)
        {
            lock (locker)
            {
                if (database.Table<IngredienteReceta>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    List<IngredienteReceta> ingredienteRecetas = database.Table<IngredienteReceta>().Where(ir => ir._IdReceta == IdReceta) .ToList();
                    foreach(IngredienteReceta ir in ingredienteRecetas)
                    {
                        ir._Ingrediente = App.DataBase.Ingrediente.Obtener(ir._IdIngrediente);
                    }

                    return ingredienteRecetas;

                }
            }
        }


        public int GuardarList(List<IngredienteReceta> obj)
        {

            lock (locker)
            {

                var ret = BorrarTodo();
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
                return database.DeleteAll<IngredienteReceta>();
            }
        }
    }
}
