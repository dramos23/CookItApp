using Newtonsoft.Json;
using SQLite;
using System;

namespace CookItApp.Models
{
    public class HistorialReceta
    {
        
        public int _IdHistorialReceta { get; set; }
        public string _Email { get; set; }       
        public int _IdReceta { set; get; }
        [JsonIgnore]
        [Ignore]
        public virtual Receta _Receta { get; set; }
        public DateTime _FechaHora { get; set; }

        public HistorialReceta() {

            _FechaHora = DateTime.Today;

        }

        
    }
}
