using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class NuevoIngRecVM
    {
        public string Titulo { get; set; }
        public int Cantidad { get; set; }
        public int IngredienteID { get; set; }
        public string Medida { get; set; }
        public List<Ingrediente> ItemsIngrediente { get; set; }
        public IngredienteReceta IngredienteReceta { get; set; }
        public bool HabIng { get; set; }

        public NuevoIngRecVM(IngredienteReceta ingredienteReceta) {

            HabIng = true;
            CargarPicker();
            CargarDatos(ingredienteReceta);
            Titulo = (ingredienteReceta != null) ? "Modificando Ing. Rec." : "Ingresando Ing. Rec.";
            IngRecPorDefecto(ingredienteReceta);
        }

        private void IngRecPorDefecto(IngredienteReceta ingredienteReceta)
        {
            if (ingredienteReceta == null)
            {
                IngredienteReceta = new IngredienteReceta()
                {
                    
                    _IdIngrediente = ItemsIngrediente[0]._IdIngrediente,
                    _Medida = ItemsIngrediente[0]._Medida,
                    _Cantidad = 0,

                };
            }
        }

        private void CargarPicker()
        {
            ItemsIngrediente = App.DataBase.Ingrediente.ObtenerList();
            Medida = ItemsIngrediente[0]._Medida.ToString();            
        }

        private void CargarDatos(IngredienteReceta ingrediente)
        {
            if (ingrediente != null) {

                IngredienteReceta = ingrediente;

                int index = BuscarIndexPicker(ingrediente._IdIngrediente);

                Cantidad = ingrediente._Cantidad;
                IngredienteID = index;
                Medida = ingrediente._Medida.ToString();

                HabIng = false;
            }
        }

        private int BuscarIndexPicker(int idIngrediente)
        {
            return ItemsIngrediente.FindIndex(i => i._IdIngrediente == idIngrediente);
        }

        internal void CargarIngRec(Ingrediente ingrediente, int cantidad, Ingrediente.TipoMedida medida)
        {
            IngredienteReceta._IdIngrediente = ingrediente._IdIngrediente;
            IngredienteReceta._Cantidad = cantidad;
            IngredienteReceta._Medida = medida;
            IngredienteReceta._Ingrediente = ingrediente;
        }
    }
}
