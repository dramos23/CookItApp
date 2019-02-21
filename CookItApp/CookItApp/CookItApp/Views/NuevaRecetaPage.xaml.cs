using Acr.UserDialogs;
using Android.Graphics;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Java.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
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
	public partial class NuevaRecetaPage : ContentPage, IViewNuevaReceta
	{
        private NuevaRecetaVM VMNuevaReceta { get; set; }       
        
        private IViewMisRecetas Vista { get; set; }
        private MediaFile Foto { get; set; }

        public NuevaRecetaPage (Usuario usuario, IViewMisRecetas vista )
		{
            Vista = vista;
			InitializeComponent ();
            VMNuevaReceta = new NuevaRecetaVM(usuario);
            BindingContext = VMNuevaReceta;
		}

        private async void BtnPasosReceta_Clicked(object sender, EventArgs e)
        {
            List<PasoReceta> pasosReceta = VMNuevaReceta.Receta._ListaPasosReceta;
            await Navigation.PushAsync(new ListPasosRecetaPage(pasosReceta, this));
        }

        private async void BtnIngredientes_Clicked(object sender, EventArgs e)
        {
            List<IngredienteReceta> ingredienteRecetas = VMNuevaReceta.Receta._ListaIngredientesReceta;
            await Navigation.PushAsync(new ListIngredientesRecetaPage(ingredienteRecetas, this));
        }

        private async void BtnSubirReceta_Clicked(object sender, EventArgs e)
        {


            if (await ValidarDatos())
            {
                UserDialogs.Instance.ShowLoading("Subiendo Receta...");

                MediaFile foto = Foto;
                string Titulo = entryTitulo.Text;
                string Descripcion = entryDescripcion.Text;
                int Tiempo = Convert.ToInt32(entryTiempo.Text);
                Dificultad dificultad = picDificultad.ItemsSource[picDificultad.SelectedIndex] as Dificultad;
                int Dificultad = dificultad.value;
                MomentoDia momento = picMomentoDia.ItemsSource[picMomentoDia.SelectedIndex] as MomentoDia;
                int Momento = momento._IdMomentoDia;
                Estacion estacion = picEstacion.ItemsSource[picEstacion.SelectedIndex] as Estacion;
                int Estacion = estacion._IdEstacion;


                VMNuevaReceta.CargarDatosReceta(foto, Titulo, Descripcion, Tiempo, Dificultad, Momento, Estacion);
                int estado = await VMNuevaReceta.SubirReceta();

                if (estado == 1)
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Se ha dado de alta tú receta, en cuanto la habiliten ya la podras ver en el listado!.", "Receta", "Continuar");
                    Vista.Actualizar();
                }
                if (estado == 2)
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Se ha producido un error, vuelva a intentarlo.", "Error!", "Continuar");
                }
                if (estado == 3)
                {
                    UserDialogs.Instance.HideLoading();
                    await UserDialogs.Instance.AlertAsync("Error interno, reinicia la aplicación.", "Error!", "Continuar");
                }
                
            }
            
        }

        private async void BtnSacarFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable & CrossMedia.Current.IsTakePhotoSupported)
            {
                Foto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    Name = "miReceta.jpg",
                    PhotoSize = PhotoSize.Large
                });

                if (Foto != null)
                {
                    imgPresentacion.Source = ImageSource.FromStream(Foto.GetStream);
                }
            }
            else
            {                
                await UserDialogs.Instance.AlertAsync("Se ha producido un error con el dispositivo!.", "Error!.", "Continuar");
            }
        }

        public void CargarIngredientes(List<IngredienteReceta> ingredientes)
        {
            VMNuevaReceta.AgregarIngredientes(ingredientes);
        }

        public void CargarPasos(List<PasoReceta> pasos)
        {
            VMNuevaReceta.AgregarPasos(pasos);
        }

        private async Task<bool> ValidarDatos()
        {
            ImageSource imagen_defecto = "noimage.png";


            if (imgPresentacion.Source == null || imgPresentacion.Source.Equals(imagen_defecto))
            {
                await UserDialogs.Instance.AlertAsync("Es necesario enviar una foto!.", "Atención!", "Continuar");
                return false;
            }
            if (entryTitulo.Text == null)
            {
                await UserDialogs.Instance.AlertAsync("Es necesario ingresar el titulo!.", "Atención!", "Continuar");
                return false;
            }
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

            if (!(picDificultad.ItemsSource[picDificultad.SelectedIndex] is Dificultad dificultad))
            {
                await UserDialogs.Instance.AlertAsync("Debe seleccionar un nivel de dificultad!.", "Atención!", "Continuar");
                return false;
            }
            if (!(picMomentoDia.ItemsSource[picMomentoDia.SelectedIndex] is MomentoDia momento))
            {
                await UserDialogs.Instance.AlertAsync("Debe seleccionar un momento del día!.", "Atención!", "Continuar");
                return false;
            }
            if (!(picEstacion.ItemsSource[picEstacion.SelectedIndex] is Estacion estacion))
            {
                await UserDialogs.Instance.AlertAsync("Debe seleccionar una estacion de año!.", "Atención!", "Continuar");
                return false;
            }
            if (VMNuevaReceta.Receta._ListaIngredientesReceta == null)
            {
                await UserDialogs.Instance.AlertAsync("Debe dar de alta los ingredientes de la receta!.", "Atención!", "Continuar");
                return false;
            }
            if (VMNuevaReceta.Receta._ListaPasosReceta == null)
            {
                await UserDialogs.Instance.AlertAsync("Debe dar de alta los pasos de la receta!.", "Atención!", "Continuar");
                return false;
            }

            return true;
        }

    }
}