using Acr.UserDialogs;
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
	public partial class ListaRecetasFavoritasPage : ContentPage
	{
        public RecetasFavoritasVM VMRecetasFavoritas { get; set; }
        public Usuario Usuario { get; set; }

        public ListaRecetasFavoritasPage (Usuario usuario)
		{
			InitializeComponent ();
            Usuario = usuario;
            VMRecetasFavoritas = new RecetasFavoritasVM(Usuario);
            BindingContext = VMRecetasFavoritas;
        }

        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {
            UserDialogs.Instance.ShowLoading("Cargando receta..");
            if (!(args.SelectedItem is RecetaFavorita recetaFavorita))
            {
                return;
            }

            MisFavoritos.SelectedItem = null;
            Receta rec = await App.RecetaService.Obtener(recetaFavorita._Receta);

            if (rec != null)
            {
                
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(rec, Usuario));
                UserDialogs.Instance.HideLoading();

            }
            else
            {
                await DisplayAlert("Error", "Ha ocurrido un error, vuelva a intentarlo.", "Continuar");
            }
        }

        private async void BtnQuitarFavoritos_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Quitando de favoritos..");

            Image image = (Image)sender;

            RecetaFavorita recetaFavorita = image.BindingContext as RecetaFavorita;


            bool result = await App.RecetaFavoritaService.Eliminar(recetaFavorita);
            if (result)
            {

                App.DataBase.RecetaFavorita.Borrar(recetaFavorita);
                VMRecetasFavoritas = new RecetasFavoritasVM(Usuario);
                BindingContext = VMRecetasFavoritas;
            }

            UserDialogs.Instance.HideLoading();

        }
    }
}