using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.Data
{
    public interface IViewNuevaReceta
    {
        void CargarIngredientes(List<IngredienteReceta> ingredientes);

        void CargarPasos(List<PasoReceta> pasos);

    }
}
