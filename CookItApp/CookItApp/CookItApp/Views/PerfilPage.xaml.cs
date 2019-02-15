using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerfilPage : ContentPage
	{
        private MediaFile Foto { get; set; }
        private PerfilVM VMReceta;
        private Usuario Usuario;  
        private IViewMaster Vista { get; set; }

        public PerfilPage (Usuario usuario, IViewMaster vista)
		{            
            InitializeComponent();
            CargarPickerMomentoDia();
            CargarPickerEstacion();
            Usuario = usuario;
            Vista = vista;
            VMReceta = new PerfilVM(Usuario);                       
            BindingContext = VMReceta;
        }

        private void CargarPickerMomentoDia()
        {
            picMomentoDia.ItemsSource = App.DataBase.MomentoDia.ObtenerList();
        }

        private void CargarPickerEstacion()
        {
            picEstacion.ItemsSource = App.DataBase.Estacion.ObtenerList();
        }

        private void TogCalorias_Toggled(object sender, ToggledEventArgs e)
        {
            
            entryCaloriasMax.IsVisible = (e.Value == true) ? true : false;
            entryCaloriasMin.IsVisible = (e.Value == true) ? true : false;
            lblCaloriasMin.IsVisible = (e.Value == true) ? true : false;
            lblCaloriasMax.IsVisible = (e.Value == true) ? true : false;
            
        }

        private void TogPrecio_Toggled(object sender, ToggledEventArgs e)
        {
            lblCostoMayorIgual.IsVisible = (e.Value == true) ? true : false;
            entryCostoMayorIgual.IsVisible = (e.Value == true) ? true : false;
            lblCostoMenorIgual.IsVisible = (e.Value == true) ? true : false;
            entryCostoMenorIgual.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogCantPlatos_Toggled(object sender, ToggledEventArgs e)
        {
            lblCantPlatosMenorIgual.IsVisible = (e.Value == true) ? true : false;
            entryCantPlatosMenorIgual.IsVisible = (e.Value == true) ? true : false;
            lblCantPlatosMayorIgual.IsVisible = (e.Value == true) ? true : false;
            entryCantPlatosMayorIgual.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogTiempoPreparacion_Toggled(object sender, ToggledEventArgs e)
        {
            lblTiempoPreparacionMayorIgual.IsVisible = (e.Value == true) ? true : false;
            entryTiempoPreparacionMayorIgual.IsVisible = (e.Value == true) ? true : false;
            lblTiempoPreparacionMenorIgual.IsVisible = (e.Value == true) ? true : false;
            entryTiempoPreparacionMenorIgual.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogMomentoDia_Toggled(object sender, ToggledEventArgs e)
        {

            if (e.Value == true)
            {
                picMomentoDia.IsVisible = true;
            }
            else
            {                
                picMomentoDia.IsVisible = false;
            }
        }

        private void TogEstacion_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                picEstacion.IsVisible = true;
            }
            else
            {
                picEstacion.IsVisible = false;
            }
        }

        private void TogPuntajeTotal_Toggled(object sender, ToggledEventArgs e)
        {
            lblPuntajeTotalMayorIgual.IsVisible = (e.Value == true) ? true : false;
            entryPuntajeTotalMayorIgual.IsVisible = (e.Value == true) ? true : false;
            lblPuntajeTotalMenorIgual.IsVisible = (e.Value == true) ? true : false;
            entryPuntajeTotalMenorIgual.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogDificultad_Toggled(object sender, ToggledEventArgs e)
        {
            lblDificultadMayorIgual.IsVisible = (e.Value == true) ? true : false;
            entryDificultadMayorIgual.IsVisible = (e.Value == true) ? true : false;
            lblDificultadMenorIgual.IsVisible = (e.Value == true) ? true : false;
            entryDificultadMenorIgual.IsVisible = (e.Value == true) ? true : false;
        }

        private async void BtnGuardarPerfil_Clicked(object sender, EventArgs e)
        {
                UserDialogs.Instance.ShowLoading("Guardando perfil..");

                Perfil p = GenerarPerfil();
                Perfil perfil = new Perfil();

                if (Usuario._Perfil == null)
                {
                    perfil = await App.PerfilService.Alta(p);
                }
                else
                {
                    perfil = await App.PerfilService.Modificar(p);
                }


                if (perfil != null)
                {


                    int save = 0;

                    if (Usuario._Perfil == null)
                    {
                        save = App.DataBase.Perfil.Guardar(perfil);
                    }
                    else
                    {
                        save = App.DataBase.Perfil.Modificar(perfil);
                    }

                    if (save == 1)
                    {
                        UserDialogs.Instance.HideLoading();
                        await UserDialogs.Instance.AlertAsync("Se ha actualizado tú perfil", "Perfil de usuario", "Continuar");
                        //await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Perfil de usuario", "Se ha actualizado tu perfil"));
                        await scroll.ScrollToAsync(0, (double)ScrollToPosition.End, true);
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await UserDialogs.Instance.AlertAsync("Error al actualizar perfil, intentalo nuevamente.", "Perfil de usuario", "Continuar");
                        //await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Perfil de usuario", "Error al actualizar perfil, " + "intentalo nuevamente."));
                }

                    Vista.Actualizar(perfil);
                    Usuario._Perfil = perfil;

                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Error al actualizar perfil, pongase en contacto con el area de soporte.", "Perfil de usuario", "Continuar");
                    //await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Perfil de usuario", "Error al actualizar perfil, " +"pongase en contacto con el area de soporte."));
            }

                


        }



        private void TogFiltrosAutomaticos_Toggled(object sender, ToggledEventArgs e)
        {
            stkCeliaco.IsVisible = (e.Value == true) ? true : false;
            stkDiabetico.IsVisible = (e.Value == true) ? true : false;
            stkVegano.IsVisible = (e.Value == true) ? true : false;
            stkVegetariano.IsVisible = (e.Value == true) ? true : false;
            stkCalorias.IsVisible = (e.Value == true) ? true : false;
            stkPrecio.IsVisible = (e.Value == true) ? true : false;
            stkPlatos.IsVisible = (e.Value == true) ? true : false;
            stkTiempo.IsVisible = (e.Value == true) ? true : false;
            stkPuntaje.IsVisible = (e.Value == true) ? true : false;
            stkDificultad.IsVisible = (e.Value == true) ? true : false;
            stkMomento.IsVisible = (e.Value == true) ? true : false;
            stkEstacion.IsVisible = (e.Value == true) ? true : false;   
            stkIngredientes.IsVisible = (e.Value == true) ? true : false;

        }

        private Perfil GenerarPerfil() {

            Perfil p = new Perfil
            {

                _Email = VMReceta.Email,
                _Foto = (Foto != null) ? ImageToByteArray() : ((Usuario._Perfil != null && Usuario._Perfil._Foto != null) ? Usuario._Perfil._Foto : null),
                _NombreUsuario = entryNombreUsuario.Text,
                _Nombre = entryNombre.Text,
                _Apellido = entryApellido.Text,
                _Categoria = Usuario._Perfil != null ? Usuario._Perfil._Categoria : Perfil.Categoria.Amatér,
                _Puntuacion = Usuario._Perfil != null ? Usuario._Perfil._Puntuacion : 0,

                _FiltroAutomatico = (togFiltrosAutomaticos.IsToggled == true) ? true : false,

                _FiltroCeliaco = (togFiltrosAutomaticos.IsToggled == true && togAptoCeliaco.IsToggled == true) ? true : false,
                _FiltroDiabetico = (togFiltrosAutomaticos.IsToggled == true && togAptoDiabetico.IsToggled == true) ? true : false,
                _FiltroVegano = (togFiltrosAutomaticos.IsToggled == true && togAptoVegano.IsToggled == true) ? true : false,
                _FiltroVegetariano = (togFiltrosAutomaticos.IsToggled == true && togAptoVegetariano.IsToggled == true) ? true : false,

                _FiltroCalorias = (togFiltrosAutomaticos.IsToggled == true && togCalorias.IsToggled == true) ? true : false,
                _FiltroCaloriasMin = (togCalorias.IsToggled == true) ? Convert.ToInt16(entryCaloriasMin.Text) : 0,
                _FiltroCaloriasMax = (togCalorias.IsToggled == true) ? Convert.ToInt16(entryCaloriasMax.Text) : 0,

                _FiltroPrecio = (togFiltrosAutomaticos.IsToggled == true && togPrecio.IsToggled == true) ? true : false,
                _FiltroPrecioMin = (togPrecio.IsToggled == true) ? Convert.ToInt16(entryCostoMayorIgual.Text) : 0,
                _FiltroPrecioMax = (togPrecio.IsToggled == true) ? Convert.ToInt16(entryCostoMenorIgual.Text) : 0,

                _FiltroCantPlatos = (togFiltrosAutomaticos.IsToggled == true && togCantPlatos.IsToggled == true) ? true : false,
                _FiltroCantPlatosMin = (togCantPlatos.IsToggled == true) ? Convert.ToInt16(entryCantPlatosMayorIgual.Text) : 0,
                _FiltroCantPlatosMax = (togCantPlatos.IsToggled == true) ? Convert.ToInt16(entryCantPlatosMenorIgual.Text) : 0,

                _FiltroTiempoPreparacion = (togFiltrosAutomaticos.IsToggled == true && togTiempoPreparacion.IsToggled == true) ? true : false,
                _FiltroTiempoPreparacionMin = (togTiempoPreparacion.IsToggled == true) ? Convert.ToInt16(entryTiempoPreparacionMayorIgual.Text) : 0,
                _FiltroTiempoPreparacionMax = (togTiempoPreparacion.IsToggled == true) ? Convert.ToInt16(entryTiempoPreparacionMenorIgual.Text) : 0,

                _FiltroPuntuacion = (togFiltrosAutomaticos.IsToggled == true && togPuntajeTotal.IsToggled == true) ? true : false,
                _FiltroPuntuacionMin = (togPuntajeTotal.IsToggled == true) ? Convert.ToInt16(entryPuntajeTotalMayorIgual.Text) : 0,
                _FiltroPuntuacionMax = (togPuntajeTotal.IsToggled == true) ? Convert.ToInt16(entryPuntajeTotalMenorIgual.Text) : 0,

                _FiltroDificultad = (togFiltrosAutomaticos.IsToggled == true && togDificultad.IsToggled == true) ? true : false,
                _FiltroDificultadMin = (togDificultad.IsToggled == true) ? Convert.ToInt16(entryDificultadMayorIgual.Text) : 0,
                _FiltroDificultadMax = (togDificultad.IsToggled == true) ? Convert.ToInt16(entryDificultadMenorIgual.Text) : 0,

                _FiltroMomentoDia = (togFiltrosAutomaticos.IsToggled == true && togMomentoDia.IsToggled == true) ? true : false,
                _FiltroMomentoDiaId = (togMomentoDia.IsToggled == true) ? picMomentoDia.SelectedIndex : 1,
                
                _FiltroEstacion = (togFiltrosAutomaticos.IsToggled == true && togEstacion.IsToggled == true) ? true : false,
                _FiltroEstacionId = (togEstacion.IsToggled == true) ? picEstacion.SelectedIndex : 1
            };

            return p;
        }



        private async void BtnSacarFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();


            if (CrossMedia.Current.IsCameraAvailable & CrossMedia.Current.IsTakePhotoSupported)
            {
                Foto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Name = "miPerfil.jpg",
                    PhotoSize = PhotoSize.Small
                });

                if (Foto != null)
                {
                    imgPerfil.Source = ImageSource.FromStream(Foto.GetStream);
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(Usuario, "Error de camara", "Hay un error con tu camara, " +
                "no se puede sacar la foto."));
            }

        }

        public byte[] ImageToByteArray()
        {
            using (var memoryStream = new MemoryStream())
            {
                Foto.GetStream().CopyTo(memoryStream);
                Foto.Dispose();
                return memoryStream.ToArray();
            }
        }

        private async void BtnCambiarCont_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupCambiarContraseñaPage(Usuario));
            await scroll.ScrollToAsync(0, (double)ScrollToPosition.End, true);
        }
    }


}