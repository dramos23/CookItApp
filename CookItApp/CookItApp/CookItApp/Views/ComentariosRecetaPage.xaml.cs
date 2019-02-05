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
	public partial class ComentariosRecetaPage : ContentPage
	{
        private ComentariosRecetaVM _ViewModel;
        private ComentarioReceta comentarioCreado;
        private Usuario Usuario { get; set; }
		public ComentariosRecetaPage (Receta rec, Usuario user)
		{
            _ViewModel = new ComentariosRecetaVM(rec);
			InitializeComponent ();
            Usuario = user;
            BindingContext = _ViewModel;
		}


        private void BtnCrearComentario_Clicked(object sender, EventArgs e)
        {
            layoutPrincipal.Opacity = 0.5;
            layoutCrearComentario.IsVisible = true;
            comentarioCreado = new ComentarioReceta
            {
                _IdReceta = _ViewModel._Receta._IdReceta,
                _Email = Usuario._Email
            };
        }

        private async void BtnVolverReceta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetaPage(_ViewModel._Receta, Usuario));
        }

        private void BtnPuntaje1_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOff.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            comentarioCreado._Puntaje = 1;
        }

        private void BtnPuntaje2_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            comentarioCreado._Puntaje = 2;
        }

        private void BtnPuntaje3_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            comentarioCreado._Puntaje = 3;
        }

        private void BtnPuntaje4_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOn.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            comentarioCreado._Puntaje = 4;
        }

        private void BtnPuntaje5_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOn.png";
            BtnPuntaje5.Source = "estrellaOn.png";
            comentarioCreado._Puntaje = 5;
        }

        private void ResetearEstrellasFavoritos()
        {
            BtnPuntaje1.Source = "estrellaOff.png";
            BtnPuntaje2.Source = "estrellaOff.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
        }

        private void ResetearComentarioYLayout()
        {
            layoutCrearComentario.IsVisible = false;
            layoutPrincipal.Opacity = 1;
            txtComentario.Text = "";
            comentarioCreado = null;
        }

        private async void BtnInsertarComentario_Clicked(object sender, EventArgs e)
        {
            comentarioCreado._Comentario = txtComentario.Text;
            comentarioCreado._Fecha = DateTime.Now;
            try
            {
                _ViewModel.InsertarComentario(comentarioCreado);
                ResetearComentarioYLayout();
                ResetearEstrellasFavoritos();
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Comentando receta", "Comentario agregado."));
                RefrescarComentarios();
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Comentando receta", "Hubo un error al ingresar su comentario," +
                    "intente nuevamente mas tarde."));
            }
        }

        private void RefrescarComentarios()
        {
            lstComentarios.ItemsSource = _ViewModel._ComentariosReceta;
        }

        private void BtnVolverComentarios_Clicked(object sender, EventArgs e)
        {
            ResetearComentarioYLayout();
            ResetearEstrellasFavoritos();
        }
    }
}