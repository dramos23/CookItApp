using Acr.UserDialogs;
using CookItApp.Controles;
using CookItApp.Models;
using Microsoft.AppCenter;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
            Init();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        public void Init()
        {

            entryEmail.Completed += (s, e) => entryPass.Focus();
            entryPass.Completed += (s, e) => entryRePass.Focus();
            entryRePass.Completed += (s, e) => BtnRegistrar_Clicked(s, e);            

        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Validando datos..");

            if (await ValidarDatos())
            {

                if (!entryPass.Text.Equals(entryRePass.Text))
                    await DisplayAlert("Registro", "Error: las contraseñas no coinciden.", "Ok");
                else
                {
                    UserDialogs.Instance.ShowLoading("Registrando..");

                    System.Guid? uuid = await AppCenter.GetInstallIdAsync();

                    if (uuid != null)
                    {
                        Usuario user = new Usuario(entryEmail.Text, entryEmail.Text, entryPass.Text, uuid, Usuario.TipoCuenta.Local, Usuario.TipoUsuario.Cliente, DateTime.Now);
                        if (user.IsValid())
                        {

                            var result = await App.RestService.Registrar(user);
                            if (result._AccessToken != null)
                            {
                                GuardarUsuPas();
                                UserDialogs.Instance.HideLoading();
                                await DisplayAlert("Registro", "Registro Satisfactorio", "Continuar");
                                await Navigation.PopAsync();
                            }
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Registro", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                        }
                    }
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task<bool> ValidarDatos()
        {
            if (ValidarEmail(entryEmail.Text))
            {
                if (!ValidarContraseña(entryPass.Text))
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
            else {

                string msj = "Error en el formato del email ingresado.";

                await DisplayAlert("Formato de email incorrecto", msj, "Continuar");

                return false;
            }
        }

        private void GuardarUsuPas()
        {
            if (togRecordar.IsEnabled)
            {
                Settings.UltimoEmailUsado = entryEmail.Text;
                Settings.UltimaPassUsada = entryPass.Text;
            }
            else
            {
                Settings.UltimoEmailUsado = string.Empty;
                Settings.UltimaPassUsada = string.Empty;
            }
        }

        public bool ValidarContraseña(string value)
        {
            if (value == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^.*(?=.{10,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            Match match = regex.Match(value);

            return match.Success;
        }

        public bool ValidarEmail(string value)
        {
            if (value == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");
            Match match = regex.Match(value);

            return match.Success;
        }

        private void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}