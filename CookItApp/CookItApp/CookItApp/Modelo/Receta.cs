using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CookItWebApi.Models;
using Newtonsoft.Json;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace CookItApp.Models
{ 
    public class Receta
    {
        [PrimaryKey]
        public int _IdReceta { get; set; }
        public string _Titulo { get; set; }
        public string _Descripcion { get; set; }
        [ForeignKey(typeof(MomentoDia))]
        public int _IdMomentoDia { get; set; }
        [Ignore]
        [JsonIgnore]
        public MomentoDia _MomentoDia { get; set; }
        [ForeignKey(typeof(Estacion))]
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
        [Ignore]
        public List<IngredienteReceta> _IngredientesReceta { set; get; }
        [Ignore]
        public List<PasoReceta> _Pasos { set; get; }
        [Ignore]
        public List<ComentarioReceta> _ComentariosReceta { set; get; }


        public Receta() {
            _IngredientesReceta = new List<IngredienteReceta>();
            _Pasos = new List<PasoReceta>();
            _ComentariosReceta = new List<ComentarioReceta>();
        }

        public Receta(int IdReceta, string Titulo, string Descripcion, int IdMomentoDia, int IdEstacion, int Dificultad, 
            int TiempoPreparacion, int CantCalorias, string Email, int CantPlatos, float Costo, 
            DateTime FechaCreacion, double PuntajeTotal, bool AptoCeliacos, bool AptoDiabeticos, bool AptoVegetarianos, 
            bool AptoVeganos, bool Habilitada)
        {
            _IdReceta = IdReceta;
            _Titulo = Titulo;
            _Descripcion = Descripcion;
            _IdMomentoDia = IdMomentoDia;
            _IdEstacion = IdEstacion;
            _Dificultad = Dificultad;
            _TiempoPreparacion = TiempoPreparacion;
            _CantCalorias = CantCalorias;
            _Email = Email;
            _CantPlatos = CantPlatos;
            _Costo = Costo;
            _FechaCreacion = FechaCreacion;
            _PuntajeTotal = PuntajeTotal;
            _AptoCeliacos = AptoCeliacos;
            _AptoDiabeticos = AptoDiabeticos;
            _AptoVegetarianos = AptoVegetarianos;
            _AptoVeganos = AptoVeganos;
            _Habilitada = Habilitada;
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

        //public string _RutaFotoPuntajeTotal
        //{
        //    get
        //    {
        //        double punt = Math.Round(_PuntajeTotal);
        //        return "iconoEstrella" + punt + ".png";
        //    }
        //}

        //public string _RutaFotoDificultad
        //{
        //    get
        //    {
        //        return "iconoEstrella" + _Dificultad + ".png";
        //    }
        //}
    }


}
