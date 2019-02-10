using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RecetaListVM
    {
        public ObservableCollection<Receta> Recetas { get; set; }

        public RecetaListVM(List<Receta> recetas)
        {


            
            //Aca se tienen que cargar las recetas desde el API. Obviamente, despues se cargarian con filtros.
            if (recetas == null)
            {
                this.Recetas = new ObservableCollection<Receta>();
                CargarRecetas(null);
            }
            else
            {
                this.Recetas = new ObservableCollection<Receta>(recetas);
            }

           
        }

        private void CargarRecetas(List<Receta> receta)
        {
            List<Receta> recetas = new List<Receta>();

            recetas = receta ?? App.DataBase.Receta.ObtenerList();

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
