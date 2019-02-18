using Acr.UserDialogs;
using CookItApp.Models;
using CookItApp.ViewModels;
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
	public partial class ListaNotificacionesPage : ContentPage
	{

        NotificacionListVM VMNotificacion;
        Usuario _Usuario;

		public ListaNotificacionesPage (Usuario Usuario)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            //Se inicializa el ViewModelUsuario
            VMNotificacion = new NotificacionListVM();
            //Este "BindingContext" le dice a Xamarin que elemento es el que vamos a usar para mostrar cosas visuales en la vista.
            BindingContext = VMNotificacion;
            //Paso siguiente: crear un "ListView" en el .xaml y atarlo a esto.
		}

        private async void IrReceta_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Notificacion notificacion = button?.BindingContext as Notificacion;

            Receta receta = new Receta()
            {
                _IdReceta = Convert.ToInt32(notificacion._Pk2)
            };
            receta = await App.RecetaService.Obtener(receta);
            if (receta != null) {

                await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

            }


        }

        private async void IrReto_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando..");

            Button button = sender as Button;
            Notificacion notificacion = button?.BindingContext as Notificacion;

            Reto reto = new Reto()
            {
                _IdReto = Convert.ToInt32(notificacion._Pk1)
                
            };
            reto = App.DataBase.Reto.Obtener(reto);
            if (reto != null)
            {
                reto._EstadoReto = App.DataBase.EstadoReto.Obtener(reto._IdEstadoReto);
                reto._Receta = App.DataBase.Receta.Obtener(reto._RecetaId);
                UserDialogs.Instance.HideLoading();
                await Navigation.PushAsync(new RetoPage(reto, _Usuario, null));

            }

        }

        //private async void ListaNotificacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    //Se levanta y castea la receta recibida del evento.
        //    if (!(e.SelectedItem is Notificacion notificacion))
        //    {
        //        return;
        //    }

        //    if (notificacion._Estado.Equals(Notificacion.Estado.SinLeer))
        //    {
        //        notificacion._Estado = Notificacion.Estado.Leido;

        //        bool actualizo = await App.NotificacionService.Modificar(notificacion);

        //        if (actualizo)
        //        {
        //            App.DataBase.Notificacion.Modificar(notificacion);

        //            VMNotificacion = new NotificacionListVM();
        //            BindingContext = VMNotificacion;

        //        }

        //    }


        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {           
            Image image = (Image)sender;

            Notificacion notificacion = image.BindingContext as Notificacion;

            if (notificacion._Estado.Equals(Notificacion.Estado.SinLeer))
            {

                bool actualizado = await VMNotificacion.MarcarLeido(notificacion);

                if (actualizado)
                {
                    VMNotificacion = new NotificacionListVM();
                    BindingContext = VMNotificacion;
                }
            }

        }
    }
}