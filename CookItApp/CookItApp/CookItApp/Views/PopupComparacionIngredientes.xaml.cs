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
    public partial class PopupComparacionIngredientes : PopupPage
    {
        private Usuario _Usuario;
        private Receta _Receta;
        private ComparacionIngredientesVM _ViewModel;

        public PopupComparacionIngredientes(Usuario usr, Receta rec)
        {
            InitializeComponent();
            _Usuario = usr;
            _Receta = rec;
            _ViewModel = new ComparacionIngredientesVM(_Usuario, _Receta);
            BindingContext = _ViewModel;
            ListaIngredientesFaltantes.ItemsSource = _ViewModel.IngredientesFaltantes;
            ListaIngredientesHeladera.ItemsSource = _ViewModel.IngredientesEnHeladera;
            ListaIngredientesInsuficiente.ItemsSource = _ViewModel.IngredientesPocaCantidad;

        }

        private async void imgCerrar_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}