using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        [PrimaryKey]
        public string _NotificacionId { get; set; }
        public string _Email { get; set; }
        [JsonIgnore]
        [Ignore]
        public virtual Perfil _Perfil { get; set; }
        public DateTime _FechaHora { get; set; }
        public Estado _Estado { get; set; }
        public string _Titulo { get; set; }
        public string _Descripcion { get; set; }
        public string _Pk1 { get; set; }
        public string _Pk2 { get; set; }
        public string _Pk3 { get; set; }
        public string _Pk4 { get; set; }
        public string _Tabla { get; set; }

        public Color _Color { get { return _Estado == 0 ? Color.Silver : Color.White; } }

        public Notificacion()
        {
        }
    }
}
