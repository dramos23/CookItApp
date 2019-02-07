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
    public partial class PopupIngredientesReceta : PopupPage
    {
        private Usuario _Usuario;
        private Receta _Receta;

        public PopupIngredientesReceta(Usuario usr, Receta rec)
        {
            InitializeComponent();
            _Usuario = usr;
            _Receta = rec;
            BindingContext = _Receta;
        }

        private async void btnCompararIngredientes_Clicked(object sender, EventArgs e)
        {
            if(_Usuario._Perfil._ListaIngredientesUsuario.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(_Usuario, "Ingredientes de receta", "¡No tienes ningun ingrediente! Prueba" +
                    " ingresar algunos así podemos ayudarte a elegir mejores recetas."));
                return;
            }

            await PopupNavigation.Instance.PushAsync(new PopupComparacionIngredientes(_Usuario, _Receta));
        }

        private async void btnVolverReceta_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            return true;
        }

    }
}