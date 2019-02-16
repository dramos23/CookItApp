using CookItApp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{

    public class IngredienteReceta : IngredienteConCantidad
    {        

        public int _IdReceta { set; get; }

        [JsonIgnore]
        [Ignore]
        public Receta _Receta { set; get; }

        [JsonIgnore]
        [PrimaryKey]
        public string _ClaveSQLLite
        {
            get {

                return _IdReceta.ToString() + "_" + _IdIngrediente.ToString();

            }
            set {

                _ClaveSQLLite = value;

            }

        }

        public IngredienteReceta() : base()
        {

        }

    }
}
