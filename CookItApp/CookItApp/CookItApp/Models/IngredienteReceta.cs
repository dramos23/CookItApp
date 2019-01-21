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

        public IngredienteReceta() : base()
        {

        }

    }
}
