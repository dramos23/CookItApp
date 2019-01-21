using CookItApp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class Reto
    {
        public string _EmailUsuOri { get; set; }
        [JsonIgnore]
        [Ignore]
        public Perfil _PerfilUsuOri { get; set; }
        public string _EmialUsuDes { get; set; }
        [JsonIgnore]
        [Ignore]
        public Perfil _PerfilUsuDes { get; set; }
        public int _RecetaId { get; set; }
        [JsonIgnore]
        [Ignore]
        public Receta _Receta { get; set; }
        public bool _Cumplido { get; set; }
        public DateTime _Fecha { get; set; }
        public int _IdEstadoReto { get; set; }
        [JsonIgnore]
        [Ignore]
        public EstadoReto _EstadoReto { get; set; }
        public byte[] _Presentacion { get; set; }
        public int _Puntaje { get; set; }        
        public string _ComentarioOrigen { get; set; }
        public string _ComentarioDestino { get; set; }

        public Reto(string EmailUsuOri, string EmialUsuDes, int RecetaId, bool Cumplido, DateTime Fecha, 
            int IdEstadoReto, byte[] Presentacion, int Puntaje, string ComentarioOrigen, string ComentarioDestino)
        {
            _EmailUsuOri = EmailUsuOri;
            _EmialUsuDes = EmialUsuDes;
            _RecetaId = RecetaId;
            _Cumplido = Cumplido;
            _Fecha = Fecha;
            _IdEstadoReto = IdEstadoReto;
            _Presentacion = Presentacion;
            _Puntaje = Puntaje;
            _ComentarioOrigen = ComentarioOrigen;
            _ComentarioDestino = ComentarioDestino;
        }

        public Reto() { }
    }
}
