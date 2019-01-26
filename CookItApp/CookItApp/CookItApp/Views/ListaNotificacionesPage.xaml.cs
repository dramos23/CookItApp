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

		public ListaNotificacionesPage (Usuario Usuario)
		{
			InitializeComponent ();
            //Se inicializa el ViewModelUsuario
            _ViewModelNotificacion = new NotificacionListVM();
            //Este "BindingContext" le dice a Xamarin que elemento es el que vamos a usar para mostrar cosas visuales en la vista.
            BindingContext = _ViewModelNotificacion;
            //Paso siguiente: crear un "ListView" en el .xaml y atarlo a esto.
		}
	}
}