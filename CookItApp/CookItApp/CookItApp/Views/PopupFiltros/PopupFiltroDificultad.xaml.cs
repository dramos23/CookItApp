﻿using CookItApp.ViewModels;
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
            List<string> ListaPicker = new List<string>();
            ListaPicker.Add("Ignorar");
            ListaPicker.Add("1");
            ListaPicker.Add("2");
            ListaPicker.Add("3");
            ListaPicker.Add("4");
            ListaPicker.Add("5");

            picMax.ItemsSource = ListaPicker;
            picMin.ItemsSource = ListaPicker;
        }

        private void Ok_Tapped(object sender, EventArgs e)
        {
            string picMinStr = picMin.SelectedItem.ToString();
            int min = -1;
            string picMaxStr = picMax.SelectedItem.ToString();
            int max = -1;

            if (picMin.SelectedIndex != -1 && picMin.SelectedItem.ToString() != "Ignorar")
                min = Convert.ToInt32(picMinStr);
            if (picMax.SelectedIndex != -1 && picMax.SelectedItem.ToString() != "Ignorar")
                max = Convert.ToInt32(picMaxStr);

            if (picMaxStr.Equals("Ignorar") && picMinStr.Equals("Ignorar"))
            {
                MensajeError("Por favor, seleccione al menos uno de los filtros.");
                return;
            }

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
            ViewModel.IngresarFiltroDificultad(min, max);
            CerrarPopup();
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            CerrarPopup();
        }

        private async void MensajeError(string msg)
        {
            await PopupNavigation.Instance.PushAsync(new PopupMensaje(ViewModel.Usuario, "Error en tu filtro", msg));
        }

        private async void CerrarPopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}