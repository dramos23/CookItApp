using Acr.UserDialogs;
using CookItApp.Controles;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupCambiarContraseñaPage : PopupPage
	{
        private PopupCambioContraseñaVM VMCambio { get; set; }

        public PopupCambiarContraseñaPage (Usuario usuario)
		{
			InitializeComponent ();
            VMCambio = new PopupCambioContraseñaVM(usuario);
            BindingContext = VMCambio;

        }

        private async void BtnCambiar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cambiando..");

            if (await ValidarDatos())
            {

                Dictionary<string, string> cambio = new Dictionary<string, string>
                {
                    { "IdUsuario", entryEmail.Text },
                    { "Email", entryEmail.Text },
                    { "ContAnterior", entryContActual.Text },
                    { "ContNueva", entryContNueva.Text }
                };


                var resultado = await App.RestService.CambiarCont(cambio);

                if (resultado)
                {
                    ModificarContraseñaAlmacenada(entryContNueva.Text);

                    UserDialogs.Instance.HideLoading();

                    await DisplayAlert("Cambiar", "Modificación efectuada!.", "Continuar");

                    await PopupNavigation.Instance.PopAsync();

                }
                else
                {
                    UserDialogs.Instance.HideLoading();

                    await DisplayAlert("Error: Cambiar", "Se produjo un error al intentar cambiar su contraseña.", "Continuar");
                }
            }

            UserDialogs.Instance.HideLoading();
        }

        private async Task<bool> ValidarDatos()
        {
                if (!ValidarContraseña(entryContNueva.Text))
                {
                    string msj = "La contraseña debe contener:\\n" +
                                 "- Al menos una letra minúscula,\\n" +
                                 "- Al menos una letra mayuscula,\\n" +
                                 "- Al menos un carácter especial,\\n" +
                                 "- Al menos un número,\\n" +
                                 "- Al menos 10 caracteres de longitud";

                    msj = (msj as string).Replace("\\n", Environment.NewLine + Environment.NewLine);

                    await DisplayAlert("Formato de contraseña incorrecto", msj, "Continuar");

                    return false;
                }
                else
                {
                    return true;
                }
            
        }

        public bool ValidarContraseña(string value)
        {
            if (value == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*.@#$%^&+=]).*$");
            Match match = regex.Match(value);

            return match.Success;
        }

        private void ModificarContraseñaAlmacenada(string nuevaCont)
        {
            if (Settings.UltimoEstadoToggle)
            {
                Settings.UltimaPassUsada = nuevaCont;
            }
        }


    }
}