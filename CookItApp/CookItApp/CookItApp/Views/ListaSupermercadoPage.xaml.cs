using Acr.UserDialogs;
using CookItApp.Models;
using CookItApp.ViewModels;
using Plugin.Geolocator.Abstractions;
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
	public partial class ListaSupermercadoPage : ContentPage
	{

        private SupermercadoListVM VMSupermercadoList { get; set; }

        private Position Punto { get; set; }

		public ListaSupermercadoPage (Position position)
		{
			InitializeComponent ();
            Punto = position;
            VMSupermercadoList = new SupermercadoListVM(position);
            BindingContext = VMSupermercadoList;

		}

        private async void BtnVerMapa_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando mapa..");

            Button button = sender as Button;
            Supermercado Super = button?.BindingContext as Supermercado;

            Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position();

            if (Punto != null)
            {
               position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(Punto.Latitude), Convert.ToDouble(Punto.Longitude));
            }

            await Navigation.PushAsync(new MostrarMaps(position, Super));

            UserDialogs.Instance.HideLoading();
        }
    }
}