using CookItWebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class IngredienteUsuario : IngredienteConCantidad
    {        
        public string _Email { set; get; }

        public Usuario _UserInfo { set; get; }
    }
}
