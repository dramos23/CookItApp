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

        public void GuardarList(List<Receta> Receta)
        {            
            lock (locker) {

                
                foreach(Receta r in Receta){

                    Receta receta = new Receta()
                    {
                        _IdReceta = r._IdReceta,
                        _Titulo = r._Titulo,
                        _Descripcion = r._Descripcion,
                        _IdMomentoDia = r._IdMomentoDia,
                        _IdEstacion = r._IdEstacion,
                        _Dificultad = r._Dificultad,
                        _TiempoPreparacion = r._TiempoPreparacion,
                        _CantCalorias = r._CantCalorias,
                        _Email = r._Email,
                        _CantPlatos = r._CantPlatos,
                        _Costo = r._Costo,
                        _FechaCreacion = r._FechaCreacion,
                        _PuntajeTotal = r._PuntajeTotal,
                        _AptoCeliacos = r._AptoCeliacos,
                        _AptoDiabeticos = r._AptoDiabeticos,
                        _AptoVegetarianos = r._AptoVegetarianos,
                        _AptoVeganos = r._AptoVeganos,
                    };

                    database.Insert(receta);

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
