using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
                        noti._Descripcion = ConvertText(noti._Descripcion);
                        Notificaciones.Add(noti);                       
                        Lista = true;
                    }
                }
                else {
                    Vacio = true;                    
                    Text = "No tiene notificaciones!.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private string ConvertText(string value)
        {
            if (value != null)
            {                
                value = (value as string).Replace("\\n", Environment.NewLine + Environment.NewLine);
            }
            return value;
        }

    }
}
