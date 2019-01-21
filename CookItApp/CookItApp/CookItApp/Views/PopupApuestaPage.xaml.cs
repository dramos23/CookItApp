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
	public partial class PopupApuestaPage
	{
        private PopupApuestaVM VMPopupApuesta { get; set; }

        private Usuario _Usuario { get; set; }

        private Receta _Receta { get; set; }

        private List<Perfil> _ListPerfiles { get; set; }

        private Perfil _UsuarioSelected { get; set; }

        public PopupApuestaPage (Usuario Usuario, Receta Receta)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            _Receta = Receta;
            ObtenerListPerfil();
            VMPopupApuesta = new PopupApuestaVM(null);
            BindingContext = VMPopupApuesta;
        }

        private async void ObtenerListPerfil()
        {
             _ListPerfiles = await App.PerfilService.ObtenerList();
        }

        private void UsuarioOrigen_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = UsuarioOrigen.Text;
            if (keyword != "")
            {
                    List<Perfil> resultado = _ListPerfiles.Where(c => c._NombreUsuario.ToLower().Contains(keyword.ToLower())).ToList();

                    ListPerfiles.IsVisible = true;
                    ListPerfiles.ItemsSource = resultado;

            }
            else {

                ListPerfiles.IsVisible = false;

            }


        }

        private async void RetarUsuario_Clicked(object sender, EventArgs e)
        {
            Reto reto = new Reto
            {
                _EmailUsuOri = _Usuario._Email,
                _ComentarioOrigen = ComentaioOrigen.Text,
                _EmialUsuDes = _UsuarioSelected._Email,
                _RecetaId = _Receta._IdReceta,
                _Fecha = DateTime.Now,
                _IdEstadoReto = 1,
                _Puntaje = Convert.ToInt32(Math.Round(_Receta._Dificultad + _Receta._PuntajeTotal, 0))
            };

            if (reto != null) {

                //rest api
                Reto r = await App.RetoService.Alta(reto);

                if (r != null) {

                    //guardo ?

                }

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