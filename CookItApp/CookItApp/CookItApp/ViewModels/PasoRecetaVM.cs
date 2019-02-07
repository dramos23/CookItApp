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
        private int _IdPrimerPaso;
        private int _IdUltimoPaso;

        public PasoRecetaVM(Receta rec, PasoReceta pas)
        {
            this._Receta = rec;
            this._Paso = pas;
            ConseguirIdPrimerUltimoPaso();            
        }

        private void ConseguirIdPrimerUltimoPaso()
        {
            _IdPrimerPaso = _Receta.DevolverPrimerPaso();
            _IdUltimoPaso = _Receta.DevolverUltimoPaso();
        }

        internal bool HaySiguiente()
        {
            if (_Paso._IdPasoReceta == _IdUltimoPaso) return false;
            return true;
        }

        internal bool HayAnterior()
        {
            if (_Paso._IdPasoReceta == _IdPrimerPaso) return false;
            return true;
        }

        internal PasoReceta DevolverProximoPaso()
        {
            if (!HaySiguiente()) throw new Exception("No hay más pasos en la receta.");
            return _Receta.DevolverProximoPaso(_Paso);
        }

        internal PasoReceta DevolverPasoAnterior()
        {
            if (!HaySiguiente()) throw new Exception("Este es el primer paso, no puedes ir hacia atras.");
            return _Receta.DevolverPasoAnterior(_Paso);
        }

        internal string DevolverNumeroPaso()
        {
            string numPas = _Receta.DevolverNumeroPaso(_Paso);
            numPas += "/" + _Receta._ListaPasosReceta.Count;
            return numPas;
        }
    }
}
