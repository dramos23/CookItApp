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

namespace CookItApp.Views.PopupFiltros
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupFiltroPuntaje : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroPuntaje (FiltrosVM viewmodel)
		{
            InitializeComponent();
            GenerarDatosPicker();
            ViewModel = viewmodel;
		}

        private void GenerarDatosPicker()
        {
            List<int> ListaPicker = new List<int>();
            ListaPicker.Add(1);
            ListaPicker.Add(2);
            ListaPicker.Add(3);
            ListaPicker.Add(4);
            ListaPicker.Add(5);

            picMax.ItemsSource = ListaPicker;
            picMin.ItemsSource = ListaPicker;
        }

        private void Ok_Tapped(object sender, EventArgs e)
        {
            int min = -1;
            if (picMin.SelectedIndex != -1) min = Convert.ToInt32(picMin.SelectedItem);
            int max = -1;
            if (picMax.SelectedIndex != -1) max = Convert.ToInt32(picMax.SelectedItem);
            if (min != -1 && max != -1)
            {
                if (min > max)
                {
                    MensajeError("El valor minimo no puede ser mayor al maximo.");
                    return;
                }
                if (max < min)
                {
                    MensajeError("El valor maximo no puede ser menor al minimo.");
                    return;
                }

            }
            ViewModel.IngresarFiltroPuntuacion(min, max);
            CerrarPopup();
        }

        private async void MensajeError(string msg)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMensaje(ViewModel.Usuario, "Error en tu filtro", msg));
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            CerrarPopup();
        }

        private async void CerrarPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}