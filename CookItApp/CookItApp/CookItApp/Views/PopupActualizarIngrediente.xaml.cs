using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupActualizarIngrediente : PopupPage
    {
        private Usuario Usuario;
        private IngredienteUsuario Ing;
        private AltaIngredientesVM ViewModel;

        public PopupActualizarIngrediente(Usuario usr, IViewIngUsuario view, IngredienteUsuario ing)
        {
            InitializeComponent();
            ViewModel = new AltaIngredientesVM(usr, view);
            Usuario = usr;
            Ing = ing;
            txtCantidad.Text = Ing._Cantidad + "";
            txtNombreIng.Text = Ing._Ingrediente._Nombre;
        }

        private void ActualizarIngrediente_Tapped(object sender, EventArgs e)
        {           
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            ViewModel.ActualizarIngrediente(Ing, cantidad);
        }
    }
}