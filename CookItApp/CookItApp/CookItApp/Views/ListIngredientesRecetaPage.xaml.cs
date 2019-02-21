using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
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
    public partial class ListIngredientesRecetaPage : ContentPage, IViewListIngRec
    {
        private IViewNuevaReceta Vista { get; set; }
        
        private ListIngredientesRecetaVM VMListIngRec { get; set; }

        public ListIngredientesRecetaPage (List<IngredienteReceta> ingredienteRecetas, IViewNuevaReceta vista)
		{
			InitializeComponent ();
            Vista = vista;
            
            VMListIngRec = new ListIngredientesRecetaVM(ingredienteRecetas);
            BindingContext = VMListIngRec;
        }

        private async void BtnGuardarIngRec_Clicked(object sender, EventArgs e)
        {
            if (await ValidarDatos())
            {
                Vista.CargarIngredientes(VMListIngRec.IngredientesReceta);
                await Navigation.PopAsync();
            }
           
        }

        private async Task<bool> ValidarDatos()
        {
            if(VMListIngRec.IngredientesReceta == null || VMListIngRec.IngredientesReceta.Count < 1)
            {
                await UserDialogs.Instance.AlertAsync("Antes de guardar debe ingresar ingredientes a la receta!.", "Atención!", "Continuar");
                return false;
            }

            return true;
        }

        private async void NuevoIngrediente_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new NuevoIngredienteRecetaPage(null, this));
        }

        private async void IngredientesReceta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando ingrediente receta..");

            
            if (!(e.SelectedItem is IngredienteReceta ingredienteReceta))
            {
                return;
            }

                await Navigation.PushAsync(new NuevoIngredienteRecetaPage(ingredienteReceta, this));

                IngredientesReceta.SelectedItem = null;
                UserDialogs.Instance.HideLoading();
            }


        public void Insertar(IngredienteReceta ingredienteReceta)
        {
            VMListIngRec.AgregarIngRec(ingredienteReceta);
            ActualizarVista();
        }

        public void Eliminar(IngredienteReceta ingredienteReceta)
        {
            VMListIngRec.EliminarIngRec(ingredienteReceta);
            ActualizarVista();
        }

        public void Actualizar(IngredienteReceta ingredienteReceta)
        {
            VMListIngRec.ActualizarIngRec(ingredienteReceta);
            ActualizarVista();
        }

        private void ActualizarVista()
        {
            List<IngredienteReceta> ingredienteRecetas = new List<IngredienteReceta>(VMListIngRec.IngredientesReceta);
            VMListIngRec = new ListIngredientesRecetaVM(ingredienteRecetas);
            BindingContext = VMListIngRec;
        }

        public bool Existe(int idIngrediente)
        {
            if (VMListIngRec.IngredientesReceta.Count > 0)
            {
                IngredienteReceta x = null;

                try
                {
                    x = VMListIngRec.IngredientesReceta.FirstOrDefault(i => i._IdIngrediente == idIngrediente);
                }
                catch
                {
                    x = null;
                }


                if (x != null)
                {
                    return true;
                }

                return false;
            }
            return false;
        }
    }
    
}