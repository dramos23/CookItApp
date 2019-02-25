using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class PerfilVM
    {
        public string Email { get; set; }
        public ImageSource Foto { get; set; }
        public string NombreUsuario { set; get; }
        public string Nombre { set; get; }
        public string Apellido { set; get; }
        public int Puntuación { get; set; }
        public string Categoría { get; set; }

        public bool FiltroAutomatico { set; get; }
        public bool FiltroPrecio { set; get; }
        public int FiltroPrecioMin { set; get; }
        public int FiltroPrecioMax { set; get; }
        public bool FiltroVegetariano { set; get; }
        public bool FiltroVegano { set; get; }
        public bool FiltroDiabetico { set; get; }
        public bool FiltroCeliaco { set; get; }
        public bool FiltroIngredientes { set; get; }
        public bool FiltroCalorias { set; get; }
        public int FiltroCaloriasMin { set; get; }
        public int FiltroCaloriasMax { set; get; }
        public bool FiltroPaisOrigen { set; get; }
        public bool FiltroMomentoDia { set; get; }
        public int? FiltroMomentoDiaId { set; get; }
        public bool FiltroPuntuacion { set; get; }
        public int FiltroPuntuacionMin { set; get; }
        public int FiltroPuntuacionMax { set; get; }
        public bool FiltroEstacion { set; get; }
        public int? FiltroEstacionId { set; get; }
        public bool FiltroDificultad { set; get; }
        public int FiltroDificultadMin { set; get; }
        public int FiltroDificultadMax { set; get; }
        public bool FiltroCantPlatos { set; get; }
        public int FiltroCantPlatosMin { set; get; }
        public int FiltroCantPlatosMax { set; get; }
        public bool FiltroTiempoPreparacion { set; get; }
        public int FiltroTiempoPreparacionMin { set; get; }
        public int FiltroTiempoPreparacionMax { set; get; }        
        
        public List<Estacion> ItemsEstacion { get; set; }
        public List<MomentoDia> ItemsMomentoDia { get; set; }

        private Usuario Usuario { get; set; }

        public PerfilVM(Usuario usuario) {

            Usuario = usuario;

            CargarPickerEstacion();
            CargarPickerMomentoDia();
            CargarDatos(usuario);
        }

        private void CargarDatos(Usuario usuario)
        {
            Email = usuario._Email;
            Foto = (usuario._Perfil != null && usuario._Perfil._Foto != null) ? usuario._Perfil.ImageFoto() : "img_user.png";
            NombreUsuario = usuario._Perfil?._NombreUsuario;
            Nombre = usuario._Perfil?._Nombre;
            Apellido = usuario._Perfil?._Apellido;
            Puntuación = (usuario._Perfil != null) ? usuario._Perfil._Puntuacion : 0;
            Categoría = (usuario._Perfil != null) ? usuario._Perfil._Categoria.ToString() : Perfil.Categoria.Amatér.ToString();

            FiltroAutomatico = (usuario._Perfil != null) ? usuario._Perfil._FiltroAutomatico : false;
            FiltroPrecio = (usuario._Perfil != null) ? usuario._Perfil._FiltroPrecio : false;
            FiltroPrecioMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroPrecioMin : 1;
            FiltroPrecioMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroPrecioMax : 9999;
            FiltroVegetariano = (usuario._Perfil != null) ? usuario._Perfil._FiltroVegetariano : false;
            FiltroVegano = (usuario._Perfil != null) ? usuario._Perfil._FiltroVegano : false;
            FiltroDiabetico = (usuario._Perfil != null) ? usuario._Perfil._FiltroDiabetico : false;
            FiltroCeliaco = (usuario._Perfil != null) ? usuario._Perfil._FiltroCeliaco : false;
            FiltroIngredientes = (usuario._Perfil != null) ? usuario._Perfil._FiltroIngredientes : false;
            FiltroCalorias = (usuario._Perfil != null) ? usuario._Perfil._FiltroCalorias : false;
            FiltroCaloriasMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroCaloriasMin : 1;
            FiltroCaloriasMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroCaloriasMax : 9999;
            FiltroMomentoDia = (usuario._Perfil != null) ? usuario._Perfil._FiltroMomentoDia : false;
            FiltroMomentoDiaId = (usuario._Perfil != null) ? BuscarIndexPickerMomento(usuario._Perfil._FiltroMomentoDiaId) : 0;
            FiltroPuntuacion = (usuario._Perfil != null) ? usuario._Perfil._FiltroPuntuacion : false;
            FiltroPuntuacionMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroPuntuacionMin : 1;
            FiltroPuntuacionMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroPuntuacionMax : 5;
            FiltroEstacion = (usuario._Perfil != null) ? usuario._Perfil._FiltroEstacion : false;
            FiltroEstacionId = (usuario._Perfil != null) ? BuscarIndexPickerEstacion(usuario._Perfil._FiltroEstacionId) : 0;
            FiltroDificultad = (usuario._Perfil != null) ? usuario._Perfil._FiltroDificultad : false;
            FiltroDificultadMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroDificultadMin : 1;
            FiltroDificultadMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroDificultadMax : 5;
            FiltroCantPlatos = (usuario._Perfil != null) ? usuario._Perfil._FiltroCantPlatos : false;
            FiltroCantPlatosMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroCantPlatosMin : 1;
            FiltroCantPlatosMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroCantPlatosMax : 1;
            FiltroTiempoPreparacion = (usuario._Perfil != null) ? usuario._Perfil._FiltroTiempoPreparacion : false;
            FiltroTiempoPreparacionMin = (usuario._Perfil != null) ? usuario._Perfil._FiltroTiempoPreparacionMin : 1;
            FiltroTiempoPreparacionMax = (usuario._Perfil != null) ? usuario._Perfil._FiltroTiempoPreparacionMax : 9999;

            
        }

        private void CargarPickerEstacion()
        {
            List<Estacion> estaciones = App.DataBase.Estacion.ObtenerList();
            if (estaciones != null && estaciones.Count > 0)
            {
                ItemsEstacion = new List<Estacion>(estaciones);
            }
            else
            {
                ItemsEstacion = new List<Estacion>();
            }
        }

        private void CargarPickerMomentoDia()
        {
            List<MomentoDia> momentoDias = App.DataBase.MomentoDia.ObtenerList();
            if (momentoDias != null && momentoDias.Count > 0)
            {
                ItemsMomentoDia = new List<MomentoDia>(momentoDias);
            }
            else
            {
                ItemsMomentoDia = new List<MomentoDia>();
            }

        }

        public async Task<int> GuardarPerfil(Perfil perfil) {

            bool estado = false;

            if (Usuario._Perfil == null)
            {
                estado = await App.PerfilService.Alta(perfil);
            }
            else
            {
                estado = await App.PerfilService.Modificar(perfil);
            }


            if (estado)
            {
                if (Usuario._Perfil == null)
                {
                    return App.DataBase.Perfil.Guardar(perfil);
                }
                else
                {
                    return App.DataBase.Perfil.Modificar(perfil);
                }
            }
            else
            {
                return 2;                
            }            

        }

        private int BuscarIndexPickerEstacion(int? id)
        {
            return ItemsEstacion.FindIndex(i => i._IdEstacion == id);
        }

        private int BuscarIndexPickerMomento(int? id)
        {
            return ItemsMomentoDia.FindIndex(i => i._IdMomentoDia == id);
        }
    }
}
