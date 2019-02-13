
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{ 
    public abstract class IngredienteConCantidad
    {
        [PrimaryKey]
        public int _IdIngrediente { set; get; }
  
        public int _Cantidad { get; set; }

        public Ingrediente.TipoMedida _Medida { get; set; }


        [JsonIgnore]
        [Ignore]
        public Ingrediente _Ingrediente { get; set; }

        [JsonIgnore]
        [Ignore]
        public string _CantidadMedida { get {return _Cantidad.ToString() + " " + _Medida.ToString(); } }

        public IngredienteConCantidad() {

             

        }

    }
}
