using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class MisRecetasVM
    {
        public ObservableCollection<Receta> MisRecetas { get; set; }

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public MisRecetasVM(Usuario usuario)
        {

            Vacio = false;
            Lista = true;
            
            CargarRecetas(usuario);
        }

        public void CargarRecetas(Usuario usuario)
        {
            var recetas = App.DataBase.Receta.ObtenerList().Where(r => r._Email == usuario._Email).ToList();
            
            if (recetas == null)
            {

                Lista = false;
                Vacio = true;
                Text = "Por lo visto no has subido tus propias recetas, animate, es muy facíl!";

            }
            else
            {
                MisRecetas = new ObservableCollection<Receta>(recetas);
            }
        }
    }
}
