using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.ViewModels
{


    class PopupApuestaVM
    {

        public ObservableCollection<Perfil> Perfiles { get; set; }        

        public PopupApuestaVM(List<Perfil> perfiles)
        {            
            Perfiles = new ObservableCollection<Perfil>(perfiles);
        }

        internal async Task<int> CrearReto(Reto reto)
        {
            int idReto = await App.RetoService.Alta(reto);

            if (idReto != -1)
            {

                reto._IdReto = idReto;
                int save = App.DataBase.Reto.Guardar(reto);

                if (save == 0)
                {
                    return 0;
                }

                Guid? uuid = await App.RestService.ObtenerUUID(reto._EmailUsuDes);

                NotificacionAppCenter notificacionAppCenter = new NotificacionAppCenter();
                notificacionAppCenter.CompletarInfo(reto, uuid);                    

                Notificacion notificacion = GenerarNotificacion(reto, notificacionAppCenter);

                int idNotificacion = await App.NotificacionService.Alta(notificacion);

                notificacionAppCenter.Notificacion_content.Custom_Data.Add("NotificacionID", idNotificacion.ToString());
                notificacionAppCenter.Notificacion_content.Custom_Data.Add("RetoID", reto._IdReto.ToString());

                await App.AppCenterNotiService.Enviar(notificacionAppCenter);

                return 1;

            }
            else {

                return -1;

            }
                

        }

        private Notificacion GenerarNotificacion(Reto reto, NotificacionAppCenter notificacionAppCenter)
        {
            Notificacion notificacion = new Notificacion()
            {
                _Email = reto._EmailUsuDes,
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
