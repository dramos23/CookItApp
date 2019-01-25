using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.ViewModels
{
    public class NotificacionListVM : BaseViewModel
    {
        public List<Notificacion> Usuarios = new List<Notificacion>();

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public NotificacionListVM()
        {
            Usuarios = App.DataBase.Notificacion.ObtenerList();
            Vacio = (Usuarios.Count == 0) ? true : false;
            Lista = (Usuarios.Count == 0) ? false : true;
            Text = Vacio ? "No tiene notificaciones!." : null;
        }


    }
}
