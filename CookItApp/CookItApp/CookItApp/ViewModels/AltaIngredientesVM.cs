using CookItApp.Data;
using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CookItApp.ViewModels
{
    public class AltaIngredientesVM
    {
        public Usuario Usuario;
        public IViewIngUsuario Vista;
        public ObservableCollection<Ingrediente> Ingredientes { get; set; }

        public AltaIngredientesVM(Usuario usr, IViewIngUsuario vista)
        {
            Usuario = usr;
            Vista = vista;
            Ingredientes = new ObservableCollection<Ingrediente>();
            CargarIngredientes();
        }
        
        private void CargarIngredientes()
        {
            Ingredientes.Clear();
            List<Ingrediente> lista = App.DataBase.Ingrediente.ObtenerList();
            foreach (Ingrediente ing in lista)
            {
                Ingredientes.Add(ing);
            }
        }

        internal ObservableCollection<Ingrediente> DevolverListaFiltrada(string nom)
        {
            List<Ingrediente> filtrada = Ingredientes.Where(i => i._Nombre.Contains(nom)).ToList();
            ObservableCollection<Ingrediente> retorno = new ObservableCollection<Ingrediente>();
            foreach (Ingrediente ing in filtrada)
            {
                retorno.Add(ing);
            }
            return retorno;
        }

        //Se pone mensaje si el usuario ya tiene el ingrediente insertado para avisar que se actualizó la cantidad?
        public async void AgregarIngrediente(Ingrediente ing, int cantidad)
        {
            IngredienteUsuario ingUs = new IngredienteUsuario
            {
                _Ingrediente = ing,
                _Cantidad = cantidad,
                _Email = Usuario._Email,
                _Perfil = Usuario._Perfil,
                _IdIngrediente = ing._IdIngrediente,
                _CantidadMedida = cantidad + ing._Medida.ToString()
            };

            string aver = ingUs._CantidadMedida;
            IngredienteUsuario ingredienteUsuario = await App.IngredienteUsuarioService.Alta(ingUs);
            if (ingredienteUsuario != null)
            {
                App.DataBase.IngredienteUsuario.Guardar(ingredienteUsuario);
            }
            
           
            if (!Usuario._Perfil._ListaIngredientesUsuario.Contains(ingUs))
            {
                Usuario._Perfil._ListaIngredientesUsuario.Add(ingUs);
            }

            Vista.RefrescarListaIng();
        }

        public async void ActualizarIngrediente(IngredienteUsuario ing, int cantidad)
        {
            ing._Cantidad = cantidad;
            IngredienteUsuario ingUs = await App.IngredienteUsuarioService.Alta(ing);
            if (ingUs != null) App.DataBase.IngredienteUsuario.Guardar(ingUs);
            else Vista.MensajeError("Hubo un error al ingresar el ingrediente, por favor intentelo nuevamente.");
        }


    }
}
