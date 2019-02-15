using CookItApp.Models;
using CookItApp.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarMaps : ContentPage
	{
        
        private MapaVM MapaVM { get; set; }

        public MostrarMaps (Position x, Supermercado supermercado)
		{
			InitializeComponent ();

            MapaVM = new MapaVM(this, x, supermercado);
            BindingContext = MapaVM;
        }

        public MostrarMaps(Supermercado supermercado)
        {
            InitializeComponent();

            MapaVM = new MapaVM(this, supermercado);
            BindingContext = MapaVM;
        }


    }
}