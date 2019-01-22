﻿using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientesUsuarioView : ContentPage, IViewIngUsuario
    {
        Usuario usuario;
        private IngredientesUsuarioVM viewModel = null;

        public IngredientesUsuarioView(Usuario usr)
        {
            InitializeComponent();
            usuario = usr;
            viewModel = new IngredientesUsuarioVM(usr, this);
            ListaIngredientes.ItemDragging += ListaIngredientes_ItemDragging;
            BindingContext = viewModel;
        }

        //Evento que se activa cuando se arrastra un elemento
        private async void ListaIngredientes_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            var viewModel = ListaIngredientes.BindingContext as IngredientesUsuarioVM;

            if (e.Action == DragAction.Start)
            {
            }

            if (e.Action == DragAction.Dragging)
            {
                var position = new Point(e.Position.X - this.ListaIngredientes.Bounds.X, e.Position.Y - this.ListaIngredientes.Bounds.Y);
                if (this.frmBorrar.Bounds.Contains(position))
                    imgTrash.Source = "iconTrashOn.png";
                else
                    imgTrash.Source = "iconTrashOff.png";
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - this.ListaIngredientes.Bounds.X, e.Position.Y - this.ListaIngredientes.Bounds.Y);
                if (this.frmBorrar.Bounds.Contains(position))
                {
                    await Task.Delay(100);
                    BorrarIngrediente(e.ItemData as IngredienteUsuario);
                }
            }
        }

        public void ActualizarIngrediente(IngredienteUsuario ing)
        {
            throw new NotImplementedException();
        }

        public void AgregarIngrediente(IngredienteUsuario ing)
        {
            throw new NotImplementedException();
        }

        public void BorrarIngrediente(IngredienteUsuario ing)
        {
            viewModel.BorrarIngrediente(ing);
        }

        public void RefrescarListaIng(ObservableCollection<IngredienteUsuario> ings)
        {
            ListaIngredientes.ItemsSource = ings;
        }
    }
}