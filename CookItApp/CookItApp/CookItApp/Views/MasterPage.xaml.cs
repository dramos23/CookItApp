using Acr.UserDialogs;
using CookItApp.Data;
using CookItApp.Models;
using CookItApp.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
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
        //public List<MasterPageItem> MenuList { get; set; }

        private Usuario Usuario { get; set; }
        private MasterPageVM VMMaster { get; set; }

        private int IdR { get; set; }
        private int IdN { get; set; }
        

        private bool MostrarMsjCons { get; set; }

        public MasterPage (Usuario usuario)
		{
			InitializeComponent ();
            Usuario = usuario;
            MostrarMsjCons = true;

            App.Vista = this;

            NavigationPage.SetHasNavigationBar(this, false);

            VMMaster = new MasterPageVM(usuario, this);
            BindingContext = VMMaster;            

        }

        public void Notificacion(PushNotificationReceivedEventArgs e)
        {
            Notificaciones(e);
        }


        private async Task Notificaciones(PushNotificationReceivedEventArgs e)
        {

            if (e.CustomData.Keys.Contains("Reto"))
            {
                int idReto = Convert.ToInt32(e.CustomData["RetoID"]);
                int idNoti = Convert.ToInt32(e.CustomData["NotificacionID"]);

                await ProcesarNotificacion(e, idNoti, idReto);
            }
            else
            {
                await UserDialogs.Instance.AlertAsync(e.Message, e.Title, "Continuar");
            }

        }

        


        private async Task ProcesarNotificacion(PushNotificationReceivedEventArgs e, int idNoti,int idReto)
        {
            bool notificacion = await Usuario._Perfil.TraerNotificacion(idNoti);
            if (notificacion)
            {
                bool reto = await Usuario._Perfil.TraerReto(idReto, this);
                if (reto)
                {
                    

                    await UserDialogs.Instance.AlertAsync(e.Message, e.Title, "Continuar");

                    //await Navigation.PushAsync(new ListaNotificacionesPage(Usuario));

                }

            } 
        }

        protected override void OnAppearing()
        {

            if (MostrarMsjCons)
            {
                MostrarMsjCons = false;

                var continuar = Mensajes();
                if (continuar)
                {
                    ControlPerfil();
                }
            }
        }

        private bool Mensajes()
        {
            bool continuar = true;

            Usuario usuario = App.DataBase.Usuario.Obtener();
            if (usuario._Perfil != null)
            {

                string mensaje = "Hola " + usuario._Perfil._Nombre + ",\\n";
                int notificaciones = App.DataBase.Notificacion.SinLeer();
                if (notificaciones > 0)
                {
                    mensaje += "Tienes " + notificaciones.ToString() + " notificacion/es sin leer.\\n";
                }
                int retos = App.DataBase.Reto.RetosActivos();
                if (retos > 0)
                {
                    mensaje += "Tienes " + retos.ToString() + " desafio/s activos.";
                }
                if (notificaciones > 0 || retos > 0)
                {
                    mensaje = (mensaje as string).Replace("\\n", Environment.NewLine + Environment.NewLine);
                    UserDialogs.Instance.AlertAsync(mensaje, "Atención!", "Continuar");
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
                VMMaster.ActualizarUUID(Usuario);
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
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario, this));
                        IsPresented = false;
                    }

                    if (page == typeof(ListaSupermercadoPage) && entro == false)
                    {
                        entro = true;

                        var position = await Ubicacion();

                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, position));
                        IsPresented = false;
                    }

                    if (entro == false)
                    {
                        Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario));
                        IsPresented = false;
                    }
                }
            }
            
            
        }

        private async Task<Position> Ubicacion()
        {
            
            var action = await DisplayAlert("Supermercado más cercano.", "Desea obtener los supermercados más cercanos a su ubicación?", "Si", "No");
            if (action)
            {
                try {

                    UserDialogs.Instance.ShowLoading("Ubicando..");

                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 30;

                    TimeSpan? tiempo = TimeSpan.MaxValue;
                    Position position = await locator.GetPositionAsync(tiempo);


                    UserDialogs.Instance.HideLoading();
                    return position;

                    
                }
                catch
                {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        public void ControlPerfil() {

            if (Usuario._Perfil == null)
            {
                UserDialogs.Instance.AlertAsync("Aún no has completado tú perfil, hacerlo habilitara más funciones!", "Complete su perfil", "Continuar");

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
            VMMaster = new MasterPageVM(Usuario, this);
            BindingContext = VMMaster;
        }

        public void Gamificacion()
        {
            VMMaster = new MasterPageVM(Usuario, this);
            LblCat.Text = VMMaster.Categoria;
            LblNiv.Text = VMMaster.ProxNivel;
            
            
        }


    }
}