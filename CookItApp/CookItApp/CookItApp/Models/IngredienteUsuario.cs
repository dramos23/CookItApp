using CookItApp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class IngredienteUsuario : IngredienteConCantidad
    {        
        public string _Email { set; get; }
        [JsonIgnore]
        [Ignore]
        public Perfil _Perfil { set; get; }
    }
}
