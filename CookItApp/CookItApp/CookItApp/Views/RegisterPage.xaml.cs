using CookItApp.Models;
using Microsoft.AppCenter;
using System;

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
		}

        public async void PrcRegistrar(object sender, EventArgs e)
        {
            if (!inpPassword.Text.Equals(inpRetapePassword.Text))
                await DisplayAlert("Registro", "Error: las contraseñas no coinciden.", "Ok");
            else
            {
                System.Guid? uuid = await AppCenter.GetInstallIdAsync();

                if (uuid != null)
                {
                    Usuario user = new Usuario(inpEmail.Text, inpPassword.Text, uuid, Usuario.Tipo.Local, DateTime.Now);
                    if (user.IsValid())
                    {

                        var result = await App.RestService.Registrar(user);
                        if (result._AccessToken != null)
                        {
                            await DisplayAlert("Registro", "Registro Satisfactorio", "Ok");
                            await Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Registro", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                    }
                }
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}