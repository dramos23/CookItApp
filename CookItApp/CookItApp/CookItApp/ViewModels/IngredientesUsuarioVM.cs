﻿using CookItApp.Data;
using CookItApp.Models;
using CookItWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class IngredientesUsuarioVM
    {
        public Usuario Usr;
        public IViewIngUsuario Vista;
        public ObservableCollection<IngredienteUsuario> IngredientesUsuario { get; set; }

        public IngredientesUsuarioVM(Usuario usr, IViewIngUsuario vista)
        {
            Usr = usr;
            Vista = vista;
            //    CargarDatos();
            GenerarDatosPrueba();
        }

        /*
        public void CargarDatos()
        {
            List<IngredienteUsuario> lista = Usr._Perfil._IngredientesUsuario;
            if (lista.Count != 0) return;
            foreach (IngredienteUsuario iu in lista)
            {
                iu._CantidadMedida = iu._Cantidad + iu._Ingrediente._Medida.ToString();
                this.IngredientesUsuario.Add(iu);
            }
        }
        */
        internal void BorrarIngrediente(IngredienteUsuario ing)
        {
            IngredientesUsuario.Remove(ing);
            //Falta metodo de controlador que borra ingrediente.
            Vista.RefrescarListaIng(IngredientesUsuario);
        }

        internal void AgregarIngrediente(IngredienteUsuario ing)
        {
            IngredientesUsuario.Add(ing);
            //Falta metodo de controlador que agrega ingrediente.
            Vista.RefrescarListaIng(IngredientesUsuario);
        }

        private void GenerarDatosPrueba()
        {
            IngredientesUsuario = new ObservableCollection<IngredienteUsuario>();
            IngredienteUsuario ing = new IngredienteUsuario()
            {
                _Cantidad = 500,
                _CantidadMedida = "500gr",
                _Ingrediente = new Ingrediente
                {
                    _Nombre = "Jamon",
                    _TipoIngrediente = new TipoIngrediente { _IdTipoIngrediente = 6 }
                }
            };

            IngredienteUsuario ing2 = new IngredienteUsuario()
            {
                _Cantidad = 600,
                _CantidadMedida = "600ml",
                _Ingrediente = new Ingrediente
                {
                    _Nombre = "Aceite de oliva",
                    _TipoIngrediente = new TipoIngrediente { _IdTipoIngrediente = 1 }
                }
            };

            IngredienteUsuario ing3 = new IngredienteUsuario()
            {
                _Cantidad = 200,
                _CantidadMedida = "200gr",
                _Ingrediente = new Ingrediente
                {
                    _Nombre = "Fideos prehechos",
                    _TipoIngrediente = new TipoIngrediente { _IdTipoIngrediente = 11 }
                }
            };

            IngredienteUsuario ing4 = new IngredienteUsuario()
            {
                _Cantidad = 1000,
                _CantidadMedida = "1000ml",
                _Ingrediente = new Ingrediente
                {
                    _Nombre = "Leche descremada",
                    _TipoIngrediente = new TipoIngrediente { _IdTipoIngrediente = 10 }
                }
            };
            IngredientesUsuario.Add(ing); IngredientesUsuario.Add(ing2); IngredientesUsuario.Add(ing3);
            IngredientesUsuario.Add(ing4);


        }

    }
}
