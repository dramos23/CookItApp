using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecetaPage : ContentPage
    {
     
        RecetaVM VMReceta { get; set; }
        private Receta Receta { get; set;}
        private string Email { get; set; }
        private Usuario Usuario { get; set; }
        


        public RecetaPage(Receta receta, Usuario usuario)
        {
            InitializeComponent();
            Email = usuario._Email;
            Usuario = usuario;
            Receta = receta;
            VMReceta = new RecetaVM(Receta);
            BindingContext = VMReceta;
            //Se generan el ViewModel que precisa la página xaml para mostrar los datos. La Receta "rec" se recibe cuando un usuario clickea la receta
            //desde el buscador de recetas, generando una instancia de esta página.
            //BindingContext = _VMReceta;
        }

        //public async void ObtenerReceta(Receta receta) {

        //    while (Receta == null)
        //    {
        //        this.Receta = await App.RecetaService.Obtener(receta);

        //        if (Receta != null)
        //        {
        //            _VMReceta = new RecetaVM(Receta);
        //            BindingContext = _VMReceta;
        //        }

        //    }

        //}

        private async void BtnComentarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ComentariosRecetaPage(Receta, Usuario));
        }

        private async void BtnPrepararReceta_Clicked(object sender, EventArgs e)
        {
            CargarDatosPrueba();
            await Navigation.PushAsync(new PasoRecetaPage(Receta, Receta._ListaPasosReceta[0], Usuario));
        }

        private void BtnRetar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupApuestaPage(Usuario, Receta));
        }

        private async void BtnAgregarFavoritos_Clicked(object sender, EventArgs e)
        {
            RecetaFavorita rf = new RecetaFavorita()
            {
                _Email = Email,
                _IdReceta = Receta._IdReceta,
                _FechaHora = DateTime.Now
            };
            var favorito = await App.RecetaFavoritaService.Alta(rf);
            if (favorito != null) {
                //this.BT BtnAgregarFavoritos.IsEnabled = false;
                //this.BtnAgregarFavoritos.Image = "favorite.png";
                //this.BtnAgregarFavoritos.Text = "";
                //this.BtnAgregarFavoritos.BorderColor = Color.Transparent;
                //this.btnAgregarFavoritos.BackgroundColor = Color.Transparent;
            }
        }

        private void CargarDatosPrueba()
        {
            PasoReceta paso = new PasoReceta
            {
                _IdPasoReceta = 1,
                _Descripcion = "Para empezar vea el video y siga las instrucciones. Va a precisar una espatula, dos huevos y sal.",
                _UrlVideo = "https://www.youtube.com/watch?v=ZJy1ajvMU1k"
            };
            PasoReceta paso2 = new PasoReceta
            {
                _IdPasoReceta = 2,
                _Descripcion = "Acto seguido espere cinco minutos a que se cocinen los huevos. Mientras tanto marine el pescado con condimento verde y sal.",
                _TiempoReloj = 360
            };
            PasoReceta paso3 = new PasoReceta
            {
                _IdPasoReceta = 3,
                _Descripcion = "Para terminar, espere otros 2 minutos a que se frite el pescado. Ensalse a gusto. ¡Buen provecho!",
                _TiempoReloj = 120
            };
            List<PasoReceta> pasos = new List<PasoReceta>
            {
                paso,
                paso2,
                paso3
            };

            Receta._ListaPasosReceta = pasos;

        }
            
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new ListaRecetasPage(Usuario));

            var stackNav = Navigation.NavigationStack.ToList();
            foreach (var navPage in stackNav)
            {
                Navigation.RemovePage(navPage);
            }

            return true;
        }

    }
}