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
	public partial class ListaUsuariosPage : ContentPage
	{

        UserInfoListVM _ViewModelUsuario;

		public ListaUsuariosPage ()
		{
			InitializeComponent ();
            //Se inicializa el ViewModelUsuario
            _ViewModelUsuario = new UserInfoListVM();
            //Este "BindingContext" le dice a Xamarin que elemento es el que vamos a usar para mostrar cosas visuales en la vista.
            BindingContext = _ViewModelUsuario;
            //Paso siguiente: crear un "ListView" en el .xaml y atarlo a esto.
		}
	}
}