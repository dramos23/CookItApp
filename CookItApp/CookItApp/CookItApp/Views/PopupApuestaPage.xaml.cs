using Acr.UserDialogs;
using CookItApp.Models;
using CookItApp.ViewModels;
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
	public partial class PopupApuestaPage
	{
        private PopupApuestaVM VMPopupApuesta { get; set; }

        private Usuario _Usuario { get; set; }

        private Receta _Receta { get; set; }

        private List<Perfil> _ListPerfiles { get; set; }

        private Perfil _UsuarioSelected { get; set; }

        public PopupApuestaPage (Usuario Usuario, Receta Receta, List<Perfil> Perfiles)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            _Receta = Receta;
            _ListPerfiles = Perfiles;
            VMPopupApuesta = new PopupApuestaVM(_ListPerfiles);
            BindingContext = VMPopupApuesta;
        }

        private void UsuarioOrigen_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = UsuarioOrigen.Text;
            if (keyword != "")
            {
                List<Perfil> resultado = _ListPerfiles;
                resultado = resultado.Where(c => c._NombreUsuario.ToLower().Contains(keyword.ToLower())).ToList();
               
                ListPerfiles.ItemsSource = resultado;

            }
            else {

                ListPerfiles.IsVisible = false;

            }


        }

        private async void RetarUsuario_Clicked(object sender, EventArgs e)
        {           

            UserDialogs.Instance.ShowLoading("Procesando..");

            Reto reto = new Reto
            {
                _EmailUsuOri = _Usuario._Email,
                _NomUsuOri = _Usuario._Perfil._NombreUsuario,   
                _ComentarioOrigen = ComentaioOrigen.Text,
                _EmailUsuDes = _UsuarioSelected._Email,
                _NomUsuDes = _UsuarioSelected._NombreUsuario,
                _RecetaId = _Receta._IdReceta,
                _Fecha = DateTime.Now,
                _IdEstadoReto = 1,
                _Puntaje = Convert.ToInt32(Math.Round(_Receta._Dificultad + _Receta._PuntajeTotal, 0))
            };

            if (reto != null) {
                
                int idReto = await App.RetoService.Alta(reto);
                reto._IdReto = idReto;

                if (reto != null)
                {

                    App.DataBase.Reto.Guardar(reto);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Reto", "Reto enviado...", "OK");

                }
                else {

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Reto", "Aún matiene un reto pendiente con el usuario.", "OK");

                }

                await PopupNavigation.Instance.PopAsync();

            }

        }

        private void ListPerfiles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Perfil p = (Perfil)e.SelectedItem;
            UsuarioOrigen.Text = p._NombreUsuario;
            _UsuarioSelected = p;

            ((ListView)sender).SelectedItem = null;
            ListPerfiles.ItemsSource = null;
            ListPerfiles.IsVisible = false;
            
        }
    }
}