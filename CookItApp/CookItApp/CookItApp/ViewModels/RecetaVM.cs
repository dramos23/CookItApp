using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.ViewModels
{
    public class RecetaVM
    {
        public Receta _Receta { get; set; }
        public List<IngredienteReceta> _IngredientesReceta { get; set; }
        public string Titulo { get; set; }

        public RecetaVM(Receta r)
        {
            _IngredientesReceta = r._ListaIngredientesReceta;
            _Receta = r;
            Titulo = "Receta: " + r._Titulo;            
            _Receta.OrdenarListasReceta();            
        }

        internal async Task<List<Perfil>> ObtenerPerfilesBasico(string email)
        {
            var perfiles = await App.PerfilService.ObtenerList();
            perfiles = perfiles.Where(p => p._Email != email).ToList();
            return perfiles;
        }


    }
}
