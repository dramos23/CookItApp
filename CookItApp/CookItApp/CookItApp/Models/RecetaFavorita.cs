using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class RecetaFavorita
    {
        
        public string _Email { get; set; }
        [JsonIgnore]
        [Ignore]
        public virtual Perfil _Perfil { get; set; }
        public int _IdReceta { set; get; }
        [JsonIgnore]
        [Ignore]
        public virtual Receta _Receta { set; get; }
        public DateTime _FechaHora { get; set; }

        public RecetaFavorita()
        {

        }

        public RecetaFavorita(string Email, int IdReceta, Receta Receta, DateTime FechaHora)
        {
            _Email = Email;
            _IdReceta = IdReceta;
            _Receta = Receta;
            _FechaHora = FechaHora;
        }

        public override bool Equals(object obj)
        {
            RecetaFavorita rec = obj as RecetaFavorita;
            return rec._IdReceta == this._IdReceta;
        }

    }
}
