using CookItApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookItApp.Data
{
    public class RecetaDataBaseController
    {
        static readonly object locker = new object();

        SQLite.SQLiteConnection database;

        public RecetaDataBaseController()
        {
            database = DependencyService.Get<ISQLIte>().GetConnection();
            database.CreateTable<Receta>();
        }

        public List<Receta> ObtenerList()
        {
            lock (locker)
            {
                if (database.Table<Receta>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Receta>().ToList();
                }
            }
        }

        public Receta Obtener(int idReceta)
        {
            lock (locker)
            {
                if (database.Table<Receta>().Count() == 0)
                {
                    return null;
                }
                else
                {

                    Receta receta = database.Table<Receta>().FirstOrDefault(r => r._IdReceta == idReceta);
                    //List<IngredienteReceta> ingredienteRecetas = App.DataBase.IngredienteReceta.ObtenerList(idReceta);
                    //List<ComentarioReceta> comentarioRecetas = App.DataBase.ComentarioReceta.ObtenerList(idReceta);
                    //List<PasoReceta> pasoRecetas = App.DataBase.PasoReceta.ObtenerList(idReceta);

                    //receta._ListaIngredientesReceta = ingredienteRecetas ?? null;
                    //receta._ListaComentariosReceta = comentarioRecetas ?? null;
                    //receta._ListaPasosReceta = pasoRecetas ?? null;

                    return receta;
                }
            }
        }

        public void GuardarList(List<Receta> Receta)
        {            
            lock (locker) {

                
                foreach(Receta r in Receta){

                    database.Insert(r);
                    r.InsertarBD();

                }
            }
        }

        public int BorrarTodo()
        {
            lock (locker)
            {
                return database.DeleteAll<Receta>();
            }
        }
    }
}
