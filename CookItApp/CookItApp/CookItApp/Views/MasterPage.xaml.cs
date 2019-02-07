using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage, IViewMaster
    {
        public List<MasterPageItem> MenuList { get; set; }

        protected static Usuario Usuario;
        private MasterPageVM _VMMaster;
        protected static List<Perfil> _ListPerfiles { get; set; }

        private bool MostrarMsjCons { get; set; }

        public MasterPage (Usuario usuario)
		{
			InitializeComponent ();
            Notificaciones();
            Usuario = usuario;            

            MenuList = new List<MasterPageItem>();

            var page1 = new MasterPageItem() { Title = "Recetas", Icon = "breakfast.png", TargetType = typeof(ListaRecetasPage) };
            var page2 = new MasterPageItem() { Title = "Historial", Icon = "history.png", TargetType = typeof(HistorialRecetasPage)};
            var page3 = new MasterPageItem() { Title = "Favoritos", Icon = "favorite.png", TargetType = typeof(ListaRecetasFavoritasPage)};
            var page4 = new MasterPageItem() { Title = "Mi Alacena", Icon = "kitchen.png", TargetType = typeof(IngredientesUsuarioView) };
            var page5 = new MasterPageItem() { Title = "Mi Perfil", Icon = "perfil.png", TargetType = typeof(PerfilPage)};
            var page6 = new MasterPageItem() { Title = "Desafios", Icon = "reto.png", TargetType = typeof(DesafioListPage)};
            var page7 = new MasterPageItem() { Title = "Notificaciones", Icon = "notifications.png", TargetType = typeof(ListaNotificacionesPage) };
            var page8 = new MasterPageItem() { Title = "Actualizar Recetario", Icon = "update.png", TargetType = typeof(CargaRecursos) };
            var page9 = new MasterPageItem() { Title = "Salir", Icon = "exit.png", TargetType = typeof(ExitPage) };

            MenuList.Add(page1);
            MenuList.Add(page2);
            MenuList.Add(page3);
            MenuList.Add(page4);
            MenuList.Add(page5);
            MenuList.Add(page6);
            MenuList.Add(page7);
            MenuList.Add(page8);
            MenuList.Add(page9);

            ListMenu.ItemsSource = MenuList;

            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(new ListaRecetasPage(usuario));
            IsPresented = false;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListaRecetasPage), usuario));

            MostrarMsjCons = true;

            _VMMaster = new MasterPageVM(usuario);
            BindingContext = _VMMaster;

            

        }

        private void Notificaciones()
        {
            //if (!AppCenter.Configured)
            //{
                Push.PushNotificationReceived += async (sender, e) =>
                {
                    if (e.CustomData.Keys.Contains("Reto"))
                    {
                       
                        await DisplayAlert(e.Title, e.Message, "OK");

                        UserDialogs.Instance.ShowLoading("Cargando..");

                        var notificacion = await App.NotificacionService.Obtener(Usuario);

                        if (notificacion != null) {

                            int idReto = Convert.ToInt32(notificacion._Pk1);
                            var reto = await App.RetoService.Obtener(idReto);

                            if (reto != null)
                            {
                                if (App.DataBase.Reto.Existe(reto))
                                {
                                    App.DataBase.Reto.Modificar(reto);
                                }
                                else
                                {
                                    App.DataBase.Reto.Guardar(reto);
                                }
                                
                                App.DataBase.Notificacion.Guardar(notificacion);
                                
                                UserDialogs.Instance.HideLoading();

                                await Navigation.PushAsync(new ListaNotificacionesPage(Usuario));


                            }

                        }

                    }         

                };
            //}
        }

        protected override  void OnAppearing()
        {
            if (MostrarMsjCons)
            {
                MostrarMsjCons = false;

                var continuar = Mensajes();
                if (continuar.Result)
                {
                    ControlPerfil();
                }
            }
                
        }

        private async Task<bool> Mensajes()
        {
            bool continuar = true;

            Usuario usuario = App.DataBase.Usuario.Obtener();
            if (usuario._Perfil != null)
            {

                string mensaje = "Hola " + usuario._Perfil._Nombre + "\\n";
                int notificaciones = App.DataBase.Notificacion.SinLeer();
                if (notificaciones > 0)
                {
                    mensaje += "Tienes " + notificaciones.ToString() + " notificaciones sin leer.";
                }
                int retos = App.DataBase.Reto.RetosActivos();
                if (retos > 0)
                {
                    mensaje += "Tienes " + retos.ToString() + " retos activos.";
                }
                if (notificaciones > 0 || retos > 0)
                {
                    await DisplayAlert("Atención!", mensaje, "OK");
                }
            }
            return continuar;

        }


        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (page == typeof(ExitPage))
            {
                EliminarDatosBD();
                await Navigation.PushAsync(new LoginPage(), true);
                Navigation.RemovePage(this);

            }else{

                
                if (page == typeof(CargaRecursos))
                {

                    await Navigation.PushAsync(new CargaRecursos(Usuario, "UPD"), true);
                    Navigation.RemovePage(this);
                    
                }
                else {

                    bool entro = false;

                    if (page == typeof(IngredientesUsuarioView) && entro == false)
                    {
                        entro = true;
                        if (ControlPerfilBoolean())
                        {                            
                            Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario));
                            IsPresented = false;
                        
                        }
                    }

                    if (page == typeof(PerfilPage) && entro == false)
                    {
                        entro = true;
                        if (ControlPerfilBoolean())
                        {
                            Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario, this));
                            IsPresented = false;
                            
                        }
                    }

                    if (entro == false)
                    {
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario));
                        IsPresented = false;
                        

                    }

                


                }
                

            }
        }

        public async void ControlPerfil() {

            if (Usuario._Perfil == null)
            {
                var action = await DisplayAlert("Complete su perfil", "Desea completar su perfil ahora?", "Ahora No", "Bueno");
                if (!action)
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PerfilPage), Usuario));
                    IsPresented = false;
                }
            }            

        }

        public bool ControlPerfilBoolean()
        {

            if (Usuario._Perfil == null)
            {
                string mensaje = "Para utilizar está opción tiene que completar su perfil.";
                UserDialogs.Instance.Alert("Complete su perfil", mensaje, "Continuar");
                return false;

            }
            else
            {
                return true;
            }

        }

        private void EliminarDatosBD()
        {
            App.DataBase.BorrarTodo();
        }


        private void BtnFiltros_Clicked(object sender, EventArgs e)
        {

        }

        public void Actualizar(Perfil perfil)
        {
            Usuario._Perfil = perfil;
            _VMMaster = new MasterPageVM(Usuario);
            BindingContext = _VMMaster;
        }
    }
}