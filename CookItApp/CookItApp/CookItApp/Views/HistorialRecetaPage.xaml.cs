using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistorialRecetasPage : ContentPage
	{
        public HistorialRecetasListVM _VMHistorialRecetas { get; set; }
        public Usuario _Usuario { get; set; }


        public HistorialRecetasPage(Usuario Usuario)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            _VMHistorialRecetas = new HistorialRecetasListVM(_Usuario);
            BindingContext = _VMHistorialRecetas;
        }



        
        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {            
            if (!(args.SelectedItem is HistorialReceta historialReceta))
            {
                return;
            }
            

            Receta rec = await App.RecetaService.Obtener(historialReceta._Receta);

            if (rec != null)
            {
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(rec, _Usuario));
                
            }
        }

    }
}