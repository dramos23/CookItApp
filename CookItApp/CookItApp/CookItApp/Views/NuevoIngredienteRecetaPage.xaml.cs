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
	public partial class NuevoIngredienteRecetaPage : ContentPage
	{
        private IViewListIngRec Vista { get; set; }
        private bool UPD { get; set; }
        private NuevoIngRecVM VMNuevoIngRec { get; set; } 
        private int Medida { get; set; }

		public NuevoIngredienteRecetaPage (IngredienteReceta ingredienteReceta, IViewListIngRec vista)
		{
			InitializeComponent ();
            Vista = vista;
            UPD = ingredienteReceta != null ? true : false;

            VMNuevoIngRec = new NuevoIngRecVM(ingredienteReceta);
            BindingContext = VMNuevoIngRec;

        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (await ValidarDatos())
            {
                Ingrediente ingrediente = picIngrediente.ItemsSource[picIngrediente.SelectedIndex] as Ingrediente;                
                int Cantidad = Convert.ToInt32(entryCantidad.Text);
                Ingrediente.TipoMedida Medida = ingrediente._Medida;

                VMNuevoIngRec.CargarIngRec(ingrediente, Cantidad, Medida);

                if (UPD)
                {
                    Vista.Actualizar(VMNuevoIngRec.IngredienteReceta);
                }
                else
                {
                    Vista.Insertar(VMNuevoIngRec.IngredienteReceta);
                }
                await Navigation.PopAsync();
            }
        }

        private async Task<bool> ValidarDatos()
        {

            if (!(picIngrediente.ItemsSource[picIngrediente.SelectedIndex] is Ingrediente ingrediente))
            {
                await UserDialogs.Instance.AlertAsync("Debe seleccionar un ingrediente!.", "Atención!", "Continuar");
                return false;
            }
            if (Vista.Existe(ingrediente._IdIngrediente) && UPD == false)
            {
                await UserDialogs.Instance.AlertAsync("Este ingrediente ya existe en tu receta, modificalo o eliminalo!.", "Atención!", "Continuar");
                return false;
            }
            if (entryCantidad.Text == null || Convert.ToInt32(entryCantidad.Text) <= 0)
            {
                await UserDialogs.Instance.AlertAsync("Es necesario ingresar una cantidad!.", "Atención!", "Continuar");
                return false;
            }

            

                return true;
            
        }

        private async void EliminarIngrediente_Clicked(object sender, EventArgs e)
        {
            if (UPD)
            {
                Vista.Eliminar(VMNuevoIngRec.IngredienteReceta);
                await Navigation.PopAsync();
            }
            else {
                await UserDialogs.Instance.AlertAsync("Aún no has guardado esté ingrediente!.", "Atención!", "Continuar");
            }
        }

        private void PicIngrediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ingrediente ingrediente = picIngrediente.ItemsSource[picIngrediente.SelectedIndex] as Ingrediente;
            entryMedida.Text = ingrediente._Medida.ToString();
        }
    }
}