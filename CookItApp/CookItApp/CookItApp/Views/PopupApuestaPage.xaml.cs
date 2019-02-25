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
            List<Perfil> resultado = new List<Perfil>(VMPopupApuesta.Perfiles);

            string keyword = UsuarioOrigen.Text;
            if (keyword != "")
            {
                
                resultado = resultado.Where(c => c._NombreUsuario.ToLower().Contains(keyword.ToLower())).ToList();               
                ListPerfiles.ItemsSource = resultado;

            }
            else {
                
                ListPerfiles.ItemsSource = resultado;

            }


        }

        private async void RetarUsuario_Clicked(object sender, EventArgs e)
        {
            if (await ValidarDatos())
            {

                UserDialogs.Instance.ShowLoading("Procesando..");

                Reto reto = new Reto
                {
                    _EmailUsuOri = _Usuario._Email,
                    _NomUsuOri = _Usuario._Perfil._NombreUsuario,
                    _ComentarioOrigen = ComentaioOrigen.Text,
                    _EmailUsuDes = _UsuarioSelected._Email.Trim(),
                    _NomUsuDes = _UsuarioSelected._NombreUsuario.Trim(),
                    _RecetaId = _Receta._IdReceta,
                    _Receta = _Receta,
                    _Fecha = DateTime.Now,
                    _IdEstadoReto = 1,
                    _Puntaje = Convert.ToInt32(Math.Round(_Receta._Dificultad + _Receta._PuntajeTotal, 0))
                };

                int estado = await VMPopupApuesta.CrearReto(reto);

                UserDialogs.Instance.HideLoading();

                if (estado == -1)
                {
                    await UserDialogs.Instance.AlertAsync("Aún matienes un reto pendiente con el usuario o ya los has desafiado a realizar está receta.", "Atención!", "Continuar");
                }

                if (estado == 0)
                {
                    await UserDialogs.Instance.AlertAsync("Error interno, reinicia la aplicación.", "Error!", "Continuar");
                }

                if (estado == 1)
                {
                    await UserDialogs.Instance.AlertAsync("Reto enviado...", "Reto", "Continuar");
                }

                await PopupNavigation.Instance.PopAsync();
            }

        }

        private async Task<bool> ValidarDatos()
        {

            if (_UsuarioSelected == null)
            {
                await UserDialogs.Instance.AlertAsync("Debe ingresar el usuario a retar!.", "Aatención!", "Continuar");
                return false;
            }
            if (ComentaioOrigen.Text == null)
            {
                await UserDialogs.Instance.AlertAsync("Es necesario enviar una comentario!.", "Aatención!", "Continuar");
                return false;
            }
            if (ComentaioOrigen.Text.Count() > 200)
            {
                await UserDialogs.Instance.AlertAsync("El comentario no puede superar los 200 caracteres.", "Aatención!", "Continuar");
                return false;
            }

            return true;
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