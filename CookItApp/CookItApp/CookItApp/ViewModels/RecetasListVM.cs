using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RecetasListVM
    {
        public ObservableCollection<Receta> Recetas { get; set; }

        public RecetasListVM(List<Receta> recetas)
        {


            this.Recetas = new ObservableCollection<Receta>();
            //Aca se tienen que cargar las recetas desde el API. Obviamente, despues se cargarian con filtros.
            if (recetas == null)
            {
                CargarRecetas(null);
            }
            else
            {
                CargarRecetas(recetas);
            }

            CargarDatosPrueba();
        }

        private void CargarDatosPrueba()
        {
            Receta Rec = new Receta
            {
                _Titulo = "Pato a la naranja",
                _Dificultad = 4,
                _PuntajeTotal = 3,
                _IngredientesReceta = new List<CookItWebApi.Models.IngredienteReceta>(),
                _Pasos = new List<PasoReceta>(),
                _IdReceta = 54
            };
            Recetas.Add(Rec);
        }

        private void CargarRecetas(List<Receta> receta)
        {
            List<Receta> recetas = new List<Receta>();

            recetas = receta ?? App.RecetaDataBase.ObtenerList();

            if (recetas != null)
            {
                foreach (Receta r in recetas)
                {
                    Recetas.Add(r);
                }
            }
        }


    }
}
