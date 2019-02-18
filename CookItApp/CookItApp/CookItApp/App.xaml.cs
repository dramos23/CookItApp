using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.Views;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using CookItApp.Controladores;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CookItApp
{
    public partial class App : Application
    {
        public static double ScreenHeight { get; set; }
        public static double ScreenWidth { get; set; }

        private static bool configNoti;
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
        static EstadoRetoService _EstadoRetoService;
        static SupermercadoService _SupermercadoService;
        static AppCenterNotiService _AppCenterNotiService;

        public App()
        {
            
            InitializeComponent();

            
            ConfigNoti = false;

            try
            {

                Usuario usuario = App.DataBase.Usuario.Obtener();
                if (usuario == null)
                {
                    App.DataBase.BorrarTodo();
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    Perfil perfil = DataBase.Perfil.Obtener(usuario._Email);
                    usuario._Perfil = perfil;
                    MainPage = new NavigationPage(new MasterPage(usuario));
                }

            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

        }





        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
            Usuario usuario = App.DataBase.Usuario.Obtener();
            if (usuario != null)
            {
                Actualizacion act = new Actualizacion(usuario);
                act.ActualizacionAplicativo();
            }

        }

        protected override void OnResume()
        {
           // Mensajes();
        }



        public static bool ConfigNoti
        {
            get
            {
                return configNoti;
            }
            set
            {
                configNoti = value;
            }
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

        public static EstadoRetoService EstadoRetoService
        {
            get
            {
                if (_EstadoRetoService == null) _EstadoRetoService = new EstadoRetoService();
                return _EstadoRetoService;
            }
        }

        public static SupermercadoService SupermercadoService
        {
            get
            {
                if (_SupermercadoService == null) _SupermercadoService = new SupermercadoService();
                return _SupermercadoService;
            }
        }

        public static AppCenterNotiService AppCenterNotiService
        {
            get
            {
                if (_AppCenterNotiService == null) _AppCenterNotiService = new AppCenterNotiService();
                return _AppCenterNotiService;
            }
        }


    }
}
