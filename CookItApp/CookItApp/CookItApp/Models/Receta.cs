﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace CookItApp.Models
{
    public class Receta
    {

        public int _IdReceta { get; set; }
        public string _Titulo { get; set; }
        public string _Descripcion { get; set; }

        public int _IdMomentoDia { get; set; }
        [Ignore]
        [JsonIgnore]
        public MomentoDia _MomentoDia { get; set; }

        public int _IdEstacion { get; set; }


        [Ignore]
        [JsonIgnore]
        public Estacion _Estacion { get; set; }
        public int _Dificultad { get; set; }
        public int _TiempoPreparacion { get; set; }
        public int _CantCalorias { get; set; }
        //public Pais _PaisOrigen { get; set; }
        public byte[] _Foto { get; set; }
        public string _Email { get; set; }
        public int _CantPlatos { get; set; }
        public float _Costo { get; set; }
        public DateTime _FechaCreacion { get; set; }
        public double _PuntajeTotal { get; set; }
        public bool _AptoCeliacos { get; set; }
        public bool _AptoDiabeticos { get; set; }
        public bool _AptoVegetarianos { get; set; }
        public bool _AptoVeganos { get; set; }
        public bool _Habilitada { get; set; }

        [JsonIgnore]
        [Ignore]
        public ImageSource _FotoCompleta { get
            {
                Image image = new Image();
                if (_Foto != null)
                {
                    Stream stream = new MemoryStream(_Foto);
                    image.Source = ImageSource.FromStream(() => { return stream; });
                    return image.Source;
                }
                else {
                    image.Source = "fondoFrutillas.jpg";
                    return image.Source;
                }

            }
        }

        [Ignore]
        public List<IngredienteReceta> _ListaIngredientesReceta { set; get; }
        [Ignore]
        public List<PasoReceta> _ListaPasosReceta { set; get; }
        [Ignore]
        public List<ComentarioReceta> _ListaComentariosReceta { set; get; }

        public Receta() {
            _ListaIngredientesReceta = new List<IngredienteReceta>();
            _ListaPasosReceta = new List<PasoReceta>();
            _ListaComentariosReceta = new List<ComentarioReceta>();
        }

        public int InsertarBD()
        {

            int ret = 0;

            if (ret == 0) { ret = App.DataBase.IngredienteReceta.GuardarList(_ListaIngredientesReceta); }
            if (ret == 0) { ret = App.DataBase.PasoReceta.GuardarList(_ListaPasosReceta); }
            if (ret == 0) { ret = App.DataBase.ComentarioReceta.GuardarList(_ListaComentariosReceta); }


            return ret;


        }
        
        //public string _DificultadString
        //{
        //    get
        //    {
        //        return "Dificultad: " + this._Dificultad;
        //    }
        //}
        //public string _PuntajeTotalString
        //{
        //    get
        //    {
        //        return "Puntaje: " + this._PuntajeTotal;
        //    }
        //}

        [Ignore]
        [JsonIgnore]
        public string _RutaFotoPuntajeTotal
        {
            get
            {
                double punt = Math.Round(_PuntajeTotal);
                return "iconoEstrella" + punt + ".png";
            }
        }

        [Ignore]
        [JsonIgnore]
        public string _RutaFotoDificultad
        {
            get
            {
                return "iconoEstrella" + _Dificultad + ".png";
            }
        }

        public ImageSource ImageFoto()
        {

            Image image = new Image();
            Stream stream = new MemoryStream(_Foto);
            image.Source = ImageSource.FromStream(() => { return stream; });
            return image.Source;

        }

        public void OrdenarListasReceta()
        {
            OrdenarListaPasos();
            OrdenarListaComentarios();
        }

        private void OrdenarListaPasos()
        {
            _ListaPasosReceta = _ListaPasosReceta.OrderBy(x => x._IdPasoReceta).ToList();
        }

        private void OrdenarListaComentarios()
        {
            _ListaComentariosReceta = _ListaComentariosReceta.OrderByDescending(x => x._IdComentario).ToList();
        }

    }


}
