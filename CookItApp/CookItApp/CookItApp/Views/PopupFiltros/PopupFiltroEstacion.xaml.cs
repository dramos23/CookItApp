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
            picEstacion.ItemsSource = App.EstacionDataBase.ObtenerList();
        }

        private void Ok_Tapped(object sender, EventArgs e)
        {
            int id = picEstacion.SelectedIndex;
            ViewModel.IngresarFiltroEstacion(id);
            CerrarPopup();
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