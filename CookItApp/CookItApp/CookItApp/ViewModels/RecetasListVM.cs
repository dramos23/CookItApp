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

            if (filtrada != null && filtrada.Count > 0)
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

            if (recetas.Count > 0)
            {

                Recetas = new ObservableCollection<Receta>(recetas);

            }
            else {

                Recetas = new ObservableCollection<Receta>();

            }
            //List<Receta> recetas = new List<Receta>();

            //recetas = receta ?? App.DataBase.Receta.ObtenerList();

            //if (recetas != null)
            //{
            //    foreach (Receta r in recetas)
            //    {
            //        Recetas.Add(r);
            //    }
            //}
        }

        public List<Receta> FiltrarRecetasPorNombre(string keyword)
        {
            return App.DataBase.Receta.ObtenerList().Where(c => c._Titulo.ToLower().Contains(keyword.ToLower())).ToList();
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
