using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
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
        public int _NotificacionId { get; set; }
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

        [JsonIgnore]
        [Ignore]
        public string _Desc
        {
            get { return ConvertText(_Descripcion); }
        }

        [JsonIgnore]
        public ImageSource _Foto { get { return _Estado == 0 ? "sinleer.png" : "leido.png"; } }

        public Notificacion()
        {
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
