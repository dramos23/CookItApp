using CookItApp.Models;
using CookItApp.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaRecetasPage : ContentPage
	{
        RecetaListVM _VMRecetas;
        Usuario Usuario;
        //Atributos necesarios para revisar si el usuario rotó el celular
        private double width;
        private double height;

        public ListaRecetasPage (Usuario Usuario)
		{
			InitializeComponent ();
            this.Usuario = Usuario;
            _VMRecetas = new RecetaListVM(null);
            InicializarControladorFiltros();
            BindingContext = _VMRecetas;
        }

        private void InicializarControladorFiltros()
        {
            if (!Application.Current.Properties.ContainsKey("ViewModelFiltro"))
            {
                FiltrosVM vm = new FiltrosVM(Usuario);
                Application.Current.Properties.Add("ViewModelFiltro", vm);
            }
        }

        //Método onclick que muestra una receta en mayor detalle cuando se clickea una receta de la lista.
        public async void RecetaSeleccionada(object sender, SelectedItemChangedEventArgs args)
        {
            //Se levanta y castea la receta recibida del evento.
            if (!(args.SelectedItem is Receta receta))
            {
                return;
            }

            //Receta rec = App.DataBase.Receta.Obtener(receta._IdReceta);
            Receta rec = await App.RecetaService.Obtener(receta);

            if (rec != null)
            {
                //Agrego receta al historial del usuario
                AgregarRecetaHistorial(rec);
                 
                //Se cambia a una nueva página tipo RecetaPage que muestra la receta en mas detalle.
                await Navigation.PushAsync(new RecetaPage(rec, Usuario));

                ListaRecetas.SelectedItem = null;
            }
        }

        //private async void BtnBuscar_Clicked(object sender, EventArgs e)
        //{
        //    await BtnBuscar.ScaleTo(1.2, 100);
        //    await BtnBuscar.ScaleTo(1, 100);
        //}

        private async void BtnFiltros_Clicked(object sender, EventArgs e)
        {
            var pagFiltros = new FiltrosView(Usuario);
            await PopupNavigation.Instance.PushAsync(pagFiltros);
        }

        //Metodo que detecta el evento de rotacion del celular y cambia el layout de la página.
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                //Se guardan los valores de alto (height) y ancho (width)
                this.width = width;
                this.height = height;

                //Si el ancho es mas que largo, o sea que el celular esta "acostado" o en posición horizontal...
                if (width > height)
                {
                    //Se borran todas las referencias a tamaños de columnas y filas para reconstruirlas.
                    gridPrincipal.RowDefinitions.Clear();
                    gridPrincipal.ColumnDefinitions.Clear();
                    gridPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                    gridPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8, GridUnitType.Star) });
                }
                /* Si el ancho es menos que el largo el celular esta  "parado" o en posición vertical. Se siguen los mismos pasos generales
                que arriba pero cambian los valores de alto/ancho de las columnas y filas. */
                else
                {
                    gridPrincipal.RowDefinitions.Clear();
                    gridPrincipal.ColumnDefinitions.Clear();
                    gridPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    gridPrincipal.RowDefinitions.Add(new RowDefinition { Height = new GridLength(9, GridUnitType.Star) });
                }
                GenerarDataTemplateListaReceta();
            }
        }


        private void GenerarDataTemplateListaReceta()
        {

            DataTemplate dt = new DataTemplate(() =>
            {
                //La grid primaria tiene dos partes: la foto (columna 0) y el resto de la información de la receta (columna 1)
                Grid gridPrimaria = new Grid();
                //La grid de texto varia la cantidad de filas y columnas que usa dependiendo de la orientacion del celular, esta definido en otro metodo mas abajo
                Grid gridTexto = new Grid();
                gridPrimaria.Children.Add(gridTexto);

                //Si la pantalla esta acostada o parada cambian los valores de ancho de las columnas para que queden bien presentados los controles.
                if (width > height)
                {
                    gridPrimaria.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    gridPrimaria.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) });
                }
                else
                {
                    gridPrimaria.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3.5, GridUnitType.Star) });
                    gridPrimaria.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6.5, GridUnitType.Star) });
                }

                GenerarImagenFotoReceta(gridPrimaria);
                GenerarTextoListaReceta(gridTexto);

                return new ViewCell { View = gridPrimaria };
            });
            //Se le da a la lista de recetas el datatemplate que acabamos de crear para que se muestren los controles como los definimos.
            ListaRecetas.ItemTemplate = dt;
    }

        private void GenerarTextoListaReceta(Grid grid)
        {
            Grid.SetColumn(grid, 1);
            grid.Margin = 1;
            GenerarFilasTextoListaReceta(grid);

            Label titulo = new Label
            {
                //Le aplicamos un estilo definido en app.xaml para acortar codigo.
                Style = Application.Current.Resources["styleLabelTitulo"] as Style,

            };            
            //A cada label hay que crearle un binding para que tome el atributo de la clase Receta automaticamente por cada elemento de la lista.
            titulo.SetBinding(Label.TextProperty, "_Titulo");
            Grid.SetRow(titulo, 0);
            Grid.SetColumnSpan(titulo, 4);
            grid.Children.Add(titulo);

            Label descripcion = GenerarLabelDescripcion();
            //descripcion.SetBinding(Label.TextProperty, "_Descripcion"); -- NO IMPLEMENTADO TODAVIA
            grid.Children.Add(descripcion);
            //Para que la descripcion no quede limitada a una sola columna (hay 4) se le dice setea el ColumnSpan a 4, para que pueda ocupar 4 columnas.
            Grid.SetColumnSpan(descripcion, 4);

            Label dificultad = GenerarLabelDificultad();
            grid.Children.Add(dificultad);
            Image estrellasDif = GenerarImagenEstrellasDif();
            grid.Children.Add(estrellasDif);

            Label puntajeTotal = GenerarLabelPuntajeTotal();
            grid.Children.Add(puntajeTotal);
            Image estrellasPun = GenerarImagenEstrellasPuntaje();
            grid.Children.Add(estrellasPun);
            
            if(width > height)
            {
                Grid.SetColumn(estrellasDif, 1);
                Grid.SetColumn(estrellasPun, 3);
                Grid.SetRow(estrellasDif, 2);
                Grid.SetRow(estrellasPun, 2);
            }
            else
            {
                Grid.SetColumn(estrellasDif, 1);
                Grid.SetColumn(estrellasPun, 1);
                Grid.SetRow(estrellasDif, 2);
                Grid.SetRow(estrellasPun, 3);
            }

        }

        private Image GenerarImagenEstrellasPuntaje()
        {
            Image img = new Image
            {
                Margin = 0,
                HeightRequest = 15,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            img.SetBinding(Image.SourceProperty, "_RutaFotoPuntajeTotal");
            if (width < height) Grid.SetColumnSpan(img, 2);
            return img;
        }

        private Image GenerarImagenEstrellasDif()
        {
            Image img = new Image
            {
                Margin = 0,
                HeightRequest = 15,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            img.SetBinding(Image.SourceProperty, "_RutaFotoDificultad");
            if (width < height) Grid.SetColumnSpan(img, 2);
            return img;
        }

        private Label GenerarLabelDificultad()
        {
            Label dificultad = new Label
            {
                Style = Application.Current.Resources["styleLabelDifficulty"] as Style,
                Text = "Dificultad: "
            };
            Grid.SetRow(dificultad, 2);
            Grid.SetColumn(dificultad, 0);
            if (width < height) Grid.SetColumnSpan(dificultad, 2);
            return dificultad;
        }


        private Label GenerarLabelPuntajeTotal()
        {
            Label puntaje = new Label
            {
                Style = Application.Current.Resources["styleLabelDifficulty"] as Style,
                Text = "Puntaje: "
            };
            if (width > height)
            {
                Grid.SetRow(puntaje, 2);
                Grid.SetColumn(puntaje, 2);
            }
            else
            {
                Grid.SetRow(puntaje, 3);
                Grid.SetColumn(puntaje, 0);
                Grid.SetColumnSpan(puntaje, 2);
            }
            return puntaje;
        }

        private Label GenerarLabelDescripcion()
        {
            Label descripcion = new Label
            {
                Style = Application.Current.Resources["styleLabelLongText"] as Style,
                //MaxLines hace que el label pueda tener X renglones de largo.
                MaxLines = 2,
                Text = "Aca va la descripcion que todavia no esta este texto esta muy largo para probar a ver que pasa " +
                "si el texto es demasiado largo para entrar en la caja de una, estaria bueno que se corte pero sino seguimos probando."
            };
            if (width > height) descripcion.MaxLines = 3;
            else descripcion.Margin = new Thickness(0, 0, 2, 0);
            Grid.SetRow(descripcion, 1);
            return descripcion;
        }

        private void GenerarFilasTextoListaReceta(Grid grid)
        {
            if (width > height)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            }
            else
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
            }
        }

        private void GenerarImagenFotoReceta(Grid grid)
        {
            Image img = new Image
            {
                Source = "fondoFrutillas.jpg",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.Fill,
                Margin = new Thickness(2, 0, 0, 0)
            };
            grid.Children.Add(img);
            Grid.SetColumn(img, 0);
        }

        //Falta integrar los filtros del viewmodel de filtros
        private void BuscarReceta_TextChanged(object sender, TextChangedEventArgs e)
        {

            var keyword = BuscarReceta.Text;

            if (keyword.Length >= 1)
            {
                List<Receta> resultado = App.DataBase.Receta.ObtenerList().Where(c => c._Titulo.ToLower().Contains(keyword.ToLower())).ToList();
                AplicarFiltrosALista(resultado);
            }
            else
            {
                List<Receta> resultado = App.DataBase.Receta.ObtenerList().ToList();
                AplicarFiltrosALista(resultado);
            }

        }

        private void AplicarFiltrosALista(List<Receta> recetas)
        {
            var vm = new object();
            Application.Current.Properties.TryGetValue("ViewModelFiltro", out vm);
            FiltrosVM filtros = vm as FiltrosVM;

            if (filtros.FiltroCeliaco) recetas = recetas.FindAll(x => x._AptoCeliacos == true);
            if (filtros.FiltroVegetariano) recetas = recetas.FindAll(x => x._AptoVegetarianos == true);
            if (filtros.FiltroVegano) recetas = recetas.FindAll(x => x._AptoVeganos == true);
            if (filtros.FiltroDiabetico) recetas = recetas.FindAll(x => x._AptoDiabeticos == true);
            if (filtros.FiltroCeliaco) recetas = recetas.FindAll(x => x._AptoCeliacos == true);
            if (filtros.FiltroPrecio) {
                if(filtros.FiltroPrecioMin != -1) {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroPrecioMin);
                }
                if (filtros.FiltroPrecioMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroPrecioMax);
                }
            }

            if (filtros.FiltroCalorias)
            {
                if (filtros.FiltroCaloriasMin != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroCaloriasMin);
                }
                if (filtros.FiltroCaloriasMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroCaloriasMax);
                }
            }
            if (filtros.FiltroTiempoPreparacion)
            {
                if (filtros.FiltroTiempoPreparacionMin != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroTiempoPreparacionMin);
                }
                if (filtros.FiltroTiempoPreparacionMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroTiempoPreparacionMax);
                }
            }
            if (filtros.FiltroCantPlatos)
            {
                if (filtros.FiltroCantPlatosMin != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroCantPlatosMin);
                }
                if (filtros.FiltroCantPlatosMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroCantPlatosMax);
                }
            }

            if (filtros.FiltroDificultad)
            {
                if (filtros.FiltroDificultadMin != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroDificultadMin);
                }
                if (filtros.FiltroDificultadMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroDificultadMax);
                }
            }

            if (filtros.FiltroPuntuacion)
            {
                if (filtros.FiltroPuntuacionMin != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo >= filtros.FiltroPuntuacionMin);
                }
                if (filtros.FiltroPuntuacionMax != -1)
                {
                    recetas = recetas.FindAll(x => x._Costo <= filtros.FiltroPuntuacionMax);
                }
            }

            if (filtros.FiltroMomentoDia)
            {
                recetas = recetas.FindAll(x => x._IdMomentoDia == filtros.FiltroMomentoDiaId);
            }


            if (filtros.FiltroEstacion)
            {
                recetas = recetas.FindAll(x => x._IdEstacion == filtros.FiltroEstacionId);
            }

            _VMRecetas = new RecetaListVM(recetas);
            BindingContext = _VMRecetas;

        }

        private async void AgregarRecetaHistorial(Receta receta) {

            HistorialReceta historialReceta = new HistorialReceta()
            {
                _Email = Usuario._Email,
                _IdReceta = receta._IdReceta,
                _FechaHora = DateTime.Now
                
            };

            HistorialReceta historial = await App.HistorialRecetaService.Alta(historialReceta);

            if (historial != null) {

                App.DataBase.HistorialReceta.Guardar(historial);

            }

        }


    }
}