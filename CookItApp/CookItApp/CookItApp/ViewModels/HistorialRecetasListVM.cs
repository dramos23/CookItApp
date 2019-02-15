using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class HistorialRecetasListVM
    {
        public ObservableCollection<HistorialReceta> HistorialRecetas { get; set; }    

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public HistorialRecetasListVM(Usuario usuario)
        {

            Vacio = false;
            Lista = true;
            
            //Aca se tienen que cargar las recetas desde el API. Obviamente, despues se cargarian con filtros.
            CargarRecetas(usuario);
            


        }

        private void CargarRecetas(Usuario usuario)
        {
            var historiales = App.DataBase.HistorialReceta.ObtenerList();

            //if (historiales != null)
            //{
            //    foreach (HistorialReceta h in historiales)
            //    {
            //        h._Receta = App.DataBase.Receta.Obtener(h._IdReceta);
            //        HistorialRecetas.Add(h);                    
            //    }
            //    Lista = true;
            //}
            //else
            //{
            //    Vacio = true;
            //    Text = "Aún no tiene nada en su historial!.";
            //}

            if (historiales == null) {

                Lista = false;
                Vacio = true;
                Text = "Aún no tiene nada en su historial!.";

            }
            else
            {
                HistorialRecetas = new ObservableCollection<HistorialReceta>(historiales);
            }
        }


    }
}
