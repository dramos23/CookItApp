using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CookItApp.ViewModels
{
    public class IngredienteRecetaVM
    {        

        public ObservableCollection<IngredienteReceta> IngredientesRecetas { get; set; }     

        public IngredienteRecetaVM(int idReceta)
        {
            CargarIngredientesReceta(idReceta);
        }

        private void CargarIngredientesReceta(int idReceta)
        {
            List<IngredienteReceta> ingredienteRecetas = App.DataBase.IngredienteReceta.ObtenerList(idReceta);

            if (ingredienteRecetas != null)
            {

                IngredientesRecetas = new ObservableCollection<IngredienteReceta>(ingredienteRecetas);

            }
            else {

                IngredientesRecetas = new ObservableCollection<IngredienteReceta>();

            }
        }
    }
}