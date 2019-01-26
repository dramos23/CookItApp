using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CookItApp.Data
{
    public interface IViewIngUsuario
    {
        void AgregarIngrediente(IngredienteUsuario ing);
        void BorrarIngrediente(IngredienteUsuario ing);
        void ActualizarIngrediente(IngredienteUsuario ing);
        void RefrescarListaIng();
    }
}
