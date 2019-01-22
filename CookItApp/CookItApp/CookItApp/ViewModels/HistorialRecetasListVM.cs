using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class HistorialRecetasListVM
    {
        public List<HistorialReceta> HistorialRecetas { get; set; }

        public HistorialRecetasListVM(Usuario usuario)
        {


            HistorialRecetas = new List<HistorialReceta>();
            //Aca se tienen que cargar las recetas desde el API. Obviamente, despues se cargarian con filtros.
            CargarRecetas(usuario);
            


        }

        private void CargarRecetas(Usuario usuario)
        {
            HistorialRecetas = App.DataBase.HistorialReceta.ObtenerList(usuario);

            if (HistorialRecetas != null)
            {
                foreach (HistorialReceta h in HistorialRecetas)
                {
                    h._Receta = App.DataBase.Receta.Obtener(h._IdReceta);
                }
            }
        }


    }
}
