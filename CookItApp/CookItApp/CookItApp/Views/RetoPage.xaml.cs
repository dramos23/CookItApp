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
	public partial class RetoPage : ContentPage
	{
        private RetoVM VMReto { get; set; }

        private Usuario Usuario { get; set; }

        private MediaFile Foto { get; set; }

        private IViewDesafioList Vista { get; set; }

        public RetoPage (Reto reto, Usuario usuario, IViewDesafioList vista)
		{
			InitializeComponent ();

            Vista = vista;
            Usuario = usuario;
            Receta_Clicked();
            VMReto = new RetoVM(usuario, reto);
            BindingContext = VMReto;
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            await ProcesarEstado(2);
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await ProcesarEstado(3);
        }

        private async void BtnFinalizar_Clicked(object sender, EventArgs e)
        {

            UserDialogs.Instance.ShowLoading("Validando datos..");

            if (await ValidarDatos())
            {
                UserDialogs.Instance.HideLoading();
                VMReto.Reto._Presentacion = ImageToByteArray();
                VMReto.Reto._ComentarioDestino = txtComentario.Text;

                await ProcesarEstado(4);
            }

        }
        private async void BtnAprobado_Clicked(object sender, EventArgs e)
        {
            await ProcesarEstado(5);
        }

        private async void BtnRechazado_Clicked(object sender, EventArgs e)
        {
            await ProcesarEstado(6);
        }
    
        private async Task ProcesarEstado(int num)
        {
            UserDialogs.Instance.ShowLoading("Procesando..");

            var estado = await VMReto.ProcesarEstado(num);

            UserDialogs.Instance.HideLoading();

            if (estado == -1)
            {
                await UserDialogs.Instance.AlertAsync("Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Error!", "Continuar");
            }

            if (estado == 0)
            {
                await UserDialogs.Instance.AlertAsync("Error interno, reinicia la aplicación.", "Error!", "Continuar");
            }

            if (estado == 1)
            {

                VMReto = new RetoVM(Usuario, VMReto.Reto);
                BindingContext = VMReto;

                if (Vista != null)
                {
                    Vista.Actualizar();
                }

            }
        }


        private void Receta_Clicked()
        {
            frameReceta.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {

                    frameReceta.BackgroundColor = Color.FromHex("D8D8D8");
                    await Task.Delay(10);
                    frameReceta.BackgroundColor = Color.White;

                    Receta receta = new Receta()
                    {
                        _IdReceta = Convert.ToInt32(VMReto.Reto._RecetaId)
                    };
                    receta = await App.RecetaService.Obtener(receta);
                    if (receta != null)
                    {

                        await Navigation.PushAsync(new RecetaPage(receta, Usuario));

                    }

                })
            });
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
                    imgRetoPresentacion.Source = ImageSource.FromStream(Foto.GetStream);
                }
            }
            else
            {
                await DisplayAlert("No Camera", "Camera unavailable.", "OK");
            }
        }



        private async Task<bool> ValidarDatos()
        {
            
            if (Foto == null) {

                await UserDialogs.Instance.AlertAsync("Es necesario enviar una foto!.", "Aatención!", "Continuar");                
                return false;
            }
            if (txtComentario.Text == null)
            {
                await UserDialogs.Instance.AlertAsync("Es necesario enviar una comentario!.", "Aatención!", "Continuar");                
                return false;
            }
            if (txtComentario.Text.Count() > 200)
            {
                await UserDialogs.Instance.AlertAsync("El comentario no puede superar los 200 caracteres.", "Aatención!", "Continuar");                
                return false;
            }

            return true;
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


    }
}