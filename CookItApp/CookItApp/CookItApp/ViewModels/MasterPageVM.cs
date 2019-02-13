using CookItApp.Models;
using CookItApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class MasterPageVM
    {
        public string Nombre { get; set; }
        public ImageSource Foto { get; set; }

        public string Categoria { get; set; }
        public string ProxNivel { get; set; }

        public ObservableCollection<MasterPageItem> ListMenu { get; set; }

        public MasterPageVM(Usuario usuario, MasterPage masterPage)
        {
            Nombre = usuario._Perfil != null ? (usuario._Perfil._Nombre + " " + usuario._Perfil._Apellido) : usuario._Email;
            Foto = (usuario._Perfil != null && usuario._Perfil._Foto != null) ? usuario._Perfil.ImageFoto() : "img_user.jpg";
            
            masterPage.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListaRecetasPage), usuario));
            CargarMenu();
            CargarCategoria(usuario._Perfil);
            CalcularProxNivel(usuario._Perfil);

        }

        private void CargarCategoria(Perfil perfil)
        {
            if (perfil != null)
            {
                Categoria = perfil._Categoria.ToString();
            }
            else
            {
                Categoria = null;
            }
        }

        private void CalcularProxNivel(Perfil perfil)
        {
            if (perfil != null)
            {
                int puntaje = perfil._Puntuacion;
                Perfil.Categoria categoria = perfil._Categoria;

                if (puntaje < 250) //proximo nivel AMATÉR
                {                    
                    int PuntajeMax = 250;
                    ProxNivel = "Próximo Nivel " + puntaje + "/" + PuntajeMax;
                }
                if (puntaje >= 250 && puntaje < 500) // cocinero
                {
                    int PuntajeMin = 250;
                    int PuntajeMax = 500;

                    int punt = puntaje - PuntajeMin;

                    ProxNivel = "Próximo Nivel " + punt + "/" + PuntajeMax;
                }
                if (puntaje >= 500 && puntaje < 750) // subchef
                {
                    int PuntajeMin = 500;
                    int PuntajeMax = 750;

                    int punt = puntaje - PuntajeMin;

                    ProxNivel = "Próximo Nivel " + punt + "/" + PuntajeMax;
                }
                if (puntaje >= 750 && puntaje < 1000) //chef
                {
                    int PuntajeMin = 750;
                    int PuntajeMax = 1000;

                    int punt = puntaje - PuntajeMin;

                    ProxNivel = "Próximo Nivel " + punt + "/" + PuntajeMax;
                }
                if (puntaje >= 1000 && puntaje < 1250) //master
                {
                    int PuntajeMin = 1000;
                    int PuntajeMax = 1250;

                    int punt = puntaje - PuntajeMin;

                    ProxNivel = "Próximo Nivel " + punt + "/" + PuntajeMax;
                }

            }
        }

        private void CargarMenu()
        {
            ListMenu = new ObservableCollection<MasterPageItem>();

            var page1 = new MasterPageItem() { Title = "Recetas", Icon = "breakfast.png", TargetType = typeof(ListaRecetasPage) };
            var page2 = new MasterPageItem() { Title = "Historial", Icon = "history.png", TargetType = typeof(HistorialRecetasPage) };
            var page3 = new MasterPageItem() { Title = "Favoritos", Icon = "favorite.png", TargetType = typeof(ListaRecetasFavoritasPage) };
            var page4 = new MasterPageItem() { Title = "Mi Alacena", Icon = "kitchen.png", TargetType = typeof(IngredientesUsuarioView) };
            var page5 = new MasterPageItem() { Title = "Mi Perfil", Icon = "perfil.png", TargetType = typeof(PerfilPage) };
            var page6 = new MasterPageItem() { Title = "Desafios", Icon = "reto.png", TargetType = typeof(DesafioListPage) };
            var page7 = new MasterPageItem() { Title = "Notificaciones", Icon = "notifications.png", TargetType = typeof(ListaNotificacionesPage) };
            var page8 = new MasterPageItem() { Title = "Actualizar Recetario", Icon = "update.png", TargetType = typeof(CargaRecursos) };
            var page9 = new MasterPageItem() { Title = "Supermercados", Icon = "tienda.png", TargetType = typeof(ListaSupermercadoPage) };
            var page10 = new MasterPageItem() { Title = "Salir", Icon = "exit.png", TargetType = typeof(ExitPage) };

            ListMenu.Add(page1);
            ListMenu.Add(page2);
            ListMenu.Add(page3);
            ListMenu.Add(page4);
            ListMenu.Add(page5);
            ListMenu.Add(page6);
            ListMenu.Add(page7);
            ListMenu.Add(page8);
            ListMenu.Add(page9);
            ListMenu.Add(page10);

        }


    }
}
