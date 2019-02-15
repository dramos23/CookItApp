using CookItApp.Models;
using CookItApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CookItApp.ViewModels
{
    public class MapaVM
    {

        public string Titulo { get; set; }

        public MapaVM(MostrarMaps mostrarMaps, Position x, Supermercado supermercado) {

            mostrarMaps.Content = GenerarMapa(x, supermercado);
            Titulo = "Ubicación: " + supermercado._Nombre;
        }

        public MapaVM(MostrarMaps mostrarMaps, Supermercado supermercado)
        {

            mostrarMaps.Content = GenerarMapa(supermercado);
            Titulo = "Ubicación: " + supermercado._Nombre;
        }

        private View GenerarMapa(Supermercado supermercado)
        {
            Ubicaciones customMap = new Ubicaciones
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight

            };

            Position y = new Position(Convert.ToDouble(supermercado._Latitud), Convert.ToDouble(supermercado._Longitud));

            var pinDestino = new Pin
            {
                Type = PinType.Place,
                Position = y,
                Label = supermercado._Nombre,
                Address = supermercado._Dirección
            };


            customMap.Pins.Add(pinDestino);

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(y, Distance.FromKilometers(1.0)));

            return customMap;
        }

        public Ubicaciones GenerarMapa(Position x, Supermercado supermercado)
        {
            Ubicaciones customMap = new Ubicaciones
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight

            };

            Position y = new Position(Convert.ToDouble(supermercado._Latitud), Convert.ToDouble(supermercado._Longitud));


                

                var pinOrigen = new Pin
                {
                    Type = PinType.Place,
                    Position = x,
                    Label = "Acá estoy yo!",
                };
                customMap.Pins.Add(pinOrigen);
                customMap.RouteCoordinates.Add(x);

                

                var pinDestino = new Pin
                {
                    Type = PinType.Place,
                    Position = y,
                    Label = supermercado._Nombre,
                    Address = supermercado._Dirección
                };


                customMap.Pins.Add(pinDestino);

                customMap.RouteCoordinates.Add(y);

                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(x, Distance.FromKilometers(1.0)));
            
            return customMap;

        }
    }
}
