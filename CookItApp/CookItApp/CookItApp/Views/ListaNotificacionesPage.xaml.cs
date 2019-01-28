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

        NotificacionListVM _ViewModelNotificacion;
        Usuario _Usuario;

		public ListaNotificacionesPage (Usuario Usuario)
		{
			InitializeComponent ();
            _Usuario = Usuario;
            //Se inicializa el ViewModelUsuario
            _ViewModelNotificacion = new NotificacionListVM();
            //Este "BindingContext" le dice a Xamarin que elemento es el que vamos a usar para mostrar cosas visuales en la vista.
            BindingContext = _ViewModelNotificacion;
            //Paso siguiente: crear un "ListView" en el .xaml y atarlo a esto.
		}

        private async void IrReceta_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Notificacion notificacion = button?.BindingContext as Notificacion;

            Receta receta = new Receta()
            {
                _IdReceta = Convert.ToInt32(notificacion._Pk3)
            };
            receta = await App.RecetaService.Obtener(receta);
            if (receta != null) {

                await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

            }


        }

        private void IrReto_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Notificacion notificacion = button?.BindingContext as Notificacion;

            Reto reto = new Reto()
            {
                _EmailUsuOri = notificacion._Pk1,
                _EmialUsuDes = notificacion._Pk2,
                _RecetaId    = Convert.ToInt32(notificacion._Pk3),
                _Cumplido    = Convert.ToBoolean(notificacion._Pk4),
            };
            reto = App.DataBase.Reto.Obtener(reto);
            if (reto != null)
            {
                // ABRO  RETO
                //await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

            }

        }

        private async void ListaNotificacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Se levanta y castea la receta recibida del evento.
            if (!(e.SelectedItem is Notificacion notificacion))
            {
                return;
            }

            if (notificacion._Estado.Equals(Notificacion.Estado.SinLeer))
            {
                notificacion._Estado = Notificacion.Estado.Leido;

                bool actualizo = await App.NotificacionService.Modificar(notificacion);

                if (actualizo)
                {
                    App.DataBase.Notificacion.Modificar(notificacion);

                    _ViewModelNotificacion = new NotificacionListVM();                    
                    BindingContext = _ViewModelNotificacion;

                }

            }


        }
    }
}