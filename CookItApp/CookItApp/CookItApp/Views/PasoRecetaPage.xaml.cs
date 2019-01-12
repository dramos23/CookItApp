using CookItApp.Models;
using CookItApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasoRecetaPage : ContentPage
	{
        private PasoRecetaVM _PasoRecetaVM;
        Stopwatch timer;

		public PasoRecetaPage (Receta rec, PasoReceta pas)
		{
            InitializeComponent();
            _PasoRecetaVM = new PasoRecetaVM(rec, pas);
            //Borrar despues, es para pruebas.
            
            GenerarControl();
            VisibilidadBotones();
            BindingContext = _PasoRecetaVM;
		}

        private void VisibilidadBotones()
        {
            if (!_PasoRecetaVM.HaySiguiente()) btnSiguiente.IsVisible = false;
            if (!_PasoRecetaVM.HayAnterior()) btnAnterior.IsVisible = false;
        }
        public void GenerarControl()
        {
            PasoReceta paso = _PasoRecetaVM._Paso;
            if(paso._UrlVideo != null)
            {
                GenerarControlVideo(paso);
                return;
            }
            if(paso._TiempoReloj != 0)
            {
                GenerarControlReloj(paso);
                return;
            }
            if(paso._Foto != null)
            {
                GenerarControlImagen(paso);
            }
        }

        //Metodo que genera un control Image con la foto del paso receta y la pone en el StackLayout 'layoutControl'
        private void GenerarControlImagen(PasoReceta paso)
        {
            Image imagen = new Image
            {
                //Ni idea como bindear esto todavia: imagen.Source = _PasoRecetaVM._Paso.imagen;
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill
            };
            layoutControl.Children.Add(imagen);
        }

        private void GenerarControlVideo(PasoReceta paso)
        {
            WebView vid = new WebView
            {
                Source = "https://www.youtube.com/watch?v=8RdswF5bbSA",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layoutControl.Children.Add(vid);
        }


        private void GenerarControlReloj(PasoReceta paso)
        {
            timer = new Stopwatch();
            AgregarLabelATimer();
            List<ImageButton> listaBot = new List<ImageButton>();

            ImageButton btnStart = new ImageButton
            {
                Source = "playTimer.png"
            };
            btnStart.Clicked += BtnStart_Clicked;
            listaBot.Add(btnStart);

            ImageButton btnStop = new ImageButton
            {
                Source = "pauseTimer.png"
            };
            btnStop.Clicked += BtnStop_Clicked;
            listaBot.Add(btnStop);

            ImageButton btnReset = new ImageButton
            {
                Source = "resetTimer.png"
            };
            btnReset.Clicked += BtnReset_Clicked;
            listaBot.Add(btnReset);

            ConfigurarLayoutBotonesTimer(listaBot);            
        }

        private void AgregarLabelATimer()
        {
            TimeSpan ts = TimeSpan.FromSeconds(_PasoRecetaVM._Paso._TiempoReloj);

            lblTimer = new Label
            {
                FontSize = 35,
                TextColor = Color.ForestGreen,
                HorizontalOptions = LayoutOptions.Center,
                Text = ts.ToString(@"h\:mm\:ss")
            };
            layoutControl.Children.Add(lblTimer);
        }

        private void ConfigurarLayoutBotonesTimer(List<ImageButton> listaBot)
        {
            StackLayout layoutBotonesExterior = GenerarPrimerLayoutBotonesTimer();

            foreach (ImageButton butt in listaBot)
            {
                butt.WidthRequest = 50;
                butt.HeightRequest = 50;
                StackLayout layoutBotonesInterior = GenerarSegundoLayoutBotonesTimer();
                layoutBotonesInterior.Children.Add(butt);
                layoutBotonesExterior.Children.Add(layoutBotonesInterior);
            }

            layoutControl.Children.Add(layoutBotonesExterior);
        }

        private StackLayout GenerarSegundoLayoutBotonesTimer()
        {
            StackLayout layoutBotones = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            return layoutBotones;
        }

        private StackLayout GenerarPrimerLayoutBotonesTimer()
        {
            StackLayout layoutBotones = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            return layoutBotones;
        }

        private void BtnReset_Clicked(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Reset();
            TimeSpan tiempo = TimeSpan.FromSeconds(_PasoRecetaVM._Paso._TiempoReloj);
            lblTimer.Text = tiempo.ToString(@"h\:mm\:ss");
        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            if (!timer.IsRunning)
            {
                timer.Start();

                Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                {
                    TimeSpan elapsed = TimeSpan.FromSeconds(_PasoRecetaVM._Paso._TiempoReloj) - timer.Elapsed;
                    lblTimer.Text = elapsed.ToString(@"h\:mm\:ss");
                    if (lblTimer.Text.Equals("0:00:00")) timer.Stop();
                    if (timer.IsRunning) return true;
                    else return false;
                }
                );
            }

        }


        //Metodo que pasa al siguiente paso de la receta. Si no hay un paso siguiente no hace nada.
        public async void PasoSiguiente()
        {
            int idPaso = _PasoRecetaVM._Paso._IdPasoReceta;
            try
            {
                //Se pone la variable "idPaso" sin modificarla porque el ID de PasoReceta empieza en 1, y el indice de la lista
                //en 0.
                PasoReceta proxPaso = _PasoRecetaVM._Receta._Pasos[idPaso];
                await Navigation.PushAsync(new PasoRecetaPage(_PasoRecetaVM._Receta, proxPaso));
            }
            catch
            {

            }
        }

        //Metodo que vuelve al paso anterior de la receta. Si no hay un paso anterior no hace nada.
        public async void PasoAnterior()
        {
            int idPaso = _PasoRecetaVM._Paso._IdPasoReceta;
            try
            {
                //Se hace idPaso -2 porque el id de PasoReceta empieza en 1 y el indice de la lista en 0. Si se pusiera
                PasoReceta pasoAnterior = _PasoRecetaVM._Receta._Pasos[idPaso -2];
                await Navigation.PushAsync(new PasoRecetaPage(_PasoRecetaVM._Receta, pasoAnterior));
            }
            catch
            {

            }
        }

        private void BtnSiguiente_Clicked(object sender, EventArgs e)
        {
            PasoSiguiente();
        }

        private void BtnAnterior_Clicked(object sender, EventArgs e)
        {
            PasoAnterior();
        }

    }
}