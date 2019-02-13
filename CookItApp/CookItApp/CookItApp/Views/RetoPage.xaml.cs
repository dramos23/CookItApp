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
        public RetoVM _ViewModelReto { get; set; }

        private Usuario _Usuario { get; set; }

        MediaFile _Foto { get; set; }

        public IViewDesafioList Vista { get; set; }

        public RetoPage (Reto reto, Usuario usuario, IViewDesafioList vista)
		{
			InitializeComponent ();

            Vista = vista;
            _Usuario = usuario;
            Receta_Clicked();
            _ViewModelReto = new RetoVM(usuario, reto);
            BindingContext = _ViewModelReto;
        }

        private async void BtnAceptar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Procesando..");

            Reto reto = _ViewModelReto.Reto;
            reto._IdEstadoReto = 2;
            reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);

            var result = await App.RetoService.Modificar(reto);

            if (result == true)
            {

                App.DataBase.Reto.Modificar(reto);
                
                _ViewModelReto = new RetoVM(_Usuario, reto);
                BindingContext = _ViewModelReto;

               if (Vista != null)
                {
                    Vista.Actualizar();
                }

                UserDialogs.Instance.HideLoading();
            }
            else {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Reto", "Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Ok");
            }

            
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Procesando..");

            Reto reto = _ViewModelReto.Reto;
            reto._IdEstadoReto = 3;
            reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);

            var result = await App.RetoService.Modificar(reto);

            if (result == true)
            {

                App.DataBase.Reto.Modificar(reto);
                UserDialogs.Instance.HideLoading();

                _ViewModelReto = new RetoVM(_Usuario, reto);
                BindingContext = _ViewModelReto;
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Reto", "Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Ok");
            }
        }

        private async void BtnFinalizar_Clicked(object sender, EventArgs e)
        {

            UserDialogs.Instance.ShowLoading("Validando datos..");
            Reto reto = _ViewModelReto.Reto;


            if (ValidarEstadoCuatro())
            {
                reto._IdEstadoReto = 4;
                reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);
                reto._Presentacion = ImageToByteArray();
                reto._ComentarioDestino = txtComentario.Text;

                UserDialogs.Instance.ShowLoading("Procesando..");
                bool result = await App.RetoService.Modificar(reto);

                if (result == true)
                {
                    App.DataBase.Reto.Modificar(reto);
                    UserDialogs.Instance.HideLoading();

                    _ViewModelReto = new RetoVM(_Usuario, reto);
                    BindingContext = _ViewModelReto;
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Reto", "Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Ok");
                }
            }
            UserDialogs.Instance.HideLoading();
                
            
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
                        _IdReceta = Convert.ToInt32(_ViewModelReto.Reto._RecetaId)
                    };
                    receta = await App.RecetaService.Obtener(receta);
                    if (receta != null)
                    {

                        await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

                    }

                })
            });
        }

        private async void BtnSacarFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();


            if (CrossMedia.Current.IsCameraAvailable & CrossMedia.Current.IsTakePhotoSupported)
            {
                _Foto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Name = "miPerfil.jpg",
                    PhotoSize = PhotoSize.Small
                });

                if (_Foto != null)
                {
                    imgRetoPresentacion.Source = ImageSource.FromStream(_Foto.GetStream);
                }
            }
            else
            {
                await DisplayAlert("No Camera", "Camera unavailable.", "OK");
            }
        }



        private bool ValidarEstadoCuatro()
        {
            

            if (_Foto == null) {

                UserDialogs.Instance.AlertAsync("Es necesario enviar una foto!.");
                return false;
            }
            if (txtComentario.Text == null)
            {
                UserDialogs.Instance.AlertAsync("Es necesario enviar una comentario!.");
                return false;
            }
            if (txtComentario.Text.Count() > 200)
            {
                UserDialogs.Instance.AlertAsync("El comentario no puede superar los 200 caracteres.");
                return false;
            }

            return true;
        }

        private async void BtnAprobado_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Procesando..");
            Reto reto = _ViewModelReto.Reto;
            reto._IdEstadoReto = 5;
            var presentacion = reto._Presentacion = null;
            reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);


            bool result = await App.RetoService.Modificar(reto);

            if (result == true)
            {
                reto._Presentacion = presentacion;
                App.DataBase.Reto.Modificar(reto);
                UserDialogs.Instance.HideLoading();
                
                _ViewModelReto = new RetoVM(_Usuario, reto);
                BindingContext = _ViewModelReto;
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Reto", "Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Ok");
            }
            
        }

        private async void BtnRechazado_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Procesando..");
            Reto reto = _ViewModelReto.Reto;
            reto._IdEstadoReto = 6;
            var presentacion = reto._Presentacion = null;
            reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);

            bool result = await App.RetoService.Modificar(reto);

            if (result == true)
            {
                reto._Presentacion = presentacion;
                App.DataBase.Reto.Modificar(reto);
                UserDialogs.Instance.HideLoading();

                _ViewModelReto = new RetoVM(_Usuario, reto);
                BindingContext = _ViewModelReto;
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync("Reto", "Ha ocurrido un error vuelve a intentarlo o ponte en contecto con el administrador.", "Ok");
            }
            
        }

        public byte[] ImageToByteArray()
        {
            using (var memoryStream = new MemoryStream())
            {
                _Foto.GetStream().CopyTo(memoryStream);
                _Foto.Dispose();
                return memoryStream.ToArray();
            }
        }
    }
}