using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Models
{
    public class Supermercado
    {
        [PrimaryKey]
        public int _IdSupermercado { get; set; }

        public string _Nombre { get; set; }

        public string _Empresa { get; set; }

        public byte[] _Foto { get; set; }

        public string _Dirección { get; set; }

        public string _Telefono { get; set; }

        public Decimal _Longitud { get; set; }

        public Decimal _Latitud { get; set; }
        
        [JsonIgnore]
        [Ignore]
        public String _Distancia { get; set; }
        [JsonIgnore]
        [Ignore]
        public double _DistValor { get; set; }

        private const double EarthRadius = 6371;

        internal void CalcularDistancia(Position position)
        {

            if (_Latitud != 0 && _Longitud != 0)
            {

                var latitud = Convert.ToDouble(_Latitud);
                var longitud = Convert.ToDouble(_Longitud);


                double Lat = (position.Latitude - latitud) * (Math.PI / 180);
                double Lon = (position.Longitude - longitud) * (Math.PI / 180);
                double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(latitud * (Math.PI / 180)) * Math.Cos(position.Latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                _DistValor = Math.Round(EarthRadius * c * 1000, 2);
                _Distancia = "Distancia: " + _DistValor + " mts";

            }
            else
            {
                _Distancia = "No es posible determinar la distancia.";
                _DistValor = 0;
            }
        }

        [JsonIgnore]
        [Ignore]
        public ImageSource _FotoCompleta
        {
            get
            {

                if (_Foto != null)
                {
                    byte[] imageAsBytes = (byte[])_Foto;
                    return ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                else
                {
                    return "noimage.png";
                }

            }
        }



    }
}
