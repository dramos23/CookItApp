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
    public partial class PopupIngresarIngrediente : PopupPage
    {
        private Usuario Usuario;
        private AltaIngredientesVM ViewModel;

        public PopupIngresarIngrediente(Usuario usr, IViewIngUsuario view)
        {
            InitializeComponent();
            ViewModel = new AltaIngredientesVM(usr, view);
            Usuario = usr;
            BindingContext = ViewModel;
        }

        
        private void txtIngrediente_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ListaIngredientes.ItemsSource = ViewModel.DevolverListaFiltrada(txtIngrediente.Text);
            
            string keyword = txtIngrediente.Text;
            if (keyword != "")
            {
                List<Ingrediente> resultado = ViewModel.Ingredientes.Where(c => c._Nombre.ToLower().Contains(keyword.ToLower())).ToList();

                
                ListaIngredientes.ItemsSource = resultado;

            }
            
        }

        private void IngresarIngrediente_Tapped(object sender, EventArgs e)
        {            
            Ingrediente ing = (Ingrediente)ListaIngredientes.SelectedItem;
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            ViewModel.AgregarIngrediente(ing, cantidad);
        }
    }
}