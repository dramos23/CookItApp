using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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

        
        private void TxtIngrediente_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ListaIngredientes.ItemsSource = ViewModel.DevolverListaFiltrada(txtIngrediente.Text);
            
            string keyword = txtIngrediente.Text;
            if (keyword != "")
            {
                List<Ingrediente> resultado = ViewModel.Ingredientes.Where(c => c._Nombre.ToLower().Contains(keyword.ToLower())).ToList();
                ListaIngredientes.ItemsSource = resultado;

            }
            
        }

        private async void IngresarIngrediente_Tapped(object sender, EventArgs e)
        {
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            if (cantidad > 0)
            {
                Ingrediente ing = (Ingrediente)ListaIngredientes.SelectedItem;
                try
                {
                    await ViewModel.AgregarIngrediente(ing, cantidad);
                    await PopupNavigation.Instance.PopAsync();
                }
                catch
                {
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            else {
                await DisplayAlert("Error: Cantidad", "Tiene que ingresar una cantidad mayor a cero", "OK");
            }
        }

        private void ListaIngredientes_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
        }

        private void ListaIngredientes_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            Ingrediente ing = (Ingrediente)ListaIngredientes.SelectedItem;
            txtMedida.Text = ing._Medida.ToString();
            txtIngSeleccionado.Text = ing._Nombre;
        }
    }
}