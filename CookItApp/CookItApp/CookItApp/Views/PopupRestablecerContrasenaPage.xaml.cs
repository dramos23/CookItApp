using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
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
	public partial class PopupRestablecerContrasenaPage : PopupPage
	{
		public PopupRestablecerContrasenaPage ()
		{
			InitializeComponent ();
		}

        private async void BtnRestablecer_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Restableciendo..");

            string idUsuario = entryEmail.Text;

            var resultado = App.RestService.RestablecerContraseña(idUsuario);

            if (resultado.Result) {

                UserDialogs.Instance.HideLoading();

                await DisplayAlert("Restablecer", "Se generado y enviado una nueva contraseña a su email", "Continuar");

                await PopupNavigation.Instance.PopAsync();

            }
            else
            {
                UserDialogs.Instance.HideLoading();

                await DisplayAlert("Error: Restablecer", "Se produjo un error al intentar restablecer su contraseña.", "Continuar");
            }

        }
    }
}