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
        HistorialRecetasListVM _VMHistorialRecetas;
        Usuario Usuario;


        public HistorialRecetasPage(Usuario Usuario)
		{
			InitializeComponent ();
            this.Usuario = Usuario;
            _VMHistorialRecetas = new HistorialRecetasListVM(Usuario);
            BindingContext = _VMHistorialRecetas;
        }



        
        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {            
            if (!(args.SelectedItem is Receta receta))
            {
                return;
            }

            Receta rec = await App.RecetaService.Obtener(receta);

            if (rec != null)
            {
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(rec, Usuario));

                ListaRecetasVisitadas.SelectedItem = null;
            }
        }

    }
}