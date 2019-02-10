using CookItApp.Controles;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public bool Recordar { get; set;}

        public LoginVM() {

            Email = Settings.UltimoEmailUsado;
            Contraseña = Settings.UltimaPassUsada;
            Recordar = Settings.UltimoEstadoToggle;
            
        }

    }
}
