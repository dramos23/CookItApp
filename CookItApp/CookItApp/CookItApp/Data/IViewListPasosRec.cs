using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{
    public interface IViewListPasosRec
    {
        void Insertar(PasoReceta pasoReceta);

        void Eliminar(PasoReceta pasoReceta);

        void Actualizar(PasoReceta pasoReceta);

       
    }

}
