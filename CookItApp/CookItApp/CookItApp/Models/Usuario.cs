using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace CookItApp.Models
{
    public class Usuario
    {
        public enum TipoCuenta
        {
            Local = 1,
            Google = 2,
            Facebook = 3
        }

        public enum TipoUsuario
        {
            Desarrollador = 0,
            Adminitrador = 1,
            Cliente = 2,
        }

        [PrimaryKey]
        public string _Email { get; set; }
        public string _IdUsuario { get; set; }
        public System.Guid? _DeviceId { get; set; }
        public TipoCuenta _TipoCuenta { get; set; }
        public TipoUsuario _TipoUsuario { get; set; }
        [Ignore]        
        public string _Password { get; set; }
        public DateTime _UltimoIngreso { get; set; }
        [Ignore]
        [JsonIgnore]
        public Perfil _Perfil { set; get; }
        [Ignore]
        [JsonIgnore]
        public List<HistorialReceta> _ListaHistorialRecetas { get; set; }

        public Usuario() {

            

        }

        public Usuario(string Email, string Password, Guid? DeviceId, DateTime UltimoIngreso)
        {
            _Email = Email;
            _DeviceId = DeviceId;
            _Password = Password;
            _UltimoIngreso = UltimoIngreso;
        }


        public Usuario(string Email, string Password, string text, Guid? DeviceId, TipoCuenta TipoCuenta, TipoUsuario TipoUsuario, DateTime UltimoIngreso)
        {
            _Email = Email;
            _DeviceId = DeviceId;
            _TipoCuenta = TipoCuenta;
            _TipoUsuario = TipoUsuario;
            _Password = Password;
            _UltimoIngreso = UltimoIngreso;
        }

        public bool IsValid()
        {
            return !_Email.Equals("") && !_Password.Equals("") ? true : false;
        }

        public bool RecetaEsFavorita(Receta rec)
        {
            if (_Perfil != null)
            {
                RecetaFavorita recFav = new RecetaFavorita { _IdReceta = rec._IdReceta };
                return _Perfil._ListaRecetasFavoritas.Contains(recFav) ? true : false;
            }
            else {
                return false;
            }
        }

        public bool TieneSuficienteIngrediente(IngredienteReceta ing)
        {
            foreach(IngredienteUsuario ingUs in this._Perfil._ListaIngredientesUsuario)
            {
                if(ing._IdIngrediente == ingUs._IdIngrediente)
                {
                    if (ingUs._Cantidad >= ing._Cantidad) return true;
                    return false;
                }
            }
            return false;
        }

        internal string RevisarDisponibilidadIngrediente(IngredienteReceta ing)
        {
            foreach (IngredienteUsuario ingUs in this._Perfil._ListaIngredientesUsuario)
            {
                if (ing._IdIngrediente == ingUs._IdIngrediente)
                {
                    if (ingUs._Cantidad >= ing._Cantidad) return "Tiene";
                    return "Faltan";
                }
            }
            return "No tiene";
        }


    }
}
