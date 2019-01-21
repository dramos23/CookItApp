using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookItApp.Views;
using CookItApp.Data;
using CookItApp.Models;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CookItApp
{
    public partial class App : Application
    {

        static TokenDatabaseController _TokenDatabase;
        static UsuarioDatabaseController _UsuarioDatabase;
        static PerfilDataBaseController _PerfilDataBase;
        static MomentoDiaDatabaseController _MomentoDiaDatabase;
        static EstacionDataBaseController _EstacionDataBase;
        static RecetaDatabaseController _RecetaDataBase;

        static UsuarioService _RestService;
        static RecetaService _RecetaService;
        static IngredienteService _IngredienteService;
        static IngredienteRecetaService _IngredienteRecetaService;
        static HistorialRecetaService _HistorialRecetaService;
        static ComentarioRecetaService _ComentarioRecetaService;
        static RecetaFavoritaService _RecetaFavoritaService;
        static PerfilService _PerfilService;
        static MomentoDiaService _MomentoDiaService;
        static EstacionService _EstacionService;

        public App()
        {
            InitializeComponent();
            
            Usuario usuario = App.UsuarioDatabase.Obtener();
            if (usuario == null)
            {
                MainPage = new NavigationPage(new LoginPage())
                {
                    BackgroundColor = Color.Black
                };
            }
            else
            {
                Perfil perfil = App.PerfilDataBase.Obtener(usuario._Email);
                usuario._Perfil = perfil;
                MainPage = new NavigationPage(new MasterPage(usuario));
            }

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        public static RecetaDatabaseController RecetaDataBase
        {
            get
            {
                if (_RecetaDataBase == null) _RecetaDataBase = new RecetaDatabaseController();
                return _RecetaDataBase;
            }
        }

        public static UsuarioDatabaseController UsuarioDatabase
        {
            get
            {
                if (_UsuarioDatabase == null) _UsuarioDatabase = new UsuarioDatabaseController();
                return _UsuarioDatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (_TokenDatabase == null) _TokenDatabase = new TokenDatabaseController();
                return _TokenDatabase;
            }
        }


        public static PerfilDataBaseController PerfilDataBase
        {
            get
            {
                if (_PerfilDataBase == null) _PerfilDataBase = new PerfilDataBaseController();
                return _PerfilDataBase;
            }
        }  

        public static MomentoDiaDatabaseController MomentoDiaDataBase
        {
            get
            {
                if (_MomentoDiaDatabase == null) _MomentoDiaDatabase = new MomentoDiaDatabaseController();
                return _MomentoDiaDatabase;
            }
        }

        public static EstacionDataBaseController EstacionDataBase
        {
            get
            {
                if (_EstacionDataBase == null) _EstacionDataBase = new EstacionDataBaseController();
                return _EstacionDataBase;
            }
        }

        public static UsuarioService RestService
        {
            get
            {
                if (_RestService == null) _RestService = new UsuarioService();
                return _RestService;
            }
        }

        public static RecetaService RecetaService
        {
            get
            {
                if (_RecetaService == null) _RecetaService = new RecetaService();
                return _RecetaService;
            }
        }

        public static IngredienteService IngredienteService
        {
            get
            {
                if (_IngredienteService == null) _IngredienteService = new IngredienteService();
                return _IngredienteService;
            }
        }

        public static IngredienteRecetaService IngredienteRecetaService
        {
            get
            {
                if (_IngredienteRecetaService == null) _IngredienteRecetaService = new IngredienteRecetaService();
                return _IngredienteRecetaService;
            }
        }

        public static HistorialRecetaService HistorialRecetaService
        {
            get
            {
                if (_HistorialRecetaService == null) _HistorialRecetaService = new HistorialRecetaService();
                return _HistorialRecetaService;
            }
        }

        public static ComentarioRecetaService ComentarioRecetaService
        {
            get
            {
                if (_ComentarioRecetaService == null) _ComentarioRecetaService = new ComentarioRecetaService();
                return _ComentarioRecetaService;
            }
        }

        public static RecetaFavoritaService RecetaFavoritaService
        {
            get
            {
                if (_RecetaFavoritaService == null) _RecetaFavoritaService = new RecetaFavoritaService();
                return _RecetaFavoritaService;
            }
        }

        public static PerfilService PerfilService
        {
            get
            {
                if (_PerfilService == null) _PerfilService = new PerfilService();
                return _PerfilService;
            }
        }

        public static MomentoDiaService MomentoDiaService
        {
            get
            {
                if (_MomentoDiaService == null) _MomentoDiaService = new MomentoDiaService();
                return _MomentoDiaService;
            }
        }

        public static EstacionService EstacionService
        {
            get
            {
                if (_EstacionService == null) _EstacionService = new EstacionService();
                return _EstacionService;
            }
        }
        /*<<!-- <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2.5*"/>
            <ColumnDefinition Width="2.5*"/>
            <ColumnDefinition Width="2*"/>
            <ColumnDefinition Width="2.5*"/>
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="7*"/>
                <RowDefinition Height="3*"/>
            </Grid.RowDefinitions>
            <Label Grid.Row="0" Grid.Column="0" Text="Menos de" FontSize="Small" FontAttributes="Bold" VerticalOptions="Center" TextColor="{StaticResource colorOnBackground}"/>
            <Entry Grid.Row="0" Grid.Column="1" Keyboard="Numeric" VerticalOptions ="Center" x:Name="entMinimo" />
            <Label Grid.Row="0" Grid.Column="2" Text="Mas de" FontSize="Small" FontAttributes="Bold" VerticalOptions ="Center" TextColor="{StaticResource colorOnBackground}"/>
            <Entry Grid.Row="0" Grid.Column="3" Keyboard="Numeric" VerticalOptions ="Center" x:Name="entMaximo" />

            <Image  Grid.Row="1" Grid.Column="0" Grid.ColumnSpan="2"
                    Source="iconOk.png"
                   Style="{StaticResource estiloBotonImagen}"
                        >
                <Image.GestureRecognizers>
                    <TapGestureRecognizer Tapped="Ok_Tapped" NumberOfTapsRequired="1"/>
                </Image.GestureRecognizers>
            </Image>
            <Image Grid.Row="1" Grid.Column="2" Grid.ColumnSpan="2"
                    Source="iconCancel.png"
                   Style="{StaticResource estiloBotonImagen}"
                       >
                <Image.GestureRecognizers>
                    <TapGestureRecognizer Tapped="Cancel_Tapped" NumberOfTapsRequired="1"/>
                </Image.GestureRecognizers>
            </Image>
            -->*/
        
    }
}
