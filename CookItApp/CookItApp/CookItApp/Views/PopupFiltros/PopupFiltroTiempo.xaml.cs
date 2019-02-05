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
	public partial class PopupFiltroTiempo : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroTiempo (FiltrosVM viewmodel)
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
            if (min != -1 && max != -1)
            {
                if (min > max)
                {
                    MensajeError("El valor minimo no puede ser mayor al maximo.");
                    return;
                }
                if (max < min)
                {
                    MensajeError("El valor maximo no puede ser menor al minimo.");
                    return;
                }
            }
            ViewModel.IngresarFiltroTiempoPreparacion(min, max);
            CerrarPopup();
        }

        private async void MensajeError(string msg)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMensaje(ViewModel.Usuario, "Error en tu filtro", msg));
            entMinimo.Text = "";
            entMaximo.Text = "";
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