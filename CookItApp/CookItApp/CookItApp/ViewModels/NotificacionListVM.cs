using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

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
            Notificaciones = new ObservableCollection<Notificacion>();           

            CargarDatos();

        }

        private void CargarDatos()
        {
            try
            {
                Notificaciones.Clear();
                var notificacions = App.DataBase.Notificacion.ObtenerList();
                if (notificacions != null)
                {
                    foreach (var noti in notificacions)
                    {
                        Notificaciones.Add(noti);
                        Vacio = false;
                        Lista = true;
                    }
                }
                else {
                    Vacio = true;
                    Lista = false;
                    Text = Vacio ? "No tiene notificaciones!." : null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
