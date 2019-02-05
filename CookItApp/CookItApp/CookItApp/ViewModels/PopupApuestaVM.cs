using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{


    class PopupApuestaVM
    {

        public ObservableCollection<Perfil> Perfiles { get; set; }        

        public PopupApuestaVM(List<Perfil> perfiles)
        {            
            Perfiles = new ObservableCollection<Perfil>(perfiles);
        }

    }
}
