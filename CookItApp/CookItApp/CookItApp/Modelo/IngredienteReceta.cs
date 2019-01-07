using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItWebApi.Models
{

    public class IngredienteReceta : IngredienteConCantidad
    {
        public int _IdReceta { set; get; }

        [JsonIgnore]
        public Receta _Receta { set; get; }

        public IngredienteReceta() : base()
        {

        }

    }
}
