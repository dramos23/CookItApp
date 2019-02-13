using CookItApp.Models;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class SupermercadoListVM
    {
        public ObservableCollection<Supermercado> SuperList { get; set; }

        public bool DistVis { get; set; }

        public SupermercadoListVM(Position position) {

            DistVis = false;
            CargarLocales(position);

        }

        private void CargarLocales(Position position)
        {
            List<Supermercado> supermercados = App.DataBase.Supermercado.ObtenerList();

            if (position == null)
            {
                SuperList = new ObservableCollection<Supermercado>(supermercados);
            }
            else
            {
                DistVis = true;
                foreach (Supermercado s in supermercados)
                {
                    s.CalcularDistancia(position);                    
                }
                supermercados = supermercados.Where(s => s._DistValor < 1000).ToList();
                SuperList = new ObservableCollection<Supermercado>(supermercados);

            }
        }
    }
}
