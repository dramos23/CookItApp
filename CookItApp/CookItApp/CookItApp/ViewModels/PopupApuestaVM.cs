using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{


    class PopupApuestaVM
    {

        private List<Perfil> Perfiles { get; set; }

        public PopupApuestaVM(List<Perfil> perfiles)
        {

            Perfiles = perfiles;
        }


    }
}
