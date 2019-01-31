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
            //Comentar despues
            CargarRetoPrueba();
        }

        private void CargarRetoPrueba()
        {
            Reto ret = new Reto
            {
                _ComentarioDestino = "Te desafio",
                _Cumplido = false,
                _EmailUsuOri = "dcazesv@gmail.com",
                _NomUsuOri = "diego",
                _NomUsuDes = "asd",
                _EmailUsuDes = "asd@gmail.com",
                _Fecha = DateTime.Now,
                _Receta = new Receta
                {
                    _Titulo = "Super plato"
                },
            };
            Retos.Add(ret);
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
