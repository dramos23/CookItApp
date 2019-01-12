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
            picMomentoDia.ItemsSource = App.MomentoDiaDataBase.ObtenerList();
        }

        private void Ok_Tapped(object sender, EventArgs e)
        {
            int id = picMomentoDia.SelectedIndex;
            ViewModel.IngresarFiltroMomentoDia(id);
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