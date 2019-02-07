using CookItApp.MarkupExtensions;
using CookItApp.Models;
using CookItApp.ViewModels;
using Octane.Xamarin.Forms.VideoPlayer;
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
        Usuario Usuario;
        private double width;
        private double height;

        public PasoRecetaPage(Receta rec, PasoReceta pas, Usuario usr)
        {
            InitializeComponent();
            _PasoRecetaVM = new PasoRecetaVM(rec, pas);
            Usuario = usr;
            GenerarControl();
            GenerarBotonesAtrasSiguiente();
            BindearDescripcion();
            BindingContext = _PasoRecetaVM;
            GenerarTextoCantPasos();

            Navigation.RemovePage(this);
        }

        private void GenerarTextoCantPasos()
        {
            string cantPasos = "Paso ";
            cantPasos += _PasoRecetaVM.DevolverNumeroPaso();
            txtCantPasos.Text = cantPasos;
        }

        private void BindearDescripcion()
        {
            txtDescripcion.Text = _PasoRecetaVM._Paso._Descripcion;
        }

        private void GenerarBotonesAtrasSiguiente()
        {
            if (_PasoRecetaVM.HaySiguiente())
            {
                btnSiguiente.IsVisible = true;
                var tapSiguiente = new TapGestureRecognizer();
                tapSiguiente.Tapped += (s, e) => {
                    BtnSiguiente_Clicked(s, e);
                };
                btnSiguiente.GestureRecognizers.Add(tapSiguiente);
            }
            else
            {
                btnSiguiente.IsVisible = false;
                btnSiguiente.GestureRecognizers.Clear();
            }
            if (_PasoRecetaVM.HayAnterior())
            {
                btnAnterior.IsVisible = true;
                var tapAnterior = new TapGestureRecognizer();
                tapAnterior.Tapped += (s, e) => {
                    BtnAnterior_Clicked(s, e);
                };
                btnAnterior.GestureRecognizers.Add(tapAnterior);
                btnAnterior.GestureRecognizers.Add(tapAnterior);
            }
            else
            {
                btnAnterior.IsVisible = false;
                btnAnterior.GestureRecognizers.Clear();
            }
        }

        public void GenerarControl()
        {
            PasoReceta paso = _PasoRecetaVM._Paso;
            if (paso._UrlVideo != null)
            {
                GenerarControlVideo(paso);
                return;
            }
            if (paso._TiempoReloj != 0)
            {
                GenerarControlReloj(paso);
                return;
            }
            if (paso._Foto != null)
            {
                GenerarControlImagen(paso);
            }
        }

        //Metodo que genera un control Image con la foto del paso receta y la pone en el StackLayout 'layoutControl'
        private void GenerarControlImagen(PasoReceta paso)
        {
            Image imagen = new Image
            {
                Source = paso.GenerarFoto(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill
            };
            layoutControl.Children.Add(imagen);
        }

        private void GenerarControlVideo(PasoReceta paso)
        {
            videoPlayer.Source = YouTubeVideoIdExtension.Convert(paso._UrlVideo);
            videoPlayer.Opacity = 1;
            videoPlayer.Play();
        }


        private void GenerarControlReloj(PasoReceta paso)
        {
            timer = new Stopwatch();
            AgregarLabelATimer();
            AgregarEventosTapTimer();
            gridTimer.Opacity = 1;
            /*
            Image start = GenerarBotonStart();
            Image stop = GenerarBotonStop();
            Image reset = GenerarBotonReset();

            layoutControl.Children.Add(start);
            layoutControl.Children.Add(stop);
            layoutControl.Children.Add(reset);
            */
        }

        private void AgregarEventosTapTimer()
        {
            var tapReset = new TapGestureRecognizer();
            tapReset.Tapped += (s, e) => {
                ResetearTimer(s, e);
            };
            imgResetearTimer.GestureRecognizers.Add(tapReset);

            var tapStop = new TapGestureRecognizer();
            tapStop.Tapped += (s, e) => {
                DetenerTimer(s, e);
            };
            imgPausarTimer.GestureRecognizers.Add(tapStop);

            var tapStart = new TapGestureRecognizer();
            tapStart.Tapped += (s, e) => {
                ComenzarTimer(s, e);
            };
            imgPlayTimer.GestureRecognizers.Add(tapStart);
        }

        /*
       private Image GenerarBotonReset()
       {
           Image btnReset = new Image
           {
               Source = "resetTimer.png",
               Style = Application.Current.Resources["estiloBotonImagenGrande"] as Style
           };

           
           btnReset.GestureRecognizers.Add(tapGestureRecognizer);
           Grid.SetRow(btnReset, 2);
           Grid.SetColumn(btnReset, 1);
           return btnReset;
       }

       private Image GenerarBotonStop()
       {
           Image btnStop = new Image
           {
               Source = "pauseTimer.png",
               Style = Application.Current.Resources["estiloBotonImagenGrande"] as Style
           };

           var tapGestureRecognizer = new TapGestureRecognizer();
           tapGestureRecognizer.Tapped += (s, e) => {
               DetenerTimer();
           };
           btnStop.GestureRecognizers.Add(tapGestureRecognizer);
           Grid.SetRow(btnStop, 1);
           Grid.SetColumn(btnStop, 1);

           return btnStop;
       }

       private Image GenerarBotonStart()
       {
           Image btnStart = new Image
           {
               Source = "playTimer.png",
               Style = Application.Current.Resources["estiloBotonImagenGrande"] as Style
           };

           var tapStart = new TapGestureRecognizer();
           tapStart.Tapped += (s, e) => {
               ComenzarTimer();
           };
           btnStart.GestureRecognizers.Add(tapStart);
           Grid.SetRow(btnStart, 0);
           Grid.SetColumn(btnStart, 1);

           return btnStart;
       }
       */
        private void AgregarLabelATimer()
        {
            TimeSpan ts = TimeSpan.FromSeconds(_PasoRecetaVM._Paso._TiempoReloj);

            lblTimer = new Label
            {
                FontSize = 80,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Text = ts.ToString(@"h\:mm\:ss")
            };

            Grid.SetRow(lblTimer, 0);
            Grid.SetColumn(lblTimer, 0);
            Grid.SetColumnSpan(lblTimer, 3);
            gridTimer.Children.Add(lblTimer);
        }

        //private void ConfigurarLayoutBotonesTimer(List<ImageButton> listaBot)
        //{
        //    StackLayout layoutBotonesExterior = GenerarPrimerLayoutBotonesTimer();

        //    foreach (ImageButton butt in listaBot)
        //    {
        //        butt.WidthRequest = 50;
        //        butt.HeightRequest = 50;
        //        StackLayout layoutBotonesInterior = GenerarSegundoLayoutBotonesTimer();
        //        layoutBotonesInterior.Children.Add(butt);
        //        layoutBotonesExterior.Children.Add(layoutBotonesInterior);
        //    }

        //    layoutControl.Children.Add(layoutBotonesExterior);
        //}

        //private StackLayout GenerarSegundoLayoutBotonesTimer()
        //{
        //    StackLayout layoutBotones = new StackLayout
        //    {
        //        Orientation = StackOrientation.Vertical,
        //        HorizontalOptions = LayoutOptions.FillAndExpand
        //    };
        //    return layoutBotones;
        //}

        //private StackLayout GenerarPrimerLayoutBotonesTimer()
        //{
        //    StackLayout layoutBotones = new StackLayout
        //    {
        //        Orientation = StackOrientation.Horizontal,
        //        HorizontalOptions = LayoutOptions.FillAndExpand
        //    };
        //    return layoutBotones;
        //}

        private void ResetearTimer(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Reset();
            TimeSpan tiempo = TimeSpan.FromSeconds(_PasoRecetaVM._Paso._TiempoReloj);
            lblTimer.Text = tiempo.ToString(@"h\:mm\:ss");
        }

        private void DetenerTimer(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void ComenzarTimer(object sender, EventArgs e)
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


        //Metodo que pasa al siguiente paso de la receta. Si no hay un paso siguiente no hace nada (no deberia verse la flecha en este caso).
        public async void PasoSiguiente()
        {
            try
            {
                videoPlayer.Pause();
                PasoReceta proxPaso = _PasoRecetaVM.DevolverProximoPaso(); ;
                await Navigation.PushAsync(new PasoRecetaPage(_PasoRecetaVM._Receta, proxPaso, Usuario));
            }
            catch
            {

            }
        }

        //Metodo que vuelve al paso anterior de la receta. Si no hay un paso anterior no hace nada (no deberia verse la flecha en este caso).
        public async void PasoAnterior()
        {
            try
            {
                videoPlayer.Pause();
                PasoReceta pasoAnterior = _PasoRecetaVM.DevolverPasoAnterior();
                await Navigation.PushAsync(new PasoRecetaPage(_PasoRecetaVM._Receta, pasoAnterior, Usuario));
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

        //No anda esto por ahora.
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                //Se guardan los valores de alto (height) y ancho (width)
                this.width = width;
                this.height = height;
                Grid.SetColumnSpan(videoPlayer, 50);
                Grid.SetColumnSpan(gridTimer, 50);

                if (width > height)
                {

                }
                else
                {

                }
            }
        }

        private void btnVolverReceta_Clicked(object sender, EventArgs e)
        {
            VolverReceta();
        }

        private async void VolverReceta()
        {
            await Navigation.PushAsync(new RecetaPage(_PasoRecetaVM._Receta, Usuario));
        }


        protected override bool OnBackButtonPressed()
        {
            if (_PasoRecetaVM.HayAnterior()) PasoAnterior();
            else
            {
                VolverReceta();
            }

            return true;
        }

    }
}