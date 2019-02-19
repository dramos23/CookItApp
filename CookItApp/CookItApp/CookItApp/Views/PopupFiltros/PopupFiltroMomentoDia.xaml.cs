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
	public partial class PopupFiltroMomentoDia : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroMomentoDia (FiltrosVM viewmodel)
		{
			InitializeComponent ();
            CargarPickerMomentoDia();
            ViewModel = viewmodel;
		}

        private void CargarPickerMomentoDia()
        {
            picMomentoDia.ItemsSource = App.DataBase.MomentoDia.ObtenerList();
        }

        private async void Ok_Tapped(object sender, EventArgs e)
        {
            try
            {
                int id = picMomentoDia.SelectedIndex;
                if (picMomentoDia.SelectedIndex == -1) throw new Exception("Tienes que seleccionar una opcion.");
                ViewModel.IngresarFiltroMomentoDia(id);
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