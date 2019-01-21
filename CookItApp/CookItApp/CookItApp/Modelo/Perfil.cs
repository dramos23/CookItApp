using CookItWebApi.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.Models
{
    public class Perfil
    {
        
        [PrimaryKey]
        public string _Email { get; set; }
        public byte[] _Foto { set; get; }
        public string _NombreUsuario { set; get; }
        public string _Nombre { set; get; }
        public string _Apellido { set; get; }

        public bool _FiltroAutomatico { set; get; }
        public bool _FiltroPrecio { set; get; }
        public int _FiltroPrecioMin { set; get; }
        public int _FiltroPrecioMax { set; get; }
        public bool _FiltroVegetariano { set; get; }
        public bool _FiltroVegano { set; get; }
        public bool _FiltroDiabetico { set; get; }
        public bool _FiltroCeliaco { set; get; }
        public bool _FiltroCalorias { set; get; }
        public int _FiltroCaloriasMin { set; get; }
        public int _FiltroCaloriasMax { set; get; }
        public bool _FiltroPaisOrigen { set; get; }
        public int _FiltroPaisOrigenId { set; get; }
        public bool _FiltroMomentoDia { set; get; }       
        public int _FiltroMomentoDiaId { set; get; }
        public bool _FiltroPuntuacion { set; get; }
        public int _FiltroPuntuacionMin { set; get; }
        public int _FiltroPuntuacionMax { set; get; }
        public bool _FiltroEstacion { set; get; }
        public int _FiltroEstacionId { set; get; }
        public bool _FiltroDificultad { set; get; }
        public int _FiltroDificultadMin { set; get; }
        public int _FiltroDificultadMax { set; get; }
        public bool _FiltroCantPlatos { set; get; }
        public int _FiltroCantPlatosMin { set; get; }
        public int _FiltroCantPlatosMax { set; get; }
        public bool _FiltroTiempoPreparacion { set; get; }
        public int _FiltroTiempoPreparacionMin { set; get; }
        public int _FiltroTiempoPreparacionMax { set; get; }
/*
        public List<IngredienteUsuario> _IngredientesUsuario { set; get; }
*/
        public Perfil() { }

        public Perfil(string Email, byte[] Foto, string NombreUsuario, string Nombre, string Apellido, 
            bool FiltroAutomatico, bool FiltroPrecio, int FiltroPrecioMin, int FiltroPrecioMax, 
            bool FiltroVegetariano, bool FiltroVegano, bool FiltroDiabetico, bool FiltroCeliaco, 
            bool FiltroCalorias, int FiltroCaloriasMin, int FiltroCaloriasMax, bool FiltroPaisOrigen, 
            int FiltroPaisOrigenId, bool FiltroMomentoDia, int FiltroMomentoDiaId, bool FiltroPuntuacion, 
            int FiltroPuntuacionMin, int FiltroPuntuacionMax, bool FiltroEstacion, int FiltroEstacionId, 
            bool FiltroDificultad, int FiltroDificultadMin, int FiltroDificultadMax, bool FiltroCantPlatos, 
            int FiltroCantPlatosMin, int FiltroCantPlatosMax, bool FiltroTiempoPreparacion, 
            int FiltroTiempoPreparacionMin, int FiltroTiempoPreparacionMax)
        {
            _Email = Email;
            _Foto = Foto;
            _NombreUsuario = NombreUsuario;
            _Nombre = Nombre;
            _Apellido = Apellido;
            _FiltroAutomatico = FiltroAutomatico;
            _FiltroPrecio = FiltroPrecio;
            _FiltroPrecioMin = FiltroPrecioMin;
            _FiltroPrecioMax = FiltroPrecioMax;
            _FiltroVegetariano = FiltroVegetariano;
            _FiltroVegano = FiltroVegano;
            _FiltroDiabetico = FiltroDiabetico;
            _FiltroCeliaco = FiltroCeliaco;
            _FiltroCalorias = FiltroCalorias;
            _FiltroCaloriasMin = FiltroCaloriasMin;
            _FiltroCaloriasMax = FiltroCaloriasMax;
            _FiltroPaisOrigen = FiltroPaisOrigen;
            _FiltroPaisOrigenId = FiltroPaisOrigenId;
            _FiltroMomentoDia = FiltroMomentoDia;
            _FiltroMomentoDiaId = FiltroMomentoDiaId;
            _FiltroPuntuacion = FiltroPuntuacion;
            _FiltroPuntuacionMin = FiltroPuntuacionMin;
            _FiltroPuntuacionMax = FiltroPuntuacionMax;
            _FiltroEstacion = FiltroEstacion;
            _FiltroEstacionId = FiltroEstacionId;
            _FiltroDificultad = FiltroDificultad;
            _FiltroDificultadMin = FiltroDificultadMin;
            _FiltroDificultadMax = FiltroDificultadMax;
            _FiltroCantPlatos = FiltroCantPlatos;
            _FiltroCantPlatosMin = FiltroCantPlatosMin;
            _FiltroCantPlatosMax = FiltroCantPlatosMax;
            _FiltroTiempoPreparacion = FiltroTiempoPreparacion;
            _FiltroTiempoPreparacionMin = FiltroTiempoPreparacionMin;
            _FiltroTiempoPreparacionMax = FiltroTiempoPreparacionMax;
        }

        private Dictionary<string, string> GenerarFiltro()
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            if (this._FiltroAutomatico == true)
            {
                if (_FiltroPrecio == true)
                {
                    diccionario.Add("CostoMayorIgual", _FiltroPrecioMin.ToString());
                    diccionario.Add("CostoMenorIgual", _FiltroPrecioMax.ToString());                                        
                }
                if (_FiltroVegetariano == true)
                {
                    diccionario.Add("AptoVegetariano", _FiltroVegetariano.ToString());                    
                }
                if (_FiltroVegano == true)
                {
                    diccionario.Add("AptoVegano", _FiltroVegano.ToString());
                }
                if (_FiltroDiabetico == true)
                {
                    diccionario.Add("AptoDiabetico", _FiltroDiabetico.ToString());
                }
                if (_FiltroCeliaco == true)
                {
                    diccionario.Add("AptoCeliaco", _FiltroCeliaco.ToString());
                }
                if (_FiltroCalorias == true)
                {
                    diccionario.Add("CaloriasMayorIgual", _FiltroCaloriasMin.ToString());
                    diccionario.Add("CaloriasMenorIgual", _FiltroCaloriasMax.ToString());
                }
                if (_FiltroMomentoDia == true)
                {
                    diccionario.Add("MomentoDia", _FiltroMomentoDiaId.ToString());
                }
                if (_FiltroPuntuacion == true)
                {
                    diccionario.Add("PuntajeMayorIgual", _FiltroPuntuacionMin.ToString());
                    diccionario.Add("PuntajeMenorIgual", _FiltroPuntuacionMax.ToString());
                }
                if (_FiltroEstacion == true)
                {
                    diccionario.Add("Estacion", _FiltroEstacionId.ToString());
                }
                if (_FiltroDificultad == true)
                {
                    diccionario.Add("DificultadMayorIgual", _FiltroDificultadMin.ToString());
                    diccionario.Add("DificultadMenorIgual", _FiltroDificultadMax.ToString());
                }
                if (_FiltroCantPlatos == true)
                {
                    diccionario.Add("CantPlatosMayorIgual", _FiltroCantPlatosMin.ToString());
                    diccionario.Add("CantPlatosMenorIgual", _FiltroCantPlatosMax.ToString());
                }
                if (_FiltroTiempoPreparacion  == true)
                {
                    diccionario.Add("TiempoPreparacionMayorIgual", _FiltroTiempoPreparacionMin.ToString());
                    diccionario.Add("TiempoPreparacionMenorIgual", _FiltroTiempoPreparacionMax.ToString());
                }
                if (_FiltroPaisOrigen == true)
                {
                    diccionario.Add("Pais", _FiltroPaisOrigenId.ToString());
                }
                //_FiltroPaisOrigen = FiltroPaisOrigen;

            }
            return diccionario;
        }

        public ImageSource ImageFoto() {

            Image image = new Image();
            Stream stream = new MemoryStream(_Foto);
            image.Source = ImageSource.FromStream(() => { return stream; });
            return image.Source;

        }



    }
}
