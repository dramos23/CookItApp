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
	public partial class RetoPage : ContentPage
	{
        public RetoVM _ViewModelReto { get; set; }

        private Usuario _Usuario { get; set; }

        public RetoPage (Reto reto, Usuario usuario)
		{
			InitializeComponent ();
            _Usuario = usuario;
            Receta_Clicked();
            _ViewModelReto = new RetoVM(usuario, reto);
            BindingContext = _ViewModelReto;
        }

        private void BtnAceptar_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnAceptar_Clicked_1(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {

        }

        private void Receta_Clicked()
        {
            frameReceta.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => {

                    frameReceta.BackgroundColor = Color.Silver;
                    await Task.Delay(10);
                    frameReceta.BackgroundColor = Color.White;

                    Receta receta = new Receta()
                    {
                        _IdReceta = Convert.ToInt32(_ViewModelReto.Reto._RecetaId)
                    };
                    receta = await App.RecetaService.Obtener(receta);
                    if (receta != null)
                    {

                        await Navigation.PushAsync(new RecetaPage(receta, _Usuario));

                    }

                })
            });
        }
    }
}