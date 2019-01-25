using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class NotificacionVM
    {
        public List<Notificacion>  Notificacions { get; set; }

        public NotificacionVM()
        {            
            Notificacions = App.DataBase.Notificacion.ObtenerList();
        }


    }
}
