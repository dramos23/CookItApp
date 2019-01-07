using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItWebApi.Models
{

    public class Ingrediente
    {
        //public enum Tipo {
        //    Frutas = 1,
        //    Verduras = 2,
        //    Lacteos = 3,
        //    Carnes = 4,
        //    PescadosMariscos = 5,
        //    Legumbres = 6,
        //    FrutosSecosSemillas = 7,
        //    Cereales = 8,
        //    SalsasAderezos = 9,
        //    AceitesGrasas = 10,
        //    ParaHornear = 11,
        //    EspeciasHierbas = 12
        //}
        //public enum Estacion {
        //    Verano = 1,
        //    Otono = 2,
        //    Invierno = 3,
        //    Primavera = 4,
        //    Varios = 5
        //}

        //1 = mg, 2 = ml
        public enum TipoMedida {
            ml = 1,
            gr = 2
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
        public virtual Estacion _Estacion { set; get; }

        public int _IdTipoIngrediente { set; get; }

        [JsonIgnore]
        public virtual TipoIngrediente _TipoIngrediente { get; set; }

        public Ingrediente() {

        }



    }
}
