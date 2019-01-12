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
	public partial class PopupFiltroCalorias : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroCalorias (FiltrosVM viewmodel)
		{
			InitializeComponent ();
            ViewModel = viewmodel;
		}

        private void Ok_Tapped(object sender, EventArgs e)
        {
            int min = -1;
            if(entMinimo.Text.Trim() != "") min = Convert.ToInt32(entMinimo.Text);
            int max = -1;
            if (entMaximo.Text.Trim() != "") max = Convert.ToInt32(entMaximo.Text);
            ViewModel.IngresarFiltroCalorias(min, max);
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