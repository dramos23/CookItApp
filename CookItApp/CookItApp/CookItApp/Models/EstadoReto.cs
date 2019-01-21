using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class EstadoReto
    {
        public int _IdEstadoReto { get; set; }
        public string _Nombre { get; set; }

        public EstadoReto(int IdEstadoReto, string Nombre)
        {
            _IdEstadoReto = IdEstadoReto;
            _Nombre = Nombre;
        }
    }
}
