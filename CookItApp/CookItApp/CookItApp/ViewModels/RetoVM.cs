using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.ViewModels
{
    public class RetoVM
    {

        public Reto Reto { get; set; }
        
        public bool EtapaDos { get; set; }
        public bool EtapaTres { get; set; }
        public bool EtapaCuatro { get; set; }
        public bool Finalizado { get; set; }

        public string Acepto { get; set; }
        public string Cancelo { get; set; }
        public string DescDesafio { get; set; }
        public string Preg { get; set; }

        

        public RetoVM(Usuario usuario, Reto reto) {
            
            EtapaDos = false;
            EtapaTres = false;
            EtapaCuatro = false;
            Finalizado = false;            

            Reto = reto;            
            ConfEtapaUno(usuario);
        }


        private void ConfEtapaUno(Usuario usuario)
        {
            switch (Reto._IdEstadoReto)
            {
                case 1:
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        EtapaDos = true;
                        Acepto = "Aceptó el desafío!";
                        Cancelo = "Paso!";
                        Preg = "Desea aceptar el desafío?";
                        DescDesafio = "Si aceptas el desafío deberas preparar la receta indicada, " +
                            "al finalizar deberas sacarte una foto con la presentación del plato y " +
                            "subirla en la aplicacion para que tu retador evalue lo que has " +
                            "preparado.";
                        DescDesafio = (DescDesafio as string).Replace("\\n", Environment.NewLine + Environment.NewLine);
                    }
                    break;
                case 2:
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        EtapaTres = true;
                    }
                    break;
                case 4:
                    if (usuario._Email == Reto._EmailUsuOri)
                    {
                        EtapaCuatro = true;
                    }
                    if (usuario._Email == Reto._EmailUsuDes)
                    {
                        Finalizado = true;
                    }
                    break;
                case 5:
                    Finalizado = true;
                    break;
                case 6:
                    Finalizado = true;
                    break;


            }
            
        }

        internal async Task<int> ProcesarEstado(int num)
        {
            Reto._IdEstadoReto = num;
            Reto._EstadoReto = App.DataBase.EstadoReto.Obtener(Reto._IdEstadoReto);

            byte[] presentacion = null;

            if (num > 4)
            {
                presentacion = Reto._Presentacion;
                Reto._Presentacion = null;
            }

            bool modificado = await App.RetoService.Modificar(Reto);

            if (modificado)
            {
                if (num > 4)
                {
                    Reto._Presentacion = presentacion;
                }

                int save = App.DataBase.Reto.Modificar(Reto);

                if (save == 0)
                {
                    return 0;
                }

                string email = (Reto._IdEstadoReto >= 2 && Reto._IdEstadoReto <= 4) ? Reto._EmailUsuOri : Reto._EmailUsuDes;

                Guid? uuid = await App.RestService.ObtenerUUID(email);

                NotificacionAppCenter notificacionAppCenter = new NotificacionAppCenter();
                notificacionAppCenter.CompletarInfo(Reto, uuid);

                Notificacion notificacion = GenerarNotificacion(Reto, notificacionAppCenter);

                int idNotificacion = await App.NotificacionService.Alta(notificacion);

                notificacionAppCenter.Notificacion_content.Custom_Data.Add("NotificacionID", idNotificacion.ToString());
                notificacionAppCenter.Notificacion_content.Custom_Data.Add("RetoID", Reto._IdReto.ToString());

                await App.AppCenterNotiService.Enviar(notificacionAppCenter);

                return 1;
            }
            else
            {
                return -1;
            }
        }

        private Notificacion GenerarNotificacion(Reto reto, NotificacionAppCenter notificacionAppCenter)
        {
            Notificacion notificacion = new Notificacion()
            {
                _Email = (reto._IdEstadoReto >= 2 && reto._IdEstadoReto <= 4) ? reto._EmailUsuOri : reto._EmailUsuDes,
                _Estado = Notificacion.Estado.SinLeer,
                _FechaHora = DateTime.Now,
                _Titulo = notificacionAppCenter.Notificacion_content.Title,
                _Descripcion = notificacionAppCenter.Notificacion_content.Body,
                _Tabla = "Reto",
                _Pk1 = reto._IdReto.ToString(),
                _Pk2 = reto._RecetaId.ToString()
            };

            return notificacion;

        }
    }
}
