using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class HistorialReceta
    {
        
        public int _IdHistorialReceta { get; set; }
        public string _Email { get; set; }       
        public int _IdReceta { set; get; }
        public virtual Receta _Receta { set; get; }
        public DateTime _FechaHora { get; set; }

        public HistorialReceta() {

            _FechaHora = DateTime.Today;

        }

        public HistorialReceta(int IdHistorialReceta, string EmailUsuario, int IdReceta, Receta Receta, DateTime FechaHora)
        {
            _IdHistorialReceta = IdHistorialReceta;
            _Email = EmailUsuario;
            _IdReceta = IdReceta;
            _Receta = Receta;
            _FechaHora = FechaHora;
        }
    }
}
