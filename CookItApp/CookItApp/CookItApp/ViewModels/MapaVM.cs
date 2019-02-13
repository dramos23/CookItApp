using CookItApp.Models;
using CookItApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Ubicaciones GenerarMapa(Position x, Supermercado supermercado)
        {

            Position y = new Position(Convert.ToDouble(supermercado._Latitud), Convert.ToDouble(supermercado._Longitud));

            var customMap = new Ubicaciones
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight

            };

            if (x != null) { 

                var pinOrigen = new Pin
                {
                    Type = PinType.Place,
                    Position = x,
                    Label = "Acá estoy yo!",
                };
                customMap.Pins.Add(pinOrigen);
                customMap.RouteCoordinates.Add(x);

            }

            var pinDestino = new Pin
            {
                Type = PinType.Place,
                Position = y,
                Label = supermercado._Nombre,
                Address = supermercado._Dirección
            };

            
            customMap.Pins.Add(pinDestino);
            
            customMap.RouteCoordinates.Add(y);

            if (x == null)
            {
                x = y;
            }

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(x, Distance.FromKilometers(1.0)));

            return customMap;

        }
    }
}
