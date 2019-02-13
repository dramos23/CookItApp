using CookItApp.Models;
using CookItApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarMaps : ContentPage
	{
        
        private MapaVM MapaVM { get; set; }

        public MostrarMaps (Position x, Supermercado supermercado)
		{
			InitializeComponent ();



            //Content = customMap;

            //GenerarMapa(x, supermercado);
            MapaVM = new MapaVM(this, x, supermercado);
            BindingContext = MapaVM;
        }

        //public void GenerarMapa(Position x, Supermercado supermercado)
        //{

        //    Position y = new Position(Convert.ToDouble(supermercado._Latitud), Convert.ToDouble(supermercado._Longitud));

        //    var customMap = new Ubicaciones
        //    {
        //        MapType = MapType.Street,
        //        WidthRequest = App.ScreenWidth,
        //        HeightRequest = App.ScreenHeight

        //    };

        //    var pinOrigen = new Pin
        //    {
        //        Type = PinType.Place,
        //        Position = x,
        //        Label = "Acá estoy yo!",
        //    };

        //    var pinDestino = new Pin
        //    {
        //        Type = PinType.Place,
        //        Position = y,
        //        Label = supermercado._Nombre,
        //        Address = supermercado._Dirección
        //    };

        //    customMap.Pins.Add(pinOrigen);
        //    customMap.Pins.Add(pinDestino);
        //    customMap.RouteCoordinates.Add(x);
        //    customMap.RouteCoordinates.Add(y);

        //    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(x, Distance.FromKilometers(1.0)));

        //}
    }
}