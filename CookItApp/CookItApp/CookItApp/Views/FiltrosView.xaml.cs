using CookItApp.Data;
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

        public FiltrosView()
        {
            InitializeComponent();
            InicializarControlador();
        }

        private void InicializarControlador()
        {
            if (!Application.Current.Properties.ContainsKey("ViewModelFiltro"))
            {
                FiltrosVM vm = new FiltrosVM(this);
                Application.Current.Properties.Add("ViewModelFiltro", vm);
                ViewModel = vm;
            }else
            {
                var vm = new object();
                Application.Current.Properties.TryGetValue("ViewModelFiltro", out vm);
                ViewModel = vm as FiltrosVM;
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
            throw new NotImplementedException();
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

        /*
        private void AbrirPanelSecundarioFiltros()
        {         
            ArmarLayoutPorImagen(img);
            ArmarBotonesAceptarCancelar(img);
        }

        private void ArmarBotonesAceptarCancelar(Image filtroClickeado)
        {
            Image botonOk = new Image
            {
                Source = "iconOk.png",
                Style = Application.Current.Resources["estiloBotonImagenGrande"] as Style,
            };
            Grid.SetColumn(botonOk, 0);
            Grid.SetRow(botonOk, 1);
            //Esto tiene que generar un evento que, dependiendo de la imagen que se le pasa, cree un nuevo filtro y lo agregue al diccionario, lista, etc.
            //Despues, cierra la ventana y deja el filtro clickeado.
            TapGestureRecognizer eventoOnClick = ObtenerEventoPorImgFiltro(filtroClickeado);
            botonOk.GestureRecognizers.Add(eventoOnClick);

            Image botonCancelar = new Image
            {
                Source = "iconCancel.png",
                Style = Application.Current.Resources["estiloBotonImagenGrande"] as Style,
            };
            Grid.SetColumn(botonCancelar, 1);
            Grid.SetRow(botonCancelar, 1);

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                CerrarPanelSecundarioFiltros();
                //Toggle Imagen
            };
            botonCancelar.GestureRecognizers.Add(tapGestureRecognizer);

        }

        //Incompleto
        private TapGestureRecognizer ObtenerEventoPorImgFiltro(Image filtroClickeado)
        {
            string source = filtroClickeado.Source.ToString();
            return new TapGestureRecognizer { };
        }

        private void ArmarLayoutPorImagen(Image img)
        {
            switch (img.Source.ToString().Substring(6))
            {
                case "porPrecioOff.png":
                    GenerarLayoutPrecio();
                    break;
                case "porDificultadOff.png":
                    GenerarLayoutDificultad();
                    break;
                case "porEstacionOff.png":
                    GenerarLayoutEstacion();
                    break;
                case "porMomentoDiaOff.png":
                    GenerarLayoutMomentoDia();
                    break;
                case "porPuntajeOff.png":
                    GenerarLayoutPuntaje();
                    break;
                case "porTiempoOff.png":
                    GenerarLayoutTiempo();
                    break;
                case "porCaloriasOff.png":
                    GenerarLayoutCalorias();
                    break;
            };
        }

        private void CerrarPanelSecundarioFiltros()
        {
            //Se esconde el layout con una animacion
            //Se resetea el layout para que pueda ser reusado
            ResetearLayoutGenerado();
        }

        
        private void ResetearLayoutGenerado()
        {

        }

        */


    }
}