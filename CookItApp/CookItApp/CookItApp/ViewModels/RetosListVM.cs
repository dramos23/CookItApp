using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace CookItApp.ViewModels
{
    public class RetosListVM
    {
        public ObservableCollection<Reto> Retos { get; set; }

        public RetosListVM() {

            Retos = new ObservableCollection<Reto>();

            CargarDatos();

        }

        private void CargarDatos()
        {
            try
            {
                Retos.Clear();
                var retos = App.DataBase.Reto.ObtenerList();
                if (retos != null)
                {
                    foreach (Reto r in retos)
                    {
                                               
                        r._EstadoReto = App.DataBase.EstadoReto.Obtener(r._IdEstadoReto);
                        r._Receta = App.DataBase.Receta.Obtener(r._RecetaId);
                        Retos.Add(r);

                    }
                }
                else
                {

                    //Text = Vacio ? "No tiene notificaciones!." : null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
