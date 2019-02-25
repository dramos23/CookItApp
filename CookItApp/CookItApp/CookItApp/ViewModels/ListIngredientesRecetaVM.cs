using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class ListIngredientesRecetaVM
    {
        public List<IngredienteReceta> IngredientesReceta { get; set; }

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public ListIngredientesRecetaVM(List<IngredienteReceta> ingredientesRecetas) {

            Vacio = false;
            Lista = true;

            CargarIngredientesReceta(ingredientesRecetas);

        }

        private void CargarIngredientesReceta(List<IngredienteReceta> ingredientesRecetas)
        {
            if (ingredientesRecetas != null && ingredientesRecetas.Count > 0)
            {

                IngredientesReceta = new List<IngredienteReceta>(ingredientesRecetas);

            }
            else
            {
                IngredientesReceta = new List<IngredienteReceta>();

                Lista = false;
                Vacio = true;
                Text = "Vamos ya casí que terminas faltan añadir ingredientes a la receta!.";
            }
        }

        public void AgregarIngRec(IngredienteReceta ingredienteReceta)
        {
            IngredientesReceta.Add(ingredienteReceta);

            Lista = true;
            Vacio = false;
        }

        public void ActualizarIngRec(IngredienteReceta ingredientesReceta)
        {            
            
            foreach(IngredienteReceta ir in IngredientesReceta)
            {
                if (ir._IdIngrediente == ingredientesReceta._IdIngrediente)
                {
                    ir._Medida = ingredientesReceta._Medida;
                    ir._Cantidad = ingredientesReceta._Cantidad;
                }
            }

        }

        public void EliminarIngRec(IngredienteReceta ingredienteReceta)
        {
            int index = IngredientesReceta.FindIndex(i => i._IdIngrediente == ingredienteReceta._IdIngrediente);            
            IngredientesReceta.RemoveAt(index);

            if (IngredientesReceta.Count == 0)
            {
                Lista = false;
                Vacio = true;
            }

        }
    }
}
