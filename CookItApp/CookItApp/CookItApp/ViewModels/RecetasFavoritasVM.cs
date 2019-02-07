using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RecetasFavoritasVM
    {

        public ObservableCollection<RecetaFavorita> RecetasFavoritas { get; set; }

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public RecetasFavoritasVM(Usuario usuario)
        {

            Vacio = false;
            Lista = false;
            RecetasFavoritas = new ObservableCollection<RecetaFavorita>();
            
            CargarRecetas(usuario);



        }

        private void CargarRecetas(Usuario usuario)
        {
            List<RecetaFavorita> favoritas = App.DataBase.RecetaFavorita.ObtenerList();

            if (favoritas != null)
            {
                foreach (RecetaFavorita f in favoritas)
                {
                    f._Receta = App.DataBase.Receta.Obtener(f._IdReceta);
                    RecetasFavoritas.Add(f);
                }
                Lista = true;
            }
            else
            {
                Vacio = true;
                Text = "Aún no tiene recetas favoritas!.";
            }
        }

    }
}
