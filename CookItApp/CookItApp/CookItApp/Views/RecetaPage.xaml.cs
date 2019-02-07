using Acr.UserDialogs;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            GenerarImagenFavorito();

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
            await Navigation.PushAsync(new PasoRecetaPage(Receta, Receta._ListaPasosReceta[0], Usuario));
        }

        private async void BtnRetar_Clicked(object sender, EventArgs e)
        {
            if (ControlPerfil() == true)
            {
                UserDialogs.Instance.ShowLoading("Cargando..");
                var perfiles = await App.PerfilService.ObtenerList();
                perfiles = perfiles.Where(p => p._Email != Usuario._Perfil._Email).ToList();

                if (perfiles.Count > 0)
                {
                    UserDialogs.Instance.HideLoading();
                    await PopupNavigation.Instance.PushAsync(new PopupApuestaPage(Usuario, Receta, perfiles));
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Vuelva a intentarlo.", "OK");
                }
            }
            
        }

        private async void BtnAgregarFavoritos_Clicked(object sender, EventArgs e)
        {
            if (ControlPerfil() == true)
            {
                RecetaFavorita rf = new RecetaFavorita()
                {
                    _Email = Email,
                    _IdReceta = Receta._IdReceta,
                    _FechaHora = DateTime.Now
                };

                if (!Usuario.RecetaEsFavorita(Receta))
                {

                    var favorito = await App.RecetaFavoritaService.Alta(rf);
                    if (favorito != null)
                    {
                        App.DataBase.RecetaFavorita.Guardar(favorito);
                    }

                    btnAgregarFavoritos.Source = "iconFavoritoOn.png";
                }
                else
                {
                    await App.RecetaFavoritaService.Eliminar(rf);
                    btnAgregarFavoritos.Source = "iconFavoritoOff.png";
                }
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

        private void GenerarImagenFavorito()
        {
            if (Usuario.RecetaEsFavorita(Receta)) btnAgregarFavoritos.Source = "iconFavoritoOff.png";
            else btnAgregarFavoritos.Source = "iconFavoritoOn.png";
        }


        /*Override que reemplaza el método invocado cuando el usuario apreta el boton "atras" en su celular, para evitar
         que hayan problemas con el navigation stack y los pasos de receta vistos. */
        protected override bool OnBackButtonPressed()
        {
            try
            {
                Navigation.PushAsync(new ListaRecetasPage(Usuario));

                var stackNav = Navigation.NavigationStack.ToList();
                foreach (var navPage in stackNav)
                {
                    Navigation.RemovePage(navPage);
                }
            }catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }


            return true;
        }

        public bool ControlPerfil()
        {

            if (Usuario._Perfil == null)
            {
                string mensaje = "Para utilizar está opción tiene que completar su perfil.";
                UserDialogs.Instance.Alert(mensaje, "Complete su perfil", "Continuar");
                return false;

            }
            else {
                return true;
            }

        }

    }


}