using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class RecetaListVM
    {
        public ObservableCollection<Receta> Recetas { get; set; }

        public RecetaListVM(List<Receta> filtrada)
        {

            if (filtrada != null)
            {
                CargarRecetasF(filtrada);
            }
            else { CargarRecetas(); }
                                  
        }

        private void CargarRecetasF(List<Receta> filtrada)
        {
            Recetas = new ObservableCollection<Receta>(filtrada);
        }

        private void CargarRecetas()
        {

            var recetas = App.DataBase.Receta.ObtenerList();
            recetas = recetas.Where(r => r._Habilitada == true).ToList();

            if (recetas.Count > 0)
            {

                Recetas = new ObservableCollection<Receta>(recetas);

            }
            else {

                Recetas = new ObservableCollection<Receta>();

            }
        }

        public List<Receta> FiltrarRecetasPorNombre(string keyword)
        {
            List<Receta> recetas = App.DataBase.Receta.ObtenerList();
            recetas = recetas.Where(r => r._Habilitada == true).ToList();
            recetas = recetas.Where(c => c._Titulo.ToLower().Contains(keyword.ToLower())).ToList();
            return recetas;
        }

        internal List<Receta> DevolverListaFiltrada(List<Receta> recetas)
        {
            var vm = new object();
            Application.Current.Properties.TryGetValue("ViewModelFiltro", out vm);
            FiltrosVM filtros = vm as FiltrosVM;

            return filtros.AplicarFiltrosALista(recetas);
        }
    }
}
