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
            this._IngredientesReceta = r._ListaIngredientesReceta;
            this._Receta = r;
           // CargarDatos(r);
        }

        //public void CargarDatos(Receta r) {

        //    if (_IngredientesReceta.Count != 0)
        //    {
        //        return;
        //    }

        //    foreach (IngredienteReceta ir in r._ListaIngredientesReceta) {

        //        try
        //        {
        //            ir._CantidadMedida = ir._Cantidad + " " + ir._Ingrediente._Medida.ToString();
        //            this._IngredientesReceta.Add(ir);
        //        }
        //        catch(Exception e) {

        //            var msj  = e.Message;

        //        }

        //    }
        //}
    }
}
