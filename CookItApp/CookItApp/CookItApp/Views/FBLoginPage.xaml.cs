using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
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
	public partial class FBLoginPage : ContentPage
	{
        private FBLoginVM VMFBLogin { get; set; }        

		public FBLoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            VMFBLogin = new FBLoginVM();

        }

        //public event EventHandler ItemAdded;

        //public void RaiseItemAdded()
        //{
        //    ItemAdded(this, EventArgs.Empty);
        //}

        public async void Procesar(UsuarioFacebook UsuarioFacebook)
        {            

            Usuario usuario = await VMFBLogin.Ingresar(UsuarioFacebook);

            if (usuario != null)
            {

                await Navigation.PushAsync(new CargaRecursos(usuario, "INS"), true);
                Navigation.RemovePage(this);

            }
            else {

                await UserDialogs.Instance.AlertAsync("Ha ocurrido un error vuelve a intentarlo!.", "Error!", "Continuar");
                new NavigationPage(new LoginPage());

            }

        }
    }
}