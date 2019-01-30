using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class FiltrosVM
    {
        public Usuario Usuario;
        public bool FiltroAutomatico { set; get; }
        public bool FiltroPrecio { set; get; }
        public int FiltroPrecioMin { set; get; }
        public int FiltroPrecioMax { set; get; }
        public bool FiltroVegetariano { set; get; }
        public bool FiltroVegano { set; get; }
        public bool FiltroDiabetico { set; get; }
        public bool FiltroCeliaco { set; get; }
        public bool FiltroCalorias { set; get; }
        public int FiltroCaloriasMin { set; get; }
        public int FiltroCaloriasMax { set; get; }
        public bool FiltroPaisOrigen { set; get; }

        public bool FiltroMomentoDia { set; get; }
        public int FiltroMomentoDiaId { set; get; }
        public bool FiltroPuntuacion { set; get; }
        public int FiltroPuntuacionMin { set; get; }
        public int FiltroPuntuacionMax { set; get; }
        public bool FiltroEstacion { set; get; }
        public int FiltroEstacionId { set; get; }
        public bool FiltroDificultad { set; get; }
        public int FiltroDificultadMin { set; get; }
        public int FiltroDificultadMax { set; get; }
        public bool FiltroCantPlatos { set; get; }
        public int FiltroCantPlatosMin { set; get; }
        public int FiltroCantPlatosMax { set; get; }
        public bool FiltroTiempoPreparacion { set; get; }
        public int FiltroTiempoPreparacionMin { set; get; }
        public int FiltroTiempoPreparacionMax { set; get; }

        private IPopupFiltros VistaFiltros;

        public void SetVistaFiltros(IPopupFiltros vista)
        {
            VistaFiltros = vista;
            TogglearImagenes();
        }

        public FiltrosVM(Usuario usr)
        {
            Usuario = usr;
            CargarFiltrosUsuario();
        }

        #region Carga automática de filtros
        public void CargarFiltrosUsuario()
        {
            if (Usuario._Perfil == null || !Usuario._Perfil._FiltroAutomatico) return;
            CargarFiltroDiabeticoUsuario();
            CargarFiltroCeliacoUsuario();
            CargarFiltroVeganoUsuario();
            CargarFiltroVegetarianoUsuario();
            CargarFiltroCaloriasUsuario();
            CargarFiltroPrecioUsuario();
            CargarFiltroDificultadUsuario();
            CargarFiltroPuntuacionUsuario();
            CargarFiltroCantPlatosUsuario();
            CargarFiltroTiempoPreparacionUsuario();
            CargarFiltroMomentoDia();
            CargarFiltroEstacion();

            if(VistaFiltros != null) TogglearImagenes();
        }

        private void CargarFiltroVegetarianoUsuario()
        {
            if (Usuario._Perfil._FiltroVegetariano) this.FiltroVegetariano = true;
        }

        private void CargarFiltroVeganoUsuario()
        {
            if (Usuario._Perfil._FiltroVegano) this.FiltroVegano = true;
        }

        private void CargarFiltroCeliacoUsuario()
        {
            if (Usuario._Perfil._FiltroCeliaco) this.FiltroCeliaco = true;
        }

        private void CargarFiltroDiabeticoUsuario()
        {
            if (Usuario._Perfil._FiltroDiabetico) this.FiltroDiabetico = true;
        }

        private void CargarFiltroMomentoDia()
        {
            if (Usuario._Perfil._FiltroMomentoDia) {
                this.FiltroMomentoDia = true;
                this.FiltroMomentoDiaId = Convert.ToInt32(Usuario._Perfil._FiltroMomentoDiaId);
            }
        }

        private void CargarFiltroEstacion()
        {
            if (Usuario._Perfil._FiltroEstacion)
            {
                this.FiltroEstacion = true;
                this.FiltroEstacionId = Convert.ToInt32(Usuario._Perfil._FiltroEstacionId);
            }
        }

        private void CargarFiltroCaloriasUsuario()
        {
            if (Usuario._Perfil._FiltroCalorias)
            {
                this.FiltroCalorias = true;
                this.FiltroCaloriasMin = Usuario._Perfil._FiltroCaloriasMin;
                this.FiltroCaloriasMax = Usuario._Perfil._FiltroCaloriasMax;
            }
        }

        private void CargarFiltroDificultadUsuario()
        {
            if (Usuario._Perfil._FiltroDificultad)
            {
                this.FiltroDificultad = true;
                this.FiltroDificultadMin = Usuario._Perfil._FiltroDificultadMin;
                this.FiltroDificultadMax = Usuario._Perfil._FiltroDificultadMax;
            }
        }

        private void CargarFiltroTiempoPreparacionUsuario()
        {
            if (Usuario._Perfil._FiltroTiempoPreparacion)
            {
                this.FiltroTiempoPreparacion = true;
                this.FiltroTiempoPreparacionMin = Usuario._Perfil._FiltroTiempoPreparacionMin;
                this.FiltroTiempoPreparacionMax = Usuario._Perfil._FiltroTiempoPreparacionMax;
            }
        }

        private void CargarFiltroPuntuacionUsuario()
        {
            if (Usuario._Perfil._FiltroPuntuacion)
            {
                this.FiltroPuntuacion = true;
                this.FiltroPuntuacionMin = Usuario._Perfil._FiltroPuntuacionMin;
                this.FiltroPuntuacionMax = Usuario._Perfil._FiltroPuntuacionMax;
            }
        }

        private void CargarFiltroCantPlatosUsuario()
        {
            if (Usuario._Perfil._FiltroCantPlatos)
            {
                this.FiltroCantPlatos = true;
                this.FiltroCantPlatosMin = Usuario._Perfil._FiltroCantPlatosMin;
                this.FiltroCantPlatosMax = Usuario._Perfil._FiltroCantPlatosMax;
            }
        }

        private void CargarFiltroPrecioUsuario()
        {
            if (Usuario._Perfil._FiltroPrecio)
            {
                this.FiltroPrecio = true;
                this.FiltroPrecioMin = Usuario._Perfil._FiltroPrecioMin;
                this.FiltroPrecioMax = Usuario._Perfil._FiltroPrecioMax;
            }
        }
        #endregion

        #region Reseteo de valores de filtros
        internal void ResetTiempoPreparacion()
        {
            FiltroTiempoPreparacionMax = 0;
            FiltroTiempoPreparacionMin = 0;
            FiltroTiempoPreparacion = false;
            VistaFiltros.ToggleImagenTiempoPreparacion(false);
        }

        internal void ResetPrecio()
        {
            FiltroPrecioMax = 0;
            FiltroPrecioMin = 0;
            FiltroPrecio = false;
            VistaFiltros.ToggleImagenPrecio(false);
        }

        internal void ResetCalorias()
        {
            FiltroCaloriasMax = 0;
            FiltroCaloriasMin = 0;
            FiltroCalorias = false;
            VistaFiltros.ToggleImagenCalorias(false);
        }

        internal void ResetCantPlatos()
        {
            FiltroCantPlatosMax = 0;
            FiltroCantPlatosMin = 0;
            FiltroCantPlatos = false;
            VistaFiltros.ToggleImagenCantPlatos(false);
        }

        internal void ResetDificultad()
        {
            FiltroDificultadMax = 0;
            FiltroDificultadMin = 0;
            FiltroDificultad = false;
            VistaFiltros.ToggleImagenDificultad(false);
        }

        internal void ResetPuntuacion()
        {
            FiltroPuntuacionMax = 0;
            FiltroPuntuacionMin = 0;
            FiltroPuntuacion = false;
            VistaFiltros.ToggleImagenPuntuacion(false);
        }

        internal void ResetMomentoDia()
        {
            FiltroMomentoDiaId = 0;
            FiltroPrecio = false;
            VistaFiltros.ToggleImagenMomentoDia(false);
        }
        internal void ResetEstacion()
        {
            FiltroEstacionId = 0;
            FiltroEstacion = false;
            VistaFiltros.ToggleImagenEstacion(false);
        }

        internal void ResetAll()
        {
            ResetPrecio();
            ResetMomentoDia();
            ResetCalorias();
            ResetEstacion();
            ResetPuntuacion();
            ResetDificultad();
            ResetCantPlatos();
            ResetearSimples();
        }

        private void ResetearSimples()
        {
            FiltroVegetariano = false;
            FiltroVegano = false;
            FiltroCeliaco = false;
            FiltroDiabetico = false;
            VistaFiltros.ResetearSimples();
        }
        #endregion

        #region Ingreso de filtros complejos
        internal void IngresarFiltroPrecio(int min, int max)
        {
            FiltroPrecio = true;
            FiltroPrecioMax = max;
            FiltroPrecioMin = min;
            VistaFiltros.ToggleImagenPrecio(true);
        }

        internal void IngresarFiltroPuntuacion(int min, int max)
        {
            FiltroPuntuacion = true;
            FiltroPuntuacionMax = max;
            FiltroPuntuacionMin = min;
            VistaFiltros.ToggleImagenPuntuacion(true);
        }

        internal void IngresarFiltroCalorias(int min, int max)
        {
            FiltroCalorias = true;
            FiltroCaloriasMax = max;
            FiltroCaloriasMin = min;
            VistaFiltros.ToggleImagenCalorias(true);
        }
        internal void IngresarFiltroCantPlatos(int min, int max)
        {
            FiltroCantPlatos = true;
            FiltroCantPlatosMax = max;
            FiltroCantPlatosMin = min;
            VistaFiltros.ToggleImagenCantPlatos(true);
        }
        internal void IngresarFiltroDificultad(int min, int max)
        {
            FiltroDificultad = true;
            FiltroDificultadMax = max;
            FiltroDificultadMin = min;
            VistaFiltros.ToggleImagenDificultad(true);
        }
        internal void IngresarFiltroTiempoPreparacion(int min, int max)
        {
            FiltroTiempoPreparacion = true;
            FiltroTiempoPreparacionMax = max;
            FiltroTiempoPreparacionMin = min;
            VistaFiltros.ToggleImagenTiempoPreparacion(true);
        }
        internal void IngresarFiltroEstacion(int id)
        {
            FiltroEstacion = true;
            FiltroEstacionId = id;
            VistaFiltros.ToggleImagenEstacion(true);
        }
        internal void IngresarFiltroMomentoDia(int id)
        {
            FiltroMomentoDia = true;
            FiltroMomentoDiaId = id;
            VistaFiltros.ToggleImagenMomentoDia(true);
        }
        #endregion


        internal void TogglearImagenes()
        {
            VistaFiltros.ToggleImagenCalorias(FiltroCalorias);
            //VistaFiltros.ToggleImagenCantPlatos(FiltroCantPlatos);
            VistaFiltros.ToggleImagenDificultad(FiltroDificultad);
            VistaFiltros.ToggleImagenPuntuacion(FiltroPuntuacion);
            VistaFiltros.ToggleImagenTiempoPreparacion(FiltroTiempoPreparacion);
            VistaFiltros.ToggleImagenMomentoDia(FiltroMomentoDia);
            VistaFiltros.ToggleImagenEstacion(FiltroEstacion);
            VistaFiltros.ToggleImagenPrecio(FiltroPrecio);
            VistaFiltros.ToggleImagenVegano(FiltroVegano);
            VistaFiltros.ToggleImagenVegetariano(FiltroVegetariano);
            VistaFiltros.ToggleImagenCeliaco(FiltroCeliaco);
            VistaFiltros.ToggleImagenDiabetico(FiltroDiabetico);
        }


    }
}
