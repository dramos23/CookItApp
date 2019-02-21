using Acr.UserDialogs;
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
        private HistorialRecetasListVM _VMHistorialRecetas { get; set; }
        private Usuario _Usuario { get; set; }


        public HistorialRecetasPage(Usuario Usuario)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            _VMHistorialRecetas = new HistorialRecetasListVM(_Usuario);
            BindingContext = _VMHistorialRecetas;
        }

        


        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {
            UserDialogs.Instance.ShowLoading("Cargando receta...");
            if (!(args.SelectedItem is HistorialReceta historialReceta))
            {
                return;
            }


            Receta receta = App.DataBase.Receta.Obtener(historialReceta._IdReceta);

            if (receta != null)
            {
                UserDialogs.Instance.HideLoading();
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(receta, _Usuario));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(_Usuario, "Error", "Ha ocurrido un error, vuelva a intentarlo."));

            }
        }
    }
}