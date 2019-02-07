using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;

namespace CookItApp.Models
{

    public class PasoReceta
    {
        public int _IdReceta { set; get; }
        [JsonIgnore]
        [Ignore]
        public virtual Receta _Receta { set; get; }        
        public int _IdPasoReceta { set; get; }
        
        public string _Descripcion { set; get; }
        public int _TiempoReloj { set; get; }
        public string _UrlVideo { set; get; }
        public byte[] _Foto { set; get; }

        internal ImageSource GenerarFoto()
        {
            Image image = new Image();
            if (_Foto != null)
            {
                Stream stream = new MemoryStream(_Foto);
                image.Source = ImageSource.FromStream(() => { return stream; });
                return image.Source;
            }
            else
            {
                return null;
            }
        }
    }
}
