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
        private static object locker = new object();

        private SQLiteConnection database;

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
                    List<IngredienteUsuario> ingredientesUsuario = database.Table<IngredienteUsuario>().ToList();
                    foreach(IngredienteUsuario iu in ingredientesUsuario)
                    {
                        iu._Ingrediente = App.DataBase.Ingrediente.Obtener(iu._IdIngrediente);
                    }
                    return ingredientesUsuario;
                }
            }
        }

        public int Guardar(IngredienteUsuario obj)
        {
            lock (locker)
            {
                IngredienteUsuario ingredienteUsuario = database.Table<IngredienteUsuario>().FirstOrDefault(i => i._IdIngrediente == obj._IdIngrediente);

                if (ingredienteUsuario != null)
                {
                    
                    ingredienteUsuario._Cantidad += obj._Cantidad; 
                    database.Update(ingredienteUsuario);
                    return 1;
                }
                else
                {
                    return database.Insert(obj);
                }
            }
        }

        public int Modificar(IngredienteUsuario obj)
        {
            lock (locker)
            {
                IngredienteUsuario ingredienteUsuario = database.Table<IngredienteUsuario>().FirstOrDefault(i => i._IdIngrediente == obj._IdIngrediente);

                if (ingredienteUsuario != null)
                {

                    ingredienteUsuario._Cantidad = obj._Cantidad;
                    database.Update(ingredienteUsuario);
                    return 1;
                }
                else
                {
                    return 0;
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

        public int Borrar(IngredienteUsuario obj)
        {
            var id = obj._IdIngrediente;
            if (id != 0)
            {
                lock (locker)
                {
                    database.Query<IngredienteUsuario>("Delete FROM IngredienteUsuario WHERE _IdIngrediente = " + id.ToString());
                    return 1;
                }
            }
            return id;
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                   
                database.DropTable<IngredienteUsuario>();
                database.CreateTable<IngredienteUsuario>();
                //return database.DeleteAll<IngredienteUsuario>();
                return 1;
            }
        }
    }
}
