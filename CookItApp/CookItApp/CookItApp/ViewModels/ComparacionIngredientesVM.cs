using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class ComparacionIngredientesVM
    {
        public Usuario _Usuario;
        public Receta _Receta;
        public ObservableCollection<IngredienteReceta> IngredientesFaltantes;
        public ObservableCollection<IngredienteReceta> IngredientesPocaCantidad;
        public ObservableCollection<IngredienteReceta> IngredientesEnHeladera;


        public ComparacionIngredientesVM(Usuario usr, Receta rec)
        {
            _Usuario = usr;
            _Receta = rec;
            CargarListas();
        }

        private void CargarListas()
        {
            IngredientesFaltantes = new ObservableCollection<IngredienteReceta>();
            IngredientesPocaCantidad = new ObservableCollection<IngredienteReceta>();
            IngredientesEnHeladera = new ObservableCollection<IngredienteReceta>();

            foreach(IngredienteReceta ing in _Receta._ListaIngredientesReceta)
            {
                switch (_Usuario.RevisarDisponibilidadIngrediente(ing))
                {
                    case "No tiene":
                        IngredientesFaltantes.Add(ing);
                        break;
                    case "Faltan":
                        IngredientesPocaCantidad.Add(ing);
                        break;
                    case "Tiene":
                        IngredientesEnHeladera.Add(ing);
                        break;
                }
            }
        }
    }
}
