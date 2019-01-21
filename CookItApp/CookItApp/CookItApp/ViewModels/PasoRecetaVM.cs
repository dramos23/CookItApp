using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class PasoRecetaVM
    {
        public Receta _Receta;
        public PasoReceta _Paso;

        public PasoRecetaVM(Receta rec, PasoReceta pas)
        {
            this._Receta = rec;
            this._Paso = pas;
        }

        internal bool HaySiguiente()
        {
            if (_Paso._IdPasoReceta == _Receta._ListaPasosReceta.Count) return false;
            return true;
        }

        internal bool HayAnterior()
        {
            if (_Paso._IdPasoReceta == 1) return false;
            return true;
        }
    }
}
