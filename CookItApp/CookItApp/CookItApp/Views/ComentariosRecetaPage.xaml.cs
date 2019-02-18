using CookItApp.Data;
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
	public partial class ComentariosRecetaPage : ContentPage, IViewComentario
	{
        private ComentariosRecetaVM _ViewModel;
        private Usuario Usuario { get; set; }
		public ComentariosRecetaPage (Receta rec, Usuario user)
		{
            _ViewModel = new ComentariosRecetaVM(rec);
			InitializeComponent ();
            Usuario = user;
            BindingContext = _ViewModel;
		}


        private void BtnCrearComentario_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupIngresarComentario(Usuario, _ViewModel._Receta, this));
        }

        private async void BtnVolverReceta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetaPage(_ViewModel._Receta, Usuario));
        }

        private void RefrescarComentarios()
        {
            lstComentarios.ItemsSource = _ViewModel._ComentariosReceta;
        }

        public void ActualizarLista(ComentarioReceta nuevo)
        {
            _ViewModel.AgregarComentario(nuevo);
            RefrescarComentarios();
        }
    }
}