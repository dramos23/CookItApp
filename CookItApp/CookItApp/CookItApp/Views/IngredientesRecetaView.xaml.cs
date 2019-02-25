using CookItApp.Models;
using CookItApp.ViewModels;
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
	public partial class IngredientesRecetaView : ContentPage
	{
        private Usuario _Usuario;
        private Receta _Receta;

        private IngredienteRecetaVM IngredienteRecetaVM { get; set; }

        public IngredientesRecetaView (Usuario usr, Receta rec)
		{
            InitializeComponent();
            _Usuario = usr;
            _Receta = rec;
            IngredienteRecetaVM = new IngredienteRecetaVM(_Receta._IdReceta);
            BindingContext = IngredienteRecetaVM;
        }

        private async void BtnCompararIngredientes_Clicked(object sender, EventArgs e)
        {
            if (_Usuario._Perfil._ListaIngredientesUsuario.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(new PopupMensaje(_Usuario, "Ingredientes de receta", "¡No tienes ningun ingrediente! Prueba" +
                    " ingresar algunos así podemos ayudarte a elegir mejores recetas."));
                return;
            }

            await PopupNavigation.Instance.PushAsync(new PopupComparacionIngredientes(_Usuario, _Receta));
        }

        //private async void BtnVolverReceta_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return true;
        }
    }
}