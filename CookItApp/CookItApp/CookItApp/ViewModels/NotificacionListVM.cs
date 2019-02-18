using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class NotificacionListVM : BaseViewModel
    {
        public ObservableCollection<Notificacion> Notificaciones { get; set; }

        public bool Vacio { get; set; }

        public bool Lista { get; set; }

        public string Text { get; set; }

        public NotificacionListVM()
        {
            Vacio = false;
            Lista = false;
                  

            CargarDatos();

        }

        private void CargarDatos()
        {

            List<Notificacion> notificaciones = App.DataBase.Notificacion.ObtenerList();

            if (notificaciones != null && notificaciones.Count > 0)
            {
                Notificaciones = new ObservableCollection<Notificacion>(notificaciones);
                Lista = true;
            }
            else
            {
                Notificaciones = new ObservableCollection<Notificacion>();
                Vacio = true;
                Text = "No tiene notificaciones!.";
            }
        }

        internal async Task<bool> MarcarLeido(Notificacion notificacion)
        {
            notificacion._Estado = Notificacion.Estado.Leido;

            bool actualizo = await App.NotificacionService.Modificar(notificacion);

            if (actualizo)
            {
                App.DataBase.Notificacion.Modificar(notificacion);
                return true;
            }
            return false;
        }
    }
}
