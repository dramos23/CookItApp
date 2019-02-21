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
	public partial class ListPasosRecetaPage : ContentPage, IViewListPasosRec
	{
        private IViewNuevaReceta Vista { get; set; }
        private NuevoPasosRecetaVM VMNuevoPasosReceta { get; set; }

        public ListPasosRecetaPage (List<PasoReceta> pasoRecetas, IViewNuevaReceta vista)
		{
			InitializeComponent ();
            Vista = vista;

            VMNuevoPasosReceta = new NuevoPasosRecetaVM(pasoRecetas);
            BindingContext = VMNuevoPasosReceta;
        }

        private async void NuevoPaso_Clicked(object sender, EventArgs e)
        {
            int Id = VMNuevoPasosReceta.Proximo_PasoRecetaID();
            await Navigation.PushAsync(new NuevoPasoRecetaPage(null, Id, this));
        }

        private async void BtnGuardarPasosRec_Clicked(object sender, EventArgs e)
        {
            if (await ValidarDatos())
            {
                Vista.CargarPasos(VMNuevoPasosReceta.PasosReceta);
                await Navigation.PopAsync();
            }
        }

        private async Task<bool> ValidarDatos()
        {
            if (VMNuevoPasosReceta.PasosReceta == null || VMNuevoPasosReceta.PasosReceta.Count < 1)
            {
                await UserDialogs.Instance.AlertAsync("Antes de guardar debe de ingresarle pasos a la receta!.", "Atención!", "Continuar");
                return false;
            }

            return true;
        }

        private async void PasosReceta_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Cargando paso receta..");


            if (!(e.SelectedItem is PasoReceta pasoReceta))
            {
                return;
            }

            //int Id = VMNuevoPasosReceta.Proximo_PasoRecetaID();
            await Navigation.PushAsync(new NuevoPasoRecetaPage(pasoReceta, pasoReceta._IdPasoReceta, this));

            PasosReceta.SelectedItem = null;
            UserDialogs.Instance.HideLoading();
            
        }

        public void Insertar(PasoReceta pasoReceta)
        {
            VMNuevoPasosReceta.AgregarPasoRec(pasoReceta);
            ActualizarVista();
        }

        public void Eliminar(PasoReceta pasoReceta)
        {
            VMNuevoPasosReceta.EliminarPasoRec(pasoReceta);
            ActualizarVista();
        }

        public void Actualizar(PasoReceta pasoReceta)
        {
            VMNuevoPasosReceta.ActualizarPasoRec(pasoReceta);
            ActualizarVista();
        }

        private void ActualizarVista()
        {
            List<PasoReceta> pasoRecetas = new List<PasoReceta>(VMNuevoPasosReceta.PasosReceta);
            VMNuevoPasosReceta = new NuevoPasosRecetaVM(pasoRecetas);
            BindingContext = VMNuevoPasosReceta;
        }

        
    }
}