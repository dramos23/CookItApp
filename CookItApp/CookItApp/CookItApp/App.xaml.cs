using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookItApp.Data;
using CookItApp.Servicios;
using CookItApp.Models;
using CookItApp.Views;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using CookItApp.Controladores;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CookItApp
{
    public partial class App : Application
    {
        private static DataBaseController _DataBase;

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
        static RetoService _RetoService;
        static IngredienteUsuarioService _IngredienteUsuarioService;
        static NotificacionService _NotificacionService;

        public App()
        {
            
            InitializeComponent();

            Usuario usuario = App.DataBase.Usuario.Obtener();
            if (usuario == null)
            {
                MainPage = new NavigationPage(new LoginPage())
                {
                    BackgroundColor = Color.Black
                };
            }
            else
            {
                Perfil perfil = DataBase.Perfil.Obtener(usuario._Email);
                usuario._Perfil = perfil;
                MainPage = new NavigationPage(new MasterPage(usuario));
            }



        }

        protected override async void OnStart()
        {
            AppCenter.Start("4cf52d65-8fd4-4f10-85a4-cdb18647417e", typeof(Push));
            bool isEnabled = await Push.IsEnabledAsync();
        }

        protected override void OnSleep()
        {

            
        }

        protected override void OnResume()
        {
           
            // Handle when your app resumes
        }

        public static DataBaseController DataBase
        {
            get
            {
                if (_DataBase == null) _DataBase = new DataBaseController();
                return _DataBase;
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

        public static RetoService RetoService
        {
            get
            {
                if (_RetoService == null) _RetoService = new RetoService();
                return _RetoService;
            }
        }

        public static IngredienteUsuarioService IngredienteUsuarioService
        {
            get
            {
                if (_IngredienteUsuarioService == null) _IngredienteUsuarioService = new IngredienteUsuarioService();
                return _IngredienteUsuarioService;
            }
        }

        public static NotificacionService NotificacionService
        {
            get
            {
                if (_NotificacionService == null) _NotificacionService = new NotificacionService();
                return _NotificacionService;
            }
        }


    }
}
