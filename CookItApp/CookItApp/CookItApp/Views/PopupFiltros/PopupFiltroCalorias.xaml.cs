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
            if (!ValidarCasillas()) return;
            int min = -1;
            if(entMinimo.Text != null && entMinimo.Text.Trim() != "") min = Convert.ToInt32(entMinimo.Text);
            int max = -1;
            if (entMaximo.Text != null && entMaximo.Text.Trim() != "") max = Convert.ToInt32(entMaximo.Text);
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
            ViewModel.IngresarFiltroCalorias(min, max);
            CerrarPopup();
        }
        
        private bool ValidarCasillas()
        {
            var isNumeric = double.TryParse(entMinimo.Text, out double n);
            if (!isNumeric && entMinimo.Text != null && entMinimo.Text != "") {
                MensajeError("Tiene que ingresar un numero en al menos una de las casillas.");
                return false;
            }
            isNumeric = double.TryParse(entMaximo.Text, out double g);
            if (!isNumeric && entMaximo.Text != null && entMaximo.Text != "") {
                MensajeError("Tiene que ingresar un numero en al menos una de las casillas.");
                return false;
            }
            bool hayMinimo = entMinimo.Text != null && entMinimo.Text != "";
            bool hayMaximo = entMaximo.Text != null && entMaximo.Text != "";
            if (!hayMinimo && !hayMaximo)
            {
                MensajeError("Tiene que ingresar un numero en al menos una de las casillas.");
                return false;
            }
            return true;
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            CerrarPopup();
        }

        private async void MensajeError(string msg)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMensaje(ViewModel.Usuario, "Error en tu filtro", msg));
            entMinimo.Text = "";
            entMaximo.Text = "";
        }

        private async void CerrarPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}