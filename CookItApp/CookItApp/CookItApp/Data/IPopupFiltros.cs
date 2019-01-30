using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{
    public interface IPopupFiltros
    {
        void ToggleImagenVegetariano(bool v);
        void ToggleImagenVegano(bool v);
        void ToggleImagenCeliaco(bool v);
        void ToggleImagenDiabetico(bool v);
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
