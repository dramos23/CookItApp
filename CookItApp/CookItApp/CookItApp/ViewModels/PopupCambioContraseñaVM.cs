using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class PopupCambioContraseñaVM
    {
        public string Email { get; set; }

        public PopupCambioContraseñaVM(Usuario usuario)
        {
            Email = usuario._Email;
        }
    }
}
