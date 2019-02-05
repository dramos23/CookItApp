using CookItApp.Data;
using CookItApp.Models;
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

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupMensaje : PopupPage
    {
        private Usuario Usuario;

        public PopupMensaje(Usuario usr, string titulo, string msj)
        {
            InitializeComponent();
            Usuario = usr;
            txtTitulo.Text = titulo;
            txtMensaje.Text = msj;
        }

        private async void Ok_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}