using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class Actualizacion
    {
        private Usuario Usuario { get; set;}

        public Actualizacion(Usuario usuario)
        {
            Usuario = usuario;            
        }

        public void ActualizacionAplicativo()
        {
            ActNotificaciones();
            ActRetos();
            ActHistorialRecetas();
            ActRecetasFavoritas();        
        }

        private async Task ActRecetasFavoritas()
        {
            if (Usuario._Perfil != null)
            {
                List<RecetaFavorita> recetaFavoritas = await App.RecetaFavoritaService.ObtenerList(Usuario._Perfil);

                if (recetaFavoritas != null)
                {
                    App.DataBase.RecetaFavorita.GuardarList(recetaFavoritas);
                }

            }
        }

        private async Task ActHistorialRecetas()
        {
            List<HistorialReceta> historialRecetas = await App.HistorialRecetaService.ObtenerList(Usuario);

            if (historialRecetas != null)
            {
                App.DataBase.HistorialReceta.GuardarList(historialRecetas);
            }
        }

        private async Task ActRetos()
        {
            if (Usuario._Perfil != null)
            {
                List<Reto> retos = await App.RetoService.ObtenerList(Usuario._Perfil);

                if (retos != null)
                {
                    App.DataBase.Reto.GuardarList(retos);
                }

            }
        }

        private async Task ActNotificaciones()
        {
            if (Usuario._Perfil != null)
            {
                List<Notificacion> notificacions = await App.NotificacionService.ObtenerList(Usuario._Perfil);

                if (notificacions != null)
                {
                    App.DataBase.Notificacion.GuardarList(notificacions);
                }

            }
        }
    }
}
