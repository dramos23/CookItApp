using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class ComentarioReceta
    {

        public int _IdReceta { set; get; }
        [JsonIgnore]
        [Ignore]
        public virtual Receta _Receta { set; get; }
        public int _IdComentario { get; set; }
        public string _Email { get; set; }        
        public string _Comentario { get; set; }
        public DateTime _Fecha { get; set; }
        public int _Puntaje { get; set; }

        public ComentarioReceta()
        {

        }

        public ComentarioReceta(int IdComentario, int IdReceta, string EmailUsuario, string Comentario, DateTime Fecha, int Puntaje)
        {
            _IdComentario = IdComentario;
            _IdReceta = _IdReceta;
            _Email = EmailUsuario;
            _Comentario = Comentario;
            _Fecha = Fecha;
            _Puntaje = Puntaje;
        }

        public ComentarioReceta(int IdReceta, string EmailUsuario, string Comentario, DateTime Fecha, int Puntaje)
        {
            _IdReceta = _IdReceta;
            _Email = EmailUsuario;
            _Comentario = Comentario;
            _Fecha = Fecha;
            _Puntaje = Puntaje;
        }

        public void Validar()
        {
            if (this._Comentario.Trim() == "") throw new Exception("Se tiene que insertar un texto para el comentario.");
            if (this._Puntaje < 1) throw new Exception("Se debe elegir un puntaje para la receta.");
        }
    }

}
