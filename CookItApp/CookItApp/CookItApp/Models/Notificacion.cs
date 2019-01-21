using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Models
{
    public class Notificacion
    {
        public enum Estado
        {
            SinLeer = 0,
            Leido = 1,
        }

        public string _NotificacionId { get; set; }
        public string _Email { get; set; }
        [JsonIgnore]
        [Ignore]
        public virtual Perfil _Perfil { get; set; }
        public DateTime _FechaHora { get; set; }
        public Estado _Estado { get; set; }
        public string _Titulo { get; set; }
        public string _Descripción { get; set; }

        public Notificacion()
        {
        }
    }
}
