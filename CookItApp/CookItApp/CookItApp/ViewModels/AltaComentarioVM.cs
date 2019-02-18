using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookItApp.ViewModels
{
    public class AltaComentarioVM
    {
        public Usuario _Usuario;
        public Receta _Receta;

        public AltaComentarioVM(Usuario usr, Receta rec)
        {
            _Usuario = usr;
            _Receta = rec;
        }

        internal async void IngresarComentario(ComentarioReceta comentario)
        {
            try
            {
                comentario.Validar();
                await App.ComentarioRecetaService.Alta(comentario);
                _Receta._ListaComentariosReceta.Add(comentario);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
