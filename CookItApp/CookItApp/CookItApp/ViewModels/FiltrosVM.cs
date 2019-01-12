using CookItApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class FiltrosVM
    {
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

        public IPopupFiltros VistaFiltros { set; get; }

        public FiltrosVM(IPopupFiltros vista)
        {
            VistaFiltros = vista;
        }

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

        internal void ResetAll()
        {
            ResetPrecio();
            ResetMomentoDia();
            ResetCalorias();
            ResetEstacion();
            ResetPuntuacion();
            ResetDificultad();
            ResetCantPlatos();
            VistaFiltros.ResetearSimples();
        }

    }
}
