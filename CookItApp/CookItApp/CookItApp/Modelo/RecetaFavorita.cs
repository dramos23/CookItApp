using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class RecetaFavorita
    {
        public string _Email { get; set; }


        public int _IdReceta { set; get; }

        [JsonIgnore]

        public virtual Usuario _Usuario { get; set; }


        public virtual Receta _Receta { set; get; }


        public DateTime _FechaHora { get; set; }

        public RecetaFavorita()
        {
        }

        public RecetaFavorita(string EmailUsuario, int IdReceta, Receta Receta, DateTime FechaHora)
        {
            _Email = EmailUsuario;
            _IdReceta = IdReceta;
            _Receta = Receta;
            _FechaHora = FechaHora;
        }
    }
}
