using Newtonsoft.Json;
using SQLite;

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

    }
}
