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
    public partial class PopupInfoReceta : PopupPage
    {
        Usuario Usuario;
        Receta Receta;

        public PopupInfoReceta(Usuario usr, Receta rec)
        {
            InitializeComponent();
            Usuario = usr;
            Receta = rec;
            CargarAtributosFaltantesReceta();
            CargarLabels();
        }

        private void CargarAtributosFaltantesReceta()
        {
            if(Receta._Estacion == null)
            {
                Receta._Estacion = App.DataBase.Estacion.Obtener(Receta._IdEstacion);
            }
            if(Receta._MomentoDia == null)
            {
                Receta._MomentoDia = App.DataBase.MomentoDia.Obtener(Receta._IdMomentoDia);
            }
        }

        private void CargarLabels()
        {

            lblCeliacos.Text = "Apto para celiacos? " + DevolverBoolAString(Receta._AptoCeliacos);
            lblVegetarianos.Text = "Apto para vegetarianos?" + DevolverBoolAString(Receta._AptoVegetarianos);
            lblVeganos.Text = "Apto para veganos?" + DevolverBoolAString(Receta._AptoVeganos);
            lblDiabeticos.Text = "Apto para diabeticos?" + DevolverBoolAString(Receta._AptoDiabeticos);

            lblCalorias.Text = "Calorias: " + Receta._CantCalorias;
            lblTiempo.Text = "Tiempo estimado de preparacion: " + (Receta._TiempoPreparacion) + " minutos";
            lblPlatos.Text = "Cantidad estimada de platos: " + Receta._CantPlatos;
            lblCosto.Text = "Costo estimado (pesos UY): " + Receta._Costo;
            lblMomentoDia.Text = "Momento del día: " + Receta._MomentoDia._Nombre;
            lblEstacion.Text = "Estación: " + Receta._Estacion._Nombre;
        }

        private string DevolverBoolAString(bool b)
        {
            if (b) return "Si.";
            return "No.";
        }

        private async void VolverReceta_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

    }
}