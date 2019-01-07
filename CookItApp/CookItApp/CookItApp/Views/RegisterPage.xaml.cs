using CookItApp.Models;
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
                Usuario user = new Usuario(inpEmail.Text, inpPassword.Text);
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}