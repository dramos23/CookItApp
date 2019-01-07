using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //Sacar datos de prueba despues de que esten hechos los de verdad.
            GenerarDatosPrueba();
            CargarDatos(r);
        }

        private void GenerarDatosPrueba()
        {
            _Receta._ComentariosReceta.Add(new ComentarioReceta(_Receta._IdReceta, "pepe@gmail.com", "Me encanto esta receta, es facil de hacer y muy rica.", DateTime.Now, 4));
            _Receta._ComentariosReceta.Add(new ComentarioReceta(_Receta._IdReceta, "griselda@gmail.com", "Excelente receta, me encanto. Espero ver mas de este cocinero", DateTime.Now, 5));
            _Receta._ComentariosReceta.Add(new ComentarioReceta(_Receta._IdReceta, "martina@gmail.com", "Esta muy mal explicada, no entendi nada", DateTime.Now, 2));
        }

        public void CargarDatos(Receta r)
        {
            if (_ComentariosReceta.Count != 0) return;
            foreach (ComentarioReceta cr in r._ComentariosReceta)
            {
                this._ComentariosReceta.Add(cr);
            }
        }

        internal async void InsertarComentario(ComentarioReceta comentarioCreado)
        {
            try {
                await App.ComentarioRecetaService.Alta(comentarioCreado);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
