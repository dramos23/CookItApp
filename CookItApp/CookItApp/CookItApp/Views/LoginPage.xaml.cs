using Acr.UserDialogs;
using CookItApp.Controles;
using CookItApp.Models;
using CookItApp.Data;
using CookItApp.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
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
    public partial class LoginPage : ContentPage
    {
        private LoginVM VMLogin { get; set; }
        private Usuario Usuario { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            
            NavigationPage.SetHasNavigationBar(this, false);
            Init();

            VMLogin = new LoginVM();
            BindingContext = VMLogin;
        }

        public void Init()
        {            
            entryEmail.Completed += (s, e) => entryPass.Focus();
            entryPass.Completed += (s, e) => BtnIngresar_Clicked(s, e);

        }
        
        public async void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            BtnIngresar.IsEnabled = false;
            if (entryEmail.Text == null)
            {
                //await DisplayAlert("Login", "Debe ingresar un correo.", "Ok");
                await UserDialogs.Instance.AlertAsync("Login", "Debe ingresar un correo.", "Ok");
                BtnIngresar.IsEnabled = true;
                return;
            }
            else if (entryPass.Text == null)
            {
                await DisplayAlert("Login", "Debe ingresar una contraseña.", "Ok");
                BtnIngresar.IsEnabled = true;
                return;
            }

            UserDialogs.Instance.ShowLoading("Ingresando..");

            Usuario = new Usuario(entryEmail.Text, entryPass.Text, null, DateTime.Now);

            if (Usuario.IsValid())
            {

                Token token = await App.RestService.Login(Usuario);

                if (token != null)
                {
                    if (token._AccessToken != null)
                    {
                        GuardarUsuPas();                                                       

                        App.DataBase.Usuario.Guardar(Usuario);
                        App.DataBase.Token.Guardar(token);
                        UserDialogs.Instance.HideLoading();

                        await Navigation.PushAsync(new CargaRecursos(Usuario, "INS"), true);
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Login", "Error en el servicio.", "Ok");
                }

            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
            }

            BtnIngresar.IsEnabled = true;            
            UserDialogs.Instance.HideLoading();
        }

        private void GuardarUsuPas()
        {
            if (togRecordar.IsToggled)
            {
                Settings.UltimoEmailUsado = entryEmail.Text;
                Settings.UltimaPassUsada = entryPass.Text;
            }
            else
            {
                Settings.UltimoEmailUsado = string.Empty;
                Settings.UltimaPassUsada = string.Empty;
            }
            Settings.UltimoEstadoToggle = togRecordar.IsToggled;
        }

        public async void BtnRecuperar_Clicked(object sender, EventArgs e)
        {

            await PopupNavigation.Instance.PushAsync(new PopupRestablecerContrasenaPage());

        }

        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void FacebookLogin_Clicked(object sender, EventArgs e)
        {           

            await Navigation.PushAsync(new FBLoginPage(), true);
            Navigation.RemovePage(this);
        }
    }
}