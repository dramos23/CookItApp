using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class MasterPageVM
    {
        public string Nombre { get; set; }
        public ImageSource Foto { get; set; }

        public MasterPageVM(Usuario usuario)
        {
            Nombre = usuario._Perfil != null ? (usuario._Perfil._Nombre + " " + usuario._Perfil._Apellido) : usuario._Email;
            Foto = (usuario._Perfil != null && usuario._Perfil._Foto != null) ? usuario._Perfil.ImageFoto() : "img_user.jpg";
        }


    }
}
