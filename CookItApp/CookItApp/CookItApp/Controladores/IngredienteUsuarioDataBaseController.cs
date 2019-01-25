using CookItApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Controladores
{
    public class IngredienteUsuarioDataBaseController
    {
        static readonly object locker = new object();

        SQLiteConnection database;

        public IngredienteUsuarioDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<IngredienteUsuario>();
        }

        public List<IngredienteUsuario> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<IngredienteUsuario>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<IngredienteUsuario>().ToList();
                }
            }
        }

        public int Guardar(IngredienteUsuario obj)
        {
            lock (locker)
            {
                IngredienteUsuario ingredienteUsuario = database.Table<IngredienteUsuario>().Where(i => i._IdIngrediente == obj._IdIngrediente).First();

                if (ingredienteUsuario != null)
                {
                    
                    ingredienteUsuario._Cantidad += obj._Cantidad; 
                    database.Insert(ingredienteUsuario);
                    return 1;
                }
                else
                {
                    return database.Insert(obj);
                }
            }
        }

        public int GuardarList(List<IngredienteUsuario> obj)
        {

            lock (locker)
            {
                
                if (obj.Count >  0)
                {
                    return database.InsertAll(obj);
                }
                else {
                    return 0;
                }

            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<IngredienteUsuario>();
            }
        }
    }
}
