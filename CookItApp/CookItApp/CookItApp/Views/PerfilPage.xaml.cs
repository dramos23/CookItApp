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
        private PerfilVM VMPerfil;
        private Usuario Usuario;  
        private IViewMaster Vista { get; set; }

        public PerfilPage (Usuario usuario, IViewMaster vista)
		{            
            InitializeComponent();                        
            Usuario = usuario;
            Vista = vista;
            VMPerfil = new PerfilVM(Usuario);                       
            BindingContext = VMPerfil;
        }





        private void TogCalorias_Toggled(object sender, ToggledEventArgs e)
        {

            stkCaloriasV.IsVisible = (e.Value == true) ? true : false;
            //entryCaloriasMin.IsVisible = (e.Value == true) ? true : false;
            //lblCaloriasMin.IsVisible = (e.Value == true) ? true : false;
            //lblCaloriasMax.IsVisible = (e.Value == true) ? true : false;
            
        }

        private void TogPrecio_Toggled(object sender, ToggledEventArgs e)
        {
            stkPrecioV.IsVisible = (e.Value == true) ? true : false;
            //entryPrecioMax.IsVisible = (e.Value == true) ? true : false;
            //lblPrecioMin.IsVisible = (e.Value == true) ? true : false;
            //entryPrecioMin.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogCantPlatos_Toggled(object sender, ToggledEventArgs e)
        {
            stkPlatosV.IsVisible = (e.Value == true) ? true : false;
            //entryCantPlatosMin.IsVisible = (e.Value == true) ? true : false;
            //lblCantPlatosMax.IsVisible = (e.Value == true) ? true : false;
            //entryCantPlatosMax.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogTiempoPreparacion_Toggled(object sender, ToggledEventArgs e)
        {
            stkTiempoV.IsVisible = (e.Value == true) ? true : false;
            //entryTiempoPreparacionMax.IsVisible = (e.Value == true) ? true : false;
            //lblTiempoPreparacionMin.IsVisible = (e.Value == true) ? true : false;
            //entryTiempoPreparacionMin.IsVisible = (e.Value == true) ? true : false;
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
            stkPuntajeV.IsVisible = (e.Value == true) ? true : false;
            //entryPuntajeTotalMax.IsVisible = (e.Value == true) ? true : false;
            //lblPuntajeTotalMin.IsVisible = (e.Value == true) ? true : false;
            //entryPuntajeTotalMin.IsVisible = (e.Value == true) ? true : false;
        }

        private void TogDificultad_Toggled(object sender, ToggledEventArgs e)
        {
            stkDificultadV.IsVisible = (e.Value == true) ? true : false;
            //entryDificultadMax.IsVisible = (e.Value == true) ? true : false;
            //lblDificultadMin.IsVisible = (e.Value == true) ? true : false;
            //entryDificultadMin.IsVisible = (e.Value == true) ? true : false;
        }

        private async void BtnGuardarPerfil_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Guardando perfil..");

            Perfil perfil = await GenerarPerfil();

            if (perfil != null)
            {

                int estado = await VMPerfil.GuardarPerfil(perfil);

                if (estado == 2)
                {

                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Error al actualizar perfil, pongase en contacto con el area de soporte.", "Error!", "Continuar");

                }

                if (estado == 1)
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Se ha actualizado tú perfil", "Perfil de usuario", "Continuar");
                    await scroll.ScrollToAsync(0, (double)ScrollToPosition.End, true);

                    Vista.Actualizar(perfil);
                    Usuario._Perfil = perfil;
                }

                if (estado == 0)
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Error interno, reinicia la aplicación.", "Error!", "Continuar");
                }

            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Se ha producido un error, vuelva a intentarlo.", "Error!", "Continuar");

            }


            UserDialogs.Instance.HideLoading();

        }



        private void TogFiltrosAutomaticos_Toggled(object sender, ToggledEventArgs e)
        {
            stkCeliaco.IsVisible = (e.Value == true) ? true : false;
            stkDiabetico.IsVisible = (e.Value == true) ? true : false;
            stkVegano.IsVisible = (e.Value == true) ? true : false;
            stkVegetariano.IsVisible = (e.Value == true) ? true : false;
            stkIngredientes.IsVisible = (e.Value == true) ? true : false;
            stkCalorias.IsVisible = (e.Value == true) ? true : false;
            stkPrecio.IsVisible = (e.Value == true) ? true : false;
            stkPlatos.IsVisible = (e.Value == true) ? true : false;
            stkTiempo.IsVisible = (e.Value == true) ? true : false;
            stkPuntaje.IsVisible = (e.Value == true) ? true : false;
            stkDificultad.IsVisible = (e.Value == true) ? true : false;
            stkMomento.IsVisible = (e.Value == true) ? true : false;
            stkEstacion.IsVisible = (e.Value == true) ? true : false;   
            stkIngredientes.IsVisible = (e.Value == true) ? true : false;

            if (e.Value == false)
            {
                togCalorias.IsToggled = false;
                togPrecio.IsToggled = false;
                togCantPlatos.IsToggled = false;
                togTiempoPreparacion.IsToggled = false;
                togPuntajeTotal.IsToggled = false;
                togDificultad.IsToggled = false;
                togMomentoDia.IsToggled = false;
                togEstacion.IsToggled = false;

                togAptoVegetariano.IsToggled = false;
                togAptoVegano.IsToggled = false;
                togAptoCeliaco.IsToggled = false;
                togAptoDiabetico.IsToggled = false;
                togMisIngredientes.IsToggled = false;
            }

        }

        private async Task<Perfil> GenerarPerfil() {

            if (await ValidarEntrys())
            {

                Perfil p = new Perfil
                {

                    _Email = VMPerfil.Email,
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
                    _FiltroIngredientes = (togFiltrosAutomaticos.IsToggled == true && togMisIngredientes.IsToggled == true) ? true : false,

                    _FiltroCalorias = (togFiltrosAutomaticos.IsToggled == true && togCalorias.IsToggled == true) ? true : false,
                    _FiltroCaloriasMin = (togCalorias.IsToggled == true) ? Convert.ToInt16(entryCaloriasMin.Text) : 0,
                    _FiltroCaloriasMax = (togCalorias.IsToggled == true) ? Convert.ToInt16(entryCaloriasMax.Text) : 0,

                    _FiltroPrecio = (togFiltrosAutomaticos.IsToggled == true && togPrecio.IsToggled == true) ? true : false,
                    _FiltroPrecioMin = (togPrecio.IsToggled == true) ? Convert.ToInt16(entryPrecioMax.Text) : 0,
                    _FiltroPrecioMax = (togPrecio.IsToggled == true) ? Convert.ToInt16(entryPrecioMin.Text) : 0,

                    _FiltroCantPlatos = (togFiltrosAutomaticos.IsToggled == true && togCantPlatos.IsToggled == true) ? true : false,
                    _FiltroCantPlatosMin = (togCantPlatos.IsToggled == true) ? Convert.ToInt16(entryCantPlatosMax.Text) : 0,
                    _FiltroCantPlatosMax = (togCantPlatos.IsToggled == true) ? Convert.ToInt16(entryCantPlatosMin.Text) : 0,

                    _FiltroTiempoPreparacion = (togFiltrosAutomaticos.IsToggled == true && togTiempoPreparacion.IsToggled == true) ? true : false,
                    _FiltroTiempoPreparacionMin = (togTiempoPreparacion.IsToggled == true) ? Convert.ToInt16(entryTiempoPreparacionMax.Text) : 0,
                    _FiltroTiempoPreparacionMax = (togTiempoPreparacion.IsToggled == true) ? Convert.ToInt16(entryTiempoPreparacionMin.Text) : 0,

                    _FiltroPuntuacion = (togFiltrosAutomaticos.IsToggled == true && togPuntajeTotal.IsToggled == true) ? true : false,
                    _FiltroPuntuacionMin = (togPuntajeTotal.IsToggled == true) ? Convert.ToInt16(entryPuntajeTotalMax.Text) : 0,
                    _FiltroPuntuacionMax = (togPuntajeTotal.IsToggled == true) ? Convert.ToInt16(entryPuntajeTotalMin.Text) : 0,

                    _FiltroDificultad = (togFiltrosAutomaticos.IsToggled == true && togDificultad.IsToggled == true) ? true : false,
                    _FiltroDificultadMin = (togDificultad.IsToggled == true) ? Convert.ToInt16(entryDificultadMax.Text) : 0,
                    _FiltroDificultadMax = (togDificultad.IsToggled == true) ? Convert.ToInt16(entryDificultadMin.Text) : 0,

                    _FiltroMomentoDia = (togFiltrosAutomaticos.IsToggled == true && togMomentoDia.IsToggled == true) ? true : false,
                    _FiltroMomentoDiaId = (togMomentoDia.IsToggled == true) ? picMomentoDia.SelectedIndex : 1,

                    _FiltroEstacion = (togFiltrosAutomaticos.IsToggled == true && togEstacion.IsToggled == true) ? true : false,
                    _FiltroEstacionId = (togEstacion.IsToggled == true) ? picEstacion.SelectedIndex : 1
                };

                return p;

            }
            else {

                return null;
            }

            
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

        private void EntryCaloriasMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryCaloriasMin.Text != null && entryCaloriasMin.Text != "")
            {
                string num = entryCaloriasMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryCaloriasMin.Text = num;
                    
                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryCaloriasMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryCaloriasMin.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryCaloriasMin.Text = "9999";
                }

            }

        }

        private void EntryCaloriasMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryCaloriasMax.Text != null && entryCaloriasMax.Text != "")
            {
                string num = entryCaloriasMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryCaloriasMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryCaloriasMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryCaloriasMax.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryCaloriasMax.Text = "9999";
                }

            }
        }

        private void EntryPrecioMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryPrecioMax.Text != null && entryPrecioMax.Text != "")
            {
                string num = entryPrecioMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryPrecioMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryPrecioMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryPrecioMax.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryPrecioMax.Text = "9999";
                }

            }
        }

        private void EntryPrecioMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryPrecioMin.Text != null && entryPrecioMin.Text != "")
            {
                string num = entryPrecioMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryPrecioMin.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryPrecioMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryPrecioMin.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryPrecioMin.Text = "9999";
                }

            }
        }

        private void EntryCantPlatosMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryCantPlatosMax.Text != null && entryCantPlatosMax.Text != "")
            {
                string num = entryCantPlatosMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryCantPlatosMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryCantPlatosMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryCantPlatosMax.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryCantPlatosMax.Text = "9999";
                }

            }
        }

        private void EntryCantPlatosMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryCantPlatosMin.Text != null && entryCantPlatosMin.Text != "")
            {
                string num = entryCantPlatosMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryCantPlatosMin.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryCantPlatosMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryCantPlatosMin.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryCantPlatosMin.Text = "9999";
                }

            }
        }

        private void EntryTiempoPreparacionMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryTiempoPreparacionMax.Text != null && entryTiempoPreparacionMax.Text != "")
            {
                string num = entryTiempoPreparacionMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryTiempoPreparacionMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryTiempoPreparacionMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryTiempoPreparacionMax.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryTiempoPreparacionMax.Text = "9999";
                }

            }
        }

        private void EntryTiempoPreparacionMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryTiempoPreparacionMin.Text != null && entryTiempoPreparacionMin.Text != "")
            {
                string num = entryTiempoPreparacionMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryTiempoPreparacionMin.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryTiempoPreparacionMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryTiempoPreparacionMin.Text = "1";
                }

                if (numInt > 9999)
                {
                    entryTiempoPreparacionMin.Text = "9999";
                }

            }
        }

        private void EntryPuntajeTotalMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryPuntajeTotalMax.Text != null && entryPuntajeTotalMax.Text != "")
            {
                string num = entryPuntajeTotalMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryPuntajeTotalMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryPuntajeTotalMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryPuntajeTotalMax.Text = "1";
                }

                if (numInt > 5)
                {
                    entryPuntajeTotalMax.Text = "5";
                }

            }
        }

        private void EntryPuntajeTotalMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryPuntajeTotalMin.Text != null && entryPuntajeTotalMin.Text != "")
            {
                string num = entryPuntajeTotalMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryPuntajeTotalMin.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryPuntajeTotalMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryPuntajeTotalMin.Text = "1";
                }

                if (numInt > 5)
                {
                    entryPuntajeTotalMin.Text = "5";
                }

            }
        }

        private void EntryDificultadMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryDificultadMax.Text != null && entryDificultadMax.Text != "")
            {
                string num = entryDificultadMax.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryDificultadMax.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryDificultadMax.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryCaloriasMin.Text = "1";
                }

                if (numInt > 5)
                {
                    entryCaloriasMin.Text = "5";
                }

            }
        }

        private void EntryDificultadMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entryDificultadMin.Text != null && entryDificultadMin.Text != "")
            {
                string num = entryDificultadMin.Text;
                if (num.Substring(num.Length - 1) == "," || num.Substring(num.Length - 1) == "." || num.Substring(num.Length - 1) == "-")
                {
                    num = num.Replace(",", "");
                    num = num.Replace(".", "");
                    entryDificultadMin.Text = num;

                }

                var numInt = Convert.ToInt32(num);
                var numDbl = Convert.ToDecimal(num);

                if (numInt < numDbl)
                {
                    entryDificultadMin.Text = Convert.ToString(numInt);
                }

                if (numInt < 1)
                {
                    entryDificultadMin.Text = "1";
                }

                if (numInt > 5)
                {
                    entryDificultadMin.Text = "5";
                }

            }
        }

        // VALIDANDO ENTRYS ANTES DE GENERAR OBJETO PERFIL
        private async Task<bool> ValidarEntrys()
        {
            bool validando = true;

            if (validando) { validando = await ValidarCaloriasAsync(); }
            if (validando) { validando = await ValidarPrecioAsync();  }
            if (validando) { validando = await ValidarCantPlatosAsync(); }
            if (validando) { validando = await ValidarTiempoAsync(); }
            if (validando) { validando = await ValidarPuntajeTotalAsync(); }
            if (validando) { validando = await ValidarDificultad(); }

            return validando;
        }

        private async Task<bool> ValidarCaloriasAsync() { 

            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            // CALORIAS

            if (togCalorias.IsToggled == true && entryCaloriasMin.Text == "" && todoBien) {

                mensaje = "Debe ingresar un valor a Calorias Min.";
                todoBien = false;

            }


            if (togCalorias.IsToggled == true && entryCaloriasMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Calorias Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryCaloriasMin.Text);
            max = Convert.ToInt32(entryCaloriasMax.Text);

            if (togCalorias.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Calorias Min no puede ser superior a Calorias Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }

        private async Task<bool> ValidarPrecioAsync()
        {
            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            //PRECIO

            if (togPrecio.IsToggled == true && entryPrecioMin.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Precio Min.";
                todoBien = false;

            }

            if (togPrecio.IsToggled == true && entryPrecioMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Precio Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryPrecioMin.Text);
            max = Convert.ToInt32(entryPrecioMax.Text);

            if (togPrecio.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Precio Min no puede ser superior a Precio Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }

        private async Task<bool> ValidarCantPlatosAsync()
        {
            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            //CANT. PLATOS

            if (togCantPlatos.IsToggled == true && entryCantPlatosMin.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Cant. Platos Min.";
                todoBien = false;

            }


            if (togCantPlatos.IsToggled == true && entryCantPlatosMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Cant. Platos Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryCantPlatosMin.Text);
            max = Convert.ToInt32(entryCantPlatosMax.Text);

            if (togCantPlatos.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Cant. Platos Min no puede ser superior a Cant. Platos Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }

        private async Task<bool> ValidarTiempoAsync()
        {
            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            //TIEMPO PREPARACION

            if (togTiempoPreparacion.IsToggled == true && entryTiempoPreparacionMin.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Tiempo Preparación Min.";
                todoBien = false;

            }


            if (togTiempoPreparacion.IsToggled == true && entryTiempoPreparacionMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Tiempo Preparación Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryTiempoPreparacionMin.Text);
            max = Convert.ToInt32(entryTiempoPreparacionMax.Text);

            if (togTiempoPreparacion.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Tiempo Preparación Min no puede ser superior a Tiempo Preparación Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }

        private async Task<bool> ValidarPuntajeTotalAsync()
        {
            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            //Puntaje Total

            if (togPuntajeTotal.IsToggled == true && entryPuntajeTotalMin.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Puntaje Total Min.";
                todoBien = false;

            }


            if (togPuntajeTotal.IsToggled == true && entryPuntajeTotalMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Puntaje Total Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryPuntajeTotalMin.Text);
            max = Convert.ToInt32(entryPuntajeTotalMax.Text);

            if (togPuntajeTotal.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Puntaje Total Min no puede ser superior a Puntaje Total Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }

        private async Task<bool> ValidarDificultad()
        {
            bool todoBien = true;
            string mensaje = "";
            int min = 0;
            int max = 0;

            //DIFICULTAD

            if (togDificultad.IsToggled == true && entryDificultadMin.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Dificultad Min.";
                todoBien = false;

            }


            if (togDificultad.IsToggled == true && entryDificultadMax.Text == "" && todoBien)
            {

                mensaje = "Debe ingresar un valor a Dificultad Max.";
                todoBien = false;

            }

            min = Convert.ToInt32(entryDificultadMin.Text);
            max = Convert.ToInt32(entryDificultadMax.Text);

            if (togDificultad.IsToggled == true && min > max && todoBien)
            {

                mensaje = "Dificultad Min no puede ser superior a Dificultad Max.";
                todoBien = false;

            }

            if (!todoBien)
            {
                await UserDialogs.Instance.AlertAsync(mensaje, "Error!", "Continuar");
                UserDialogs.Instance.HideLoading();
            }

            return todoBien;
        }


    }


}