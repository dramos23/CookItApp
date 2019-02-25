using CookItApp.Models;
using Microsoft.AppCenter.Push;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{    
    public interface IViewMaster
    {
        void Actualizar(Perfil perfil);
        void Gamificacion();

        void Notificacion(PushNotificationReceivedEventArgs e);
    }
}
