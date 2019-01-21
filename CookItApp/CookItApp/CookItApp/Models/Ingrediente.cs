using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{

    public class Ingrediente
    {

        public enum TipoMedida
        {
            ml = 1,
            gr = 2,
            un = 3
        }


        public int _IdIngrediente { set; get; }

        public string _Nombre { set; get; }

        public int _Costo { set; get; }

        public TipoMedida _Medida { set; get; }

        public int _MedidaPromedio { set; get; }

        public int _MedidaPorGramo { set; get; }

        public int _CantCaloriasPorMedida { set; get; }
        
        public bool _AptoCeliacos { set; get; }
        
        public bool _AptoDiabeticos{ set; get; }
        
        public bool _AptoVegetarianos { set; get; }

        public bool _AptoVeganos { set; get; }
        
        public int _IdEstacion { set; get; }
        [JsonIgnore]
        [Ignore]
        public Estacion _Estacion { set; get; }

        public int _IdTipoIngrediente { set; get; }
        [JsonIgnore]
        [Ignore]
        public TipoIngrediente _TipoIngrediente { get; set; }

        public Ingrediente() { }


    }
}
