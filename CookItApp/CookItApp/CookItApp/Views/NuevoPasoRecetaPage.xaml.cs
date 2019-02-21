using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoPasoRecetaPage : ContentPage
	{
        private IViewListPasosRec Vista { get; set; }
        private bool UPD { get; set; }
        private NuevoPasoRecetaVM VMNuevoPasoReceta { get; set; }

        private MediaFile FotoFile { get; set; }

        public NuevoPasoRecetaPage (PasoReceta pasoReceta, int idPasoReceta, IViewListPasosRec vista)
		{
			InitializeComponent ();
            Vista = vista;
            UPD = pasoReceta != null ? true : false;

            VMNuevoPasoReceta = new NuevoPasoRecetaVM(pasoReceta, idPasoReceta);
            BindingContext = VMNuevoPasoReceta;

            ForzarCargaDescripcion();
        }

        private void ForzarCargaDescripcion()
        {
            if (UPD)
            {
                entryDescripcion.Text = VMNuevoPasoReceta.Descripcion;
            }
        }

        private async void EliminarPasoReceta_Clicked(object sender, EventArgs e)
        {
            if (UPD)
            {
                Vista.Eliminar(VMNuevoPasoReceta.PasoReceta);
                await Navigation.PopAsync();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Aún no has guardado esté paso!.", "Atención!", "Continuar");
            }
        }

        private async void BtnSacarFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable & CrossMedia.Current.IsTakePhotoSupported)
            {
                FotoFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Name = "miReceta.jpg",
                    PhotoSize = PhotoSize.Large
                });
                

                if (FotoFile != null)
                {
                    imgPresentacion.Source = ImageSource.FromStream(FotoFile.GetStream);
                }
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Se ha producido un error con el dispositivo!.", "Error!.", "Continuar");
            }
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (await ValidarDatos())
            {
                string Descripcion = entryDescripcion.Text;
                string URL = entryURL.Text;
                int Tiempo = Convert.ToInt32(entryTiempo.Text);
                MediaFile Foto = FotoFile;

                ImageSource imagen_defecto = "noimage.png";

                if (imgPresentacion.Source == null || imgPresentacion.Source.Equals(imagen_defecto))
                {
                    Foto = null;
                }


                VMNuevoPasoReceta.CargarPasoRec(Foto, Descripcion, URL, Tiempo); 

                if (UPD)
                {
                    Vista.Actualizar(VMNuevoPasoReceta.PasoReceta);
                }
                else
                {
                    Vista.Insertar(VMNuevoPasoReceta.PasoReceta);
                }
                await Navigation.PopAsync();
            }
        }

        private async Task<bool> ValidarDatos()
        {
            

            if (entryDescripcion.Text == null)
            {
                await UserDialogs.Instance.AlertAsync("Es necesario ingresar la descripción!.", "Atención!", "Continuar");
                return false;
            }
            if (entryDescripcion.Text.Count() > 200)
            {
                await UserDialogs.Instance.AlertAsync("La descripción no puede superar los 200 caracteres!.", "Atención!", "Continuar");
                return false;
            }
            if (entryTiempo.Text == null || Convert.ToInt32(entryTiempo.Text) < 0)
            {
                await UserDialogs.Instance.AlertAsync("El tiempo de preparación no puede ser menor a 0 minutos!.", "Atención!", "Continuar");
                return false;
            }

            return true;
        }

    }
}