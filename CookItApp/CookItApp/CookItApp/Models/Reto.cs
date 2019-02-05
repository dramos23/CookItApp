using Newtonsoft.Json;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace CookItApp.Models
{
    public class Reto
    {
        [PrimaryKey]
        public int _IdReto { get; set; }
        public string _EmailUsuOri { get; set; }
        public string _NomUsuOri { get; set; }
        [JsonIgnore]
        [Ignore]
        public Perfil _PerfilUsuOri { get; set; }
        public string _EmailUsuDes { get; set; }
        public string _NomUsuDes { get; set; }
        [JsonIgnore]
        [Ignore]
        public Perfil _PerfilUsuDes { get; set; }
        public int _RecetaId { get; set; }
        [JsonIgnore]
        [Ignore]
        public Receta _Receta { get; set; }        
        public DateTime _Fecha { get; set; }
        public int _IdEstadoReto { get; set; }
        [JsonIgnore]
        [Ignore]
        public EstadoReto _EstadoReto { get; set; }
        public byte[] _Presentacion { get; set; }
        public int _Puntaje { get; set; }        
        public string _ComentarioOrigen { get; set; }
        public string _ComentarioDestino { get; set; }
        [JsonIgnore]
        [Ignore]
        public ImageSource _Foto { get { return ImageFoto(); } }

        //[JsonIgnore]
        //[PrimaryKey]
        

        public Reto(int IdReto, string EmailUsuOri, string EmailUsuDes, int RecetaId, DateTime Fecha, 
            int IdEstadoReto, byte[] Presentacion, int Puntaje, string ComentarioOrigen, string ComentarioDestino)
        {
            _IdReto = IdReto;
            _EmailUsuOri = EmailUsuOri;
            _EmailUsuDes = EmailUsuDes;
            _RecetaId = RecetaId;
            _Fecha = Fecha;
            _IdEstadoReto = IdEstadoReto;
            _Presentacion = Presentacion;
            _Puntaje = Puntaje;
            _ComentarioOrigen = ComentarioOrigen;
            _ComentarioDestino = ComentarioDestino;
        }

        public Reto() { }

        public ImageSource ImageFoto()
        {
            if (_Presentacion != null)
            {
                Image image = new Image();
                Stream stream = new MemoryStream(_Presentacion);
                image.Source = ImageSource.FromStream(() => { return stream; });
                return image.Source;
            }
            else {
                return null;
            }

        }
    }
}
