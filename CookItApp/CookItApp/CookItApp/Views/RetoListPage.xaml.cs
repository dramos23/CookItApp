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
	public partial class RetoListPage : ContentPage
	{
        RetosListVM _ViewModelRetosList;

        Usuario _Usuario;

        public RetoListPage (Usuario usuario)
		{            
			InitializeComponent ();

            _Usuario = usuario;

            _ViewModelRetosList = new RetosListVM();
            BindingContext = _ViewModelRetosList;

        }

        private async void IrReceta_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Reto reto = button?.BindingContext as Reto;

            Receta receta = new Receta()
            {
                _IdReceta = Convert.ToInt32(reto._RecetaId)
            };
            receta = await App.RecetaService.Obtener(receta);
            if (receta != null)
            {

                await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

            }
        }

        private async void IrReto_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button?.BindingContext is Reto reto)
            {
                await Navigation.PushAsync(new RetoPage(reto, _Usuario));
            }
        }
    }
}