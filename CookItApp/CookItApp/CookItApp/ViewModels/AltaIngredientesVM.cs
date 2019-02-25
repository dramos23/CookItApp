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
    public class AltaIngredientesVM
    {
        public Usuario Usuario;
        public IViewIngUsuario Vista;
        public ObservableCollection<Ingrediente> Ingredientes { get; set; }

        public AltaIngredientesVM(Usuario usr, IViewIngUsuario vista)
        {
            Usuario = usr;
            Vista = vista;
            ////Ingredientes = new ObservableCollection<Ingrediente>();
            CargarIngredientes();
        }
        
        private void CargarIngredientes()
        {
            //Ingredientes.Clear();
            List<Ingrediente> lista = App.DataBase.Ingrediente.ObtenerList();
            Ingredientes = new ObservableCollection<Ingrediente>(lista);
            //foreach (Ingrediente ing in lista)
            //{
            //    Ingredientes.Add(ing);
            //}
        }

        internal ObservableCollection<Ingrediente> DevolverListaFiltrada(string nom)
        {
            List<Ingrediente> filtrada = Ingredientes.Where(i => i._Nombre.Contains(nom)).ToList();
            ObservableCollection<Ingrediente> retorno = new ObservableCollection<Ingrediente>(filtrada);
            //foreach (Ingrediente ing in filtrada)
            //{
            //    retorno.Add(ing);
            //}
            return retorno;
        }

        //Se pone mensaje si el usuario ya tiene el ingrediente insertado para avisar que se actualizó la cantidad?
        public async Task AgregarIngrediente(Ingrediente ing, int cantidad)
        {
            IngredienteUsuario ingUs = new IngredienteUsuario
            {
                _Cantidad = cantidad,
                _Email = Usuario._Email,
                //_Perfil = Usuario._Perfil,
                _IdIngrediente = ing._IdIngrediente,
                _Medida = ing._Medida                       
            };

            try
            {
                bool ingresado = await App.IngredienteUsuarioService.Alta(ingUs);
                if (ingresado)
                {
                    //ingUs._Ingrediente = ing;
                    App.DataBase.IngredienteUsuario.Guardar(ingUs);
                    //Usuario._Perfil.AgregarIngredienteUsuario(ingUs);
                    Vista.Mensaje("Ingrediente ingresado!");
                    Vista.RefrescarListaIng();
                }
                else {
                    Vista.Mensaje("Error al ingresar ingrediente, por favor intentelo nuevamente.");
                }

            }
            catch
            {
                Vista.Mensaje("Error al ingresar ingrediente, por favor intentelo más tarde.");
            }
                      
        }       

        public async void ActualizarIngrediente(IngredienteUsuario ing, int cantidad)
        {
            ing._Cantidad = cantidad;
            if (cantidad > 0)
            {
                bool actualizado = await App.IngredienteUsuarioService.Modificar(ing);
                if (actualizado)
                {
                    App.DataBase.IngredienteUsuario.Modificar(ing);
                    Vista.RefrescarListaIng();
                }
                else
                {
                    Vista.Mensaje("Hubo un error al ingresar el ingrediente, por favor intentelo nuevamente.");
                }
            }
            else {
                Vista.Mensaje("No se puede actualizar un ingrdiente a cero, en este tipo de caso borrar el ingrediente!.");
            }
        }


    }
}
