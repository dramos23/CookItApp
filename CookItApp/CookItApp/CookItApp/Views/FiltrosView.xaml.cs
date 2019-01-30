using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using CookItApp.Views.PopupFiltros;
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
    public partial class FiltrosView : PopupPage, IPopupFiltros
    {

        FiltrosVM ViewModel;
        Usuario Usuario; 
        public FiltrosView(Usuario usr)
        {
            InitializeComponent();
            InicializarControlador();
            Usuario = usr;
            VisibilidadFiltrosAutomaticos();
        }

        private void VisibilidadFiltrosAutomaticos()
        {
            if (Usuario._Perfil == null) return;
            layoutFiltAuto.IsVisible = true;
            layoutFiltAuto.IsEnabled = true;
            if (Usuario._Perfil._FiltroAutomatico)
            {
                swtFiltrosUsuario.IsToggled = true;
                ViewModel.CargarFiltrosUsuario();
            }
            else
            {
                swtFiltrosUsuario.IsToggled = false;
            }
        }

        private void TogglearImagenesPorFiltrosUsuario()
        {
            if (Usuario._Perfil == null || !Usuario._Perfil._FiltroAutomatico) return;
            swtFiltrosUsuario.IsToggled = true;
            ViewModel.CargarFiltrosUsuario();
        }

        private void InicializarControlador()
        {
            if (!Application.Current.Properties.ContainsKey("ViewModelFiltro"))
            {
                FiltrosVM vm = new FiltrosVM(Usuario);
                Application.Current.Properties.Add("ViewModelFiltro", vm);
                ViewModel = vm;
            }
            else
            {
                var vm = new object();
                Application.Current.Properties.TryGetValue("ViewModelFiltro", out vm);
                ViewModel = vm as FiltrosVM;
                ViewModel.SetVistaFiltros(this);
            }
        }


        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

      
        #region Toggles

        private void EventoToggle(object sender, EventArgs e)
        {
            Image img = sender as Image;
            string source = img.Source.ToString().Substring(6);
            switch (source)
            {
                case "aptoCeliacosOn.png":
                case "aptoCeliacosOff.png":
                    ToggleCeliacos();
                    break;
                case "aptoDiabeticosOn.png":
                case "aptoDiabeticosOff.png":
                    ToggleDiabeticos();
                    break;
                case "aptoVegetarianosOn.png":
                case "aptoVegetarianosOff.png":
                    ToggleVegetarianos();
                    break;
                case "aptoVeganosOn.png":
                case "aptoVeganosOff.png":
                    ToggleVeganos();
                    break;
                case "porCaloriasOn.png":
                case "porCaloriasOff.png":
                    ToggleCalorias();
                    break;
                case "porPrecioOn.png":
                case "porPrecioOff.png":
                    TogglePrecio();
                    break;
                case "porMomentoDiaOn.png":
                case "porMomentoDiaOff.png":
                    ToggleMomentoDia();
                    break;
                case "porEstacionOn.png":
                case "porEstacionOff.png":
                    ToggleEstacion();
                    break;
                case "porPuntajeOn.png":
                case "porPuntajeOff.png":
                    TogglePuntaje();
                    break;
                case "porTiempoOn.png":
                case "porTiempoOff.png":
                    ToggleTiempo();
                    break;
                case "porDificultadOn.png":
                case "porDificultadOff.png":
                    ToggleDificultad();
                    break;
            }

        }
        private void ToggleCeliacos()
        {
            if (ViewModel.FiltroCeliaco)
            {
                btnAptoCeliacos.Source = "aptoCeliacosOff.png";
                ViewModel.FiltroCeliaco = false;
            }
            else
            {
                btnAptoCeliacos.Source = "aptoCeliacosOn.png";
                ViewModel.FiltroCeliaco = true;
            }
        }

        private void ToggleDiabeticos()
        {
            if (ViewModel.FiltroDiabetico)
            {
                btnAptoDiabeticos.Source = "aptoDiabeticosOff.png";
                ViewModel.FiltroDiabetico = false;
            }
            else
            {
                btnAptoDiabeticos.Source = "aptoDiabeticosOn.png";
                ViewModel.FiltroDiabetico = true;
            }
        }

        private void ToggleVegetarianos()
        {
            if (ViewModel.FiltroVegetariano)
            {
                btnAptoVegetarianos.Source = "aptoVegetarianosOff.png";
                ViewModel.FiltroVegetariano = false;
            }
            else
            {
                btnAptoVegetarianos.Source = "aptoVegetarianosOn.png";
                ViewModel.FiltroVegetariano = true;
            }
        }


        private void ToggleVeganos()
        {
            if (ViewModel.FiltroVegano)
            {
                btnAptoVeganos.Source = "aptoVeganosOff.png";
                ViewModel.FiltroVegano = false;            }
            else
            {
                btnAptoVeganos.Source = "aptoVeganosOn.png";
                ViewModel.FiltroVegano = true;            }
        }

        private async void TogglePrecio()
        {
            if (ViewModel.FiltroPrecio)
            {
                btnPorPrecio.Source = "porPrecioOff.png";
                ViewModel.ResetPrecio();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroPrecio(ViewModel));
            }
        }

        private async void ToggleDificultad()
        {
            if (ViewModel.FiltroDificultad)
            {
                btnPorDificultad.Source = "porDificultadOff.png";
                ViewModel.ResetDificultad();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroDificultad(ViewModel));
            }
        }

        private async void ToggleEstacion()
        {
            if (ViewModel.FiltroEstacion)
            {
                btnPorEstacion.Source = "porEstacionOff.png";
                ViewModel.ResetEstacion();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroEstacion(ViewModel));
            }
        }

        private async void ToggleTiempo()
        {
            if (ViewModel.FiltroTiempoPreparacion)
            {
                btnPorTiempo.Source = "porTiempoOff.png";
                ViewModel.ResetTiempoPreparacion();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroTiempo(ViewModel));
            }
        }

        private async void TogglePuntaje()
        {
            if (ViewModel.FiltroPuntuacion)
            {
                btnPorPuntaje.Source = "porPuntajeOff.png";
                ViewModel.ResetPuntuacion();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroPuntaje(ViewModel));
            }
        }

        private async void ToggleMomentoDia()
        {
            if (ViewModel.FiltroMomentoDia)
            {
                btnPorMomentoDia.Source = "porMomentoDiaOff.png";
                ViewModel.ResetMomentoDia();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroMomentoDia(ViewModel));
            }
        }

        private async void ToggleCalorias()
        {
            if (ViewModel.FiltroCalorias)
            {
                btnPorCalorias.Source = "porCaloriasOff.png";
                ViewModel.ResetCalorias();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new PopupFiltroCalorias(ViewModel));
            }
        }
        #endregion

        public void ToggleImagenPrecio(bool v)
        {
            if (v) btnPorPrecio.Source = "porPrecioOn.png";
            else btnPorPrecio.Source = "porPrecioOff.png";
        }

        public void ToggleImagenTiempoPreparacion(bool v)
        {
            if (v) btnPorTiempo.Source = "porTiempoOn.png";
            else btnPorTiempo.Source = "porTiempoOff.png";
        }

        public void ToggleImagenCalorias(bool v)
        {
            if (v) btnPorCalorias.Source = "porCaloriasOn.png";
            else btnPorCalorias.Source = "porCaloriasOff.png";
        }

        public void ToggleImagenCantPlatos(bool v)
        {

        }

        public void ToggleImagenDificultad(bool v)
        {
            if (v) btnPorDificultad.Source = "porDificultadOn.png";
            else btnPorDificultad.Source = "porDificultadOff.png";
        }

        public void ToggleImagenPuntuacion(bool v)
        {
            if (v) btnPorPuntaje.Source = "porPuntajeOn.png";
            else btnPorPuntaje.Source = "porPuntajeOff.png";
        }

        public void ToggleImagenMomentoDia(bool v)
        {
            if (v) btnPorMomentoDia.Source = "porMomentoDiaOn.png";
            else btnPorMomentoDia.Source = "porMomentoDiaOff.png";
        }

        public void ToggleImagenEstacion(bool v)
        {
            if (v) btnPorEstacion.Source = "porEstacionOn.png";
            else btnPorEstacion.Source = "porEstacionOff.png";
        }

        public void ResetearSimples()
        {
            btnAptoCeliacos.Source = "aptoCeliacosOff.png";
            btnAptoDiabeticos.Source = "aptoDiabeticosOff.png";
            btnAptoVegetarianos.Source = "aptoVegetarianosOff.png";
            btnAptoVeganos.Source = "aptoVeganosOff.png";
        }

        public void ToggleImagenVegetariano(bool v)
        {
            if (v) btnAptoVegetarianos.Source = "aptoVegetarianosOn.png";
            else btnAptoVegetarianos.Source = "aptoVegetarianosOff.png";
        }

        public void ToggleImagenVegano(bool v)
        {
            if (v) btnAptoVeganos.Source = "aptoVeganosOn.png";
            else btnAptoVeganos.Source = "aptoVeganosOff.png";
        }

        public void ToggleImagenCeliaco(bool v)
        {
            if (v) btnAptoCeliacos.Source = "aptoCeliacosOn.png";
            else btnAptoCeliacos.Source = "aptoCeliacosOff.png";
        }

        public void ToggleImagenDiabetico(bool v)
        {
            if (v) btnAptoDiabeticos.Source = "aptoDiabeticosOn.png";
            else btnAptoDiabeticos.Source = "aptoDiabeticosOff.png";
        }

        private void swtFiltrosUsuario_Toggled(object sender, ToggledEventArgs e)
        {
            if (!swtFiltrosUsuario.IsToggled) ViewModel.ResetAll();
            else ViewModel.CargarFiltrosUsuario();
        }
    }
}