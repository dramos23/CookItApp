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
	public partial class MasterPage : MasterDetailPage
    {
        public List<MasterPageItem> MenuList { get; set; }

        protected static Usuario Usuario;
        private MasterPageVM _VMMaster;
        protected static List<Perfil> _ListPerfiles { get; set; }

        public MasterPage (Usuario usuario)
		{
			InitializeComponent ();
            Notificaciones();
            Usuario = usuario;
            ControlPerfil();

            MenuList = new List<MasterPageItem>();

            var page1 = new MasterPageItem() { Title = "Recetas", Icon = "breakfast.png", TargetType = typeof(ListaRecetasPage) };
            var page2 = new MasterPageItem() { Title = "Historial", Icon = "history.png" /*, TargetType = typeof(View1) */};
            var page3 = new MasterPageItem() { Title = "Favoritos", Icon = "favorite.png"/*, TargetType = typeof(View1) */};
            var page4 = new MasterPageItem() { Title = "Mi Alacena", Icon = "kitchen.png"/*, TargetType = typeof(View1) */};
            var page5 = new MasterPageItem() { Title = "Mi Perfil", Icon = "perfil.png", TargetType = typeof(PerfilPage)};
            var page6 = new MasterPageItem() { Title = "Actualizar Recetario", Icon = "update.png", TargetType = typeof(CargaRecursos) };
            var page7 = new MasterPageItem() { Title = "Salir", Icon = "exit.png", TargetType = typeof(ExitPage) };

            MenuList.Add(page1);
            MenuList.Add(page2);
            MenuList.Add(page3);
            MenuList.Add(page4);
            MenuList.Add(page5);
            MenuList.Add(page6);
            MenuList.Add(page7);

            ListMenu.ItemsSource = MenuList;

            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(new ListaRecetasPage(usuario));
            IsPresented = false;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListaRecetasPage), usuario));

            _VMMaster = new MasterPageVM(usuario);
            BindingContext = _VMMaster;

        }

        private void Notificaciones()
        {
            if (!AppCenter.Configured)
            {
                Push.PushNotificationReceived += (sender, e) =>
                {

                    DisplayAlert(e.Title, e.Message, "OK");

                };
            }
        }



        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (page == typeof(ExitPage))
            {
                EliminarDatosBD();
                await Navigation.PushAsync(new LoginPage(), true);
                this.Navigation.RemovePage(this);
            }else{

                if (page == typeof(CargaRecursos))
                {
                    await Navigation.PushAsync(new CargaRecursos(Usuario, "UPD"), true);
                    Navigation.RemovePage(this);
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page, Usuario));
                    IsPresented = false;
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

        private void EliminarDatosBD()
        {
            App.DataBase.BorrarTodo();
        }

        public static void ActualizarPerfil(Perfil perfil)
        {
            Usuario._Perfil = perfil;
        }


    }
}