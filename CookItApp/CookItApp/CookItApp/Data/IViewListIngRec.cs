using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{
    public interface IViewListIngRec
    {
        void Insertar(IngredienteReceta ingredienteReceta);

        void Eliminar(IngredienteReceta ingredienteReceta);

        void Actualizar(IngredienteReceta ingredienteReceta);

        bool Existe(int idIngrediente);
    }
}
