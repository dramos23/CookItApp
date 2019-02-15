using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientesUsuarioView : ContentPage, IViewIngUsuario
    {
        Usuario usuario;
        private IngredientesUsuarioVM viewModel;

        public IngredientesUsuarioView(Usuario usr)
        {
            InitializeComponent();
            usuario = usr;
            viewModel = new IngredientesUsuarioVM(usr, this);
            ListaIngredientes.ItemDragging += ListaIngredientes_ItemDragging;
            BindingContext = viewModel;
            MensajeFaltaIngredientes();
        }

        private void MensajeFaltaIngredientes()
        {
            if (viewModel.IngredientesUsuario.Count == 0)
            {
                ListaIngredientes.IsVisible = false;
                layoutMensaje.IsVisible = true;
            }else
            {
                ListaIngredientes.IsVisible = true;
                layoutMensaje.IsVisible = false;
            }
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
                var position = new Point(e.Position.X - this.ListaIngredientes.Bounds.X, e.Position.Y - this.ListaIngredientes.Bounds.Y - 100);
                if (this.frmBorrar.Bounds.Contains(position))
                    imgTrash.Source = "iconTrashOn.png";
                else
                    imgTrash.Source = "iconTrashOff.png";
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - this.ListaIngredientes.Bounds.X, e.Position.Y - this.ListaIngredientes.Bounds.Y - 100);
                if (this.frmBorrar.Bounds.Contains(position))
                {
                    await Task.Delay(100);
                    BorrarIngrediente(e.ItemData as IngredienteUsuario);
                }
            }
        }

        public void ActualizarIngrediente(IngredienteUsuario ing)
        {
            RefrescarListaIng();
        }

        public void AgregarIngrediente(IngredienteUsuario ing)
        {
            RefrescarListaIng();
        }

        public void BorrarIngrediente(IngredienteUsuario ing)
        {
            viewModel.BorrarIngrediente(ing);
        }

        private async void BtnAgregarIng_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupIngresarIngrediente(usuario, this));
        }

        private async void BtnActualizarIng_Tapped(object sender, EventArgs e)
        {
            if (ListaIngredientes.SelectedItem == null)
            {
                Mensaje("Tienes que seleccionar un ingrediente de la lista para poder cambiar su cantidad.");
                return;
            }
            IngredienteUsuario ing = ListaIngredientes.SelectedItem as IngredienteUsuario;
            await PopupNavigation.Instance.PushAsync(new PopupActualizarIngrediente(usuario, this, ing));
        }

        public void RefrescarListaIng()
        {
            ListaIngredientes.ItemsSource = viewModel.DevolverListaIngUsuario();
            if (ListaIngredientes.IsVisible == false)
            {
                ListaIngredientes.IsVisible = true;
                layoutMensaje.IsVisible = false;
            }
        }

        public async void Mensaje(string v)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMensaje(usuario, "Tus ingredientes", v));
        }
    }
}