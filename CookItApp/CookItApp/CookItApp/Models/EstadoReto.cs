using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class EstadoReto
    {
        [PrimaryKey]
        public int _IdEstadoReto { get; set; }
        public string _Estado { get; set; }

        public EstadoReto() { }

        public EstadoReto(int IdEstadoReto, string Estado)
        {
            _IdEstadoReto = IdEstadoReto;
            _Estado = Estado;
        }
    }
}
