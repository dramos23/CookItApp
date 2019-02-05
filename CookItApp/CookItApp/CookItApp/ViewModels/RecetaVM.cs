using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RecetaVM
    {
        public Receta _Receta { get; set; }
        public List<IngredienteReceta> _IngredientesReceta { get; set; }

        public RecetaVM(Receta r)
        {
            _IngredientesReceta = r._ListaIngredientesReceta;
            _Receta = r;
            CargarDato();
            _Receta.OrdenarListasReceta();
            string test = "";
        }

        public void CargarDato()
        {
            foreach (IngredienteReceta ir in _IngredientesReceta)
            {
                ir.CantidadMedida();
            }
        }
    }
}
