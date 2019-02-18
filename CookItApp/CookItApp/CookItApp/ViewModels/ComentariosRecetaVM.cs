using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class ComentariosRecetaVM
    {
        public Receta _Receta { get; set; }
        public ObservableCollection<ComentarioReceta> _ComentariosReceta { get; set; }

        public ComentariosRecetaVM(Receta r)
        {
            this._ComentariosReceta = new ObservableCollection<ComentarioReceta>();
            this._Receta = r;
            GenerarListaObservable();
        }


        private void GenerarListaObservable()
        {
            _ComentariosReceta.Clear();
            foreach (ComentarioReceta cr in _Receta._ListaComentariosReceta)
            {
                this._ComentariosReceta.Add(cr);
            }

        }

        internal void AgregarComentario(ComentarioReceta nuevo)
        {
            _ComentariosReceta.Add(nuevo);
            GenerarListaObservable();
        }
    }
}
