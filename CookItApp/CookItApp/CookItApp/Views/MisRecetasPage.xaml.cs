using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MisRecetasPage : ContentPage, IViewMisRecetas
	{
        public MisRecetasVM VMMisRecetas { get; set; }
        private Usuario Usuario { get; set; }

        public MisRecetasPage (Usuario usuario)
		{
            InitializeComponent();

            Usuario = usuario;

            VMMisRecetas = new MisRecetasVM(Usuario);
            BindingContext = VMMisRecetas;
            
		}

        public void Actualizar()
        {
            VMMisRecetas = new MisRecetasVM(Usuario);
            BindingContext = VMMisRecetas;
        }

        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {
            UserDialogs.Instance.ShowLoading("Cargando receta...");
            if (!(args.SelectedItem is Receta receta))
            {
                return;
            }
            

            if (receta != null)
            {
                UserDialogs.Instance.HideLoading();
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(receta, Usuario));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Error", "Ha ocurrido un error, vuelva a intentarlo."));

            }
        }

        private async void NuevaReceta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevaRecetaPage(Usuario, this));
        }
    }
}