using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class ComentarioReceta
    {

        public int _IdReceta { set; get; }

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
    }

}
