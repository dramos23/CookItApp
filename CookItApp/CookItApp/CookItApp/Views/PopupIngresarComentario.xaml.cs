using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Pages;
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
    public partial class PopupIngresarComentario : PopupPage
    {
        Usuario Usuario;
        Receta Receta;
        AltaComentarioVM ViewModel;
        IViewComentario Vista;
        private int Puntaje;

        public PopupIngresarComentario(Usuario usr, Receta rec, IViewComentario vista)
        {
            InitializeComponent();
            Usuario = usr;
            Receta = rec;
            ViewModel = new AltaComentarioVM(usr, rec);
            Vista = vista;
        }

        private void BtnPuntaje1_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOff.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            Puntaje = 1;
        }

        private void BtnPuntaje2_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            Puntaje = 2;
        }

        private void BtnPuntaje3_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            Puntaje = 3;
        }

        private void BtnPuntaje4_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOn.png";
            BtnPuntaje5.Source = "estrellaOff.png";
            Puntaje = 4;
        }

        private void BtnPuntaje5_Clicked(object sender, EventArgs e)
        {
            BtnPuntaje1.Source = "estrellaOn.png";
            BtnPuntaje2.Source = "estrellaOn.png";
            BtnPuntaje3.Source = "estrellaOn.png";
            BtnPuntaje4.Source = "estrellaOn.png";
            BtnPuntaje5.Source = "estrellaOn.png";
            Puntaje = 5;
        }

        private void ResetearEstrellasFavoritos()
        {
            BtnPuntaje1.Source = "estrellaOff.png";
            BtnPuntaje2.Source = "estrellaOff.png";
            BtnPuntaje3.Source = "estrellaOff.png";
            BtnPuntaje4.Source = "estrellaOff.png";
            BtnPuntaje5.Source = "estrellaOff.png";
        }
        
        private async void BtnIngresarComentario_Clicked(object sender, EventArgs e)
        {
            if(Puntaje == 0)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Error", "Debe ingresar un puntaje."));
                return;
            }
            if(txtComentario.Text.Trim() == "")
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Error", "Debe ingresar un comentario."));
                return;
            }
            try
            {
                ComentarioReceta comentario = new ComentarioReceta
                {
                    _Fecha = DateTime.Now,
                    _Comentario = txtComentario.Text,
                    _Puntaje = Puntaje,
                    _Receta = Receta,
                    _IdReceta = Receta._IdReceta,
                    _Email = Usuario._Email
                };
                ViewModel.IngresarComentario(comentario);
                await PopupNavigation.Instance.PopAsync();
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Exito!", "Mensaje ingresado."));
                Vista.ActualizarLista(comentario);
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Error en ingreso", ex.Message));
            }

        }
        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

    }
}