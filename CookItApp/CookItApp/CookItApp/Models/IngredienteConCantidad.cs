
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{ 
    public abstract class IngredienteConCantidad
    {
        public int _IdIngrediente { set; get; }
  
        public int _Cantidad { get; set; }
        [JsonIgnore]
        [Ignore]
        public Ingrediente _Ingrediente { get; set; }

        [JsonIgnore]
        public string _CantidadMedida { set; get; }

        public IngredienteConCantidad() {

        }



    }
}
