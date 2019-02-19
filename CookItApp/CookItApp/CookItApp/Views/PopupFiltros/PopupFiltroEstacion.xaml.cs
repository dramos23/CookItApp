using CookItApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views.PopupFiltros
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupFiltroEstacion : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroEstacion (FiltrosVM viewmodel)
		{
			InitializeComponent ();
            CargarPickerEstacion();
            ViewModel = viewmodel;
		}

        private void CargarPickerEstacion()
        {
            picEstacion.ItemsSource = App.DataBase.Estacion.ObtenerList();
        }

        private async void Ok_Tapped(object sender, EventArgs e)
        {
            try
            {
                int id = picEstacion.SelectedIndex;
                if (picEstacion.SelectedIndex == -1) throw new Exception("Tienes que seleccionar una opcion.");
                ViewModel.IngresarFiltroEstacion(id);
                CerrarPopup();
            }catch(Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(ViewModel.Usuario, "Error en filtro", ex.Message));
            }
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            CerrarPopup();
        }

        private async void CerrarPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}