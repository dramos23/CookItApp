using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookItApp.Models
{
    public class Perfil
    {
        public enum Categoria
        {
            Amatér = 1,
            Cocinero = 2,
            SubChef = 3,
            Chef = 4,
            Master = 5
        }
        
        [PrimaryKey]
        public string _Email { get; set; }
        [JsonIgnore]
        [Ignore]
        public virtual Usuario _Usuario { get; set; }
        public byte[] _Foto { set; get; }
        public string _NombreUsuario { set; get; }
        public string _Nombre { set; get; }
        public string _Apellido { set; get; }
        public int _Puntuacion { get; set; }
        public Categoria _Categoria { get; set; }

        public bool _FiltroAutomatico { set; get; }
        public bool _FiltroPrecio { set; get; }
        public int _FiltroPrecioMin { set; get; }
        public int _FiltroPrecioMax { set; get; }
        public bool _FiltroVegetariano { set; get; }
        public bool _FiltroVegano { set; get; }
        public bool _FiltroDiabetico { set; get; }
        public bool _FiltroCeliaco { set; get; }
        public bool _FiltroIngredientes { set; get; }
        public bool _FiltroCalorias { set; get; }
        public int _FiltroCaloriasMin { set; get; }
        public int _FiltroCaloriasMax { set; get; }    
        public bool _FiltroMomentoDia { set; get; }       
        public int? _FiltroMomentoDiaId { set; get; }
        public bool _FiltroPuntuacion { set; get; }
        public int _FiltroPuntuacionMin { set; get; }
        public int _FiltroPuntuacionMax { set; get; }
        public bool _FiltroEstacion { set; get; }
        public int? _FiltroEstacionId { set; get; }
        public bool _FiltroDificultad { set; get; }
        public int _FiltroDificultadMin { set; get; }
        public int _FiltroDificultadMax { set; get; }
        public bool _FiltroCantPlatos { set; get; }
        public int _FiltroCantPlatosMin { set; get; }
        public int _FiltroCantPlatosMax { set; get; }

        public bool _FiltroTiempoPreparacion { set; get; }
        public int _FiltroTiempoPreparacionMin { set; get; }
        public int _FiltroTiempoPreparacionMax { set; get; }
        
        [Ignore]
        
        public List<IngredienteUsuario> _ListaIngredientesUsuario { get; set; }
        [Ignore]
        
        public List<Reto> _ListaRetos { get; set; }
        [Ignore]
        
        public List<Notificacion> _ListaNotificaciones { get; set; }
        [Ignore]
        
        public List<RecetaFavorita> _ListaRecetasFavoritas { get; set; }

        //COMO SQLlite NO PERMITE INSERTAR UN RANGO DE OBJETOS NO GENERICOS
        //CREO UN MÉTODO PARA QUE EL OBJETO INGRESE SU LISTAS.


        public Perfil() {

            _ListaIngredientesUsuario = new List<IngredienteUsuario>();
            _ListaNotificaciones = new List<Notificacion>();
            _ListaRecetasFavoritas = new List<RecetaFavorita>();
            _ListaRetos = new List<Reto>();

        }

        public void InsertarBD()
        {

            int ret = 0;

            ret = App.DataBase.IngredienteUsuario.GuardarList(_ListaIngredientesUsuario); 
            ret = App.DataBase.Reto.GuardarList(_ListaRetos); 
            ret = App.DataBase.Notificacion.GuardarList(_ListaNotificaciones); 
            ret = App.DataBase.RecetaFavorita.GuardarList(_ListaRecetasFavoritas); 
          


        }

        internal void AgregarIngredienteUsuario(IngredienteUsuario ingUs)
        {
            bool ingEncontrado = false;
            foreach(IngredienteUsuario ing in this._ListaIngredientesUsuario)
            {
                if(ingUs.Equals(ing)) ing._Cantidad += ingUs._Cantidad;
                ingEncontrado = true;
            }
            if (!ingEncontrado) _ListaIngredientesUsuario.Add(ingUs);
        }


        internal ImageSource ImageFoto() {

            Image image = new Image();
            Stream stream = new MemoryStream(_Foto);
            image.Source = ImageSource.FromStream(() => { return stream; });
            return image.Source;

        }

        internal async Task<bool> TraerNotificacion(int id)
        {
            var notificacion = await App.NotificacionService.Obtener(id);
            if (notificacion != null)
            {
                App.DataBase.Notificacion.Guardar(notificacion);
                _ListaNotificaciones.Add(notificacion);
                return true;

            }
            return false;
        }

        internal async Task<bool> TraerReto(int id, Views.MasterPage masterPage)
        {
            var reto = await App.RetoService.Obtener(id);
            if (reto != null)
            {               
                App.DataBase.Reto.Guardar(reto);
                _ListaRetos = App.DataBase.Reto.ObtenerList();
                await VerificarGamificacion(reto, masterPage);
                return true;
            }
            return false;
        }

        private async Task VerificarGamificacion(Reto reto, Views.MasterPage masterPage)
        {
            if (reto._IdEstadoReto == 5)
            {
                Dictionary<string, string> dic = await App.PerfilService.Gamificacion(reto);
                if (dic != null)
                {
                    _Categoria = (Perfil.Categoria)Convert.ToInt32(dic["Categoria"]);
                    _Puntuacion = Convert.ToInt32(dic["Puntuacion"]);
                    masterPage.Gamificacion(_Categoria, _Puntuacion);
                }
            }

        }




    }
}
