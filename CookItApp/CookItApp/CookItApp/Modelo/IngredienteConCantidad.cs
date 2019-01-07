
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItWebApi.Models
{ 
    public abstract class IngredienteConCantidad
    {
        public int _IdIngrediente { set; get; }
  
        public int _Cantidad { get; set; }
        public Ingrediente _Ingrediente { get; set; }

        [JsonIgnore]
        public string _CantidadMedida { set; get; }

        public IngredienteConCantidad() {

        }



    }
}
