using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{
    public interface IPopupFiltros
    {
        void ToggleImagenPrecio(bool v);
        void ToggleImagenTiempoPreparacion(bool v);
        void ToggleImagenCalorias(bool v);
        void ToggleImagenCantPlatos(bool v);
        void ToggleImagenDificultad(bool v);
        void ToggleImagenPuntuacion(bool v);
        void ToggleImagenMomentoDia(bool v);
        void ToggleImagenEstacion(bool v);
        void ResetearSimples();
    }
}
