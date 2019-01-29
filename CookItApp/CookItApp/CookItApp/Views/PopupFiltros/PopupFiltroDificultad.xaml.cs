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
	public partial class PopupFiltroDificultad : PopupPage
	{
        FiltrosVM ViewModel;
		public PopupFiltroDificultad (FiltrosVM viewmodel)
		{
            InitializeComponent();
            GenerarDatosPicker();
            ViewModel = viewmodel;
		}

        private void GenerarDatosPicker()
        {
            picMax.Items.Add("1");
            picMax.Items.Add("2");
            picMax.Items.Add("3");
            picMax.Items.Add("4");
            picMax.Items.Add("5");

            picMin.Items.Add("1");
            picMin.Items.Add("2");
            picMin.Items.Add("3");
            picMin.Items.Add("4");
            picMin.Items.Add("5");
        }

        private void Ok_Tapped(object sender, EventArgs e)
        {
            int min = -1;
            if (picMin.SelectedIndex != -1) min = Convert.ToInt32(picMin.SelectedItem);
            int max = -1;
            if (picMax.SelectedIndex != -1) max = Convert.ToInt32(picMax.SelectedItem);
            if(min != -1 && max != -1)
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
            ViewModel.IngresarFiltroDificultad(min, max);
            CerrarPopup();
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            CerrarPopup();
        }

        private void MensajeError(string msg)
        {
            DisplayAlert("Error", msg, "Cerrar");
        }

        private async void CerrarPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}