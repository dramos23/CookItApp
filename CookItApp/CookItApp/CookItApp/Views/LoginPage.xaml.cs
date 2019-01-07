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
	public partial class LoginPage : ContentPage
	{
        private Usuario Usuario { get; set; }

		public LoginPage ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
            Init();
		}

        public void Init() {

            entryEmail.Completed += (s, e) => entryPass.Focus();
            entryPass.Completed += (s, e) => PrcIngresar(s, e);
            BtnRegistrar();
        }

        public async void PrcIngresar(object sender, EventArgs e )
        {
            btnIngresar.IsEnabled = false;
            if(entryEmail.Text == null)
            {
                await DisplayAlert("Login", "Debe ingresar un correo.", "Ok");
                btnIngresar.IsEnabled = true;
                return;
            }else if(entryPass.Text == null)
            {
                await DisplayAlert("Login", "Debe ingresar una contraseña.", "Ok");
                btnIngresar.IsEnabled = true;
                return;
            }

            Usuario = new Usuario(entryEmail.Text, entryPass.Text);
            
            if (Usuario.IsValid())
            {

                Token token = await App.RestService.Login(Usuario);
               
                if (token._AccessToken != null)
                {                    

                    await DisplayAlert("Login", "Ingreso Satisfactorio", "Ok");

                    App.UsuarioDatabase.Guardar(Usuario);
                    App.TokenDatabase.Guardar(token);

                    //bool Continuar = await CargaDatosAplicativo(Usuario, token);

                    //if (Continuar) { 

                    //    if (Usuario._Perfil != null)
                    //    {

                    //        await Navigation.PushAsync(new MasterPage(Usuario), true);                         
                    //        Navigation.RemovePage(this);

                    //    }
                    //    else
                    //    {

                    //        var action = await DisplayAlert("Complete su perfil", "Desea completar su perfil ahora?", "Ahora No", "Bueno");
                    //        if (!action)
                    //        {


                    //            await Navigation.PushAsync(new PerfilPage(Usuario), true);                           
                    //            Navigation.RemovePage(this);
                    //        }
                    //        else {
                    //            await Navigation.PushAsync(new MasterPage(Usuario), true);
                    //            Navigation.RemovePage(this);
                    //        }

                    //    }

                    //}
                    await Navigation.PushAsync(new CargaRecursos(Usuario, "INS"), true);
                    Navigation.RemovePage(this);

                }
                else
                {
                    await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                }

            }
            else
            {
                await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");                
            }

            btnIngresar.IsEnabled = true;
        }

        public void BtnRegistrar() {

            //Navigation.PushAsync(new RegisterPage());
            lblRegistro.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                    Navigation.PushAsync(new RegisterPage())
                )
            });

        }

        public async Task<bool> CargaDatosAplicativo(Usuario usuario, Token token)
        {

            bool retorno = false;

            App.UsuarioDatabase.Guardar(usuario);
            App.TokenDatabase.Guardar(token);

            Perfil perfil = await App.PerfilService.Obtener(usuario);
            if (perfil != null)
            {
                App.PerfilDataBase.Guardar(perfil);
            }
            usuario._Perfil = perfil ?? null;

            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                App.MomentoDiaDataBase.GuardarList(momentos);
            }
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                App.EstacionDataBase.GuardarList(estaciones);
            }
            if (momentos != null && estaciones != null)
            {
                List<Receta> recetas = await App.RecetaService.ObtenerList();
                if (recetas != null)
                {
                    App.RecetaDataBase.GuardarList(recetas);
                    retorno = true;
                }
            }

            return retorno;
        }

    }
}