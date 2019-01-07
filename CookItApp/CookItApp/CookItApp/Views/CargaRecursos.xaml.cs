using CookItApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookItApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CargaRecursos : ContentPage
	{
		public CargaRecursos (Usuario usuario, string modo)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            lblTexto.Text = "Iniciando la carga de recursos";
            if (modo.Equals("INS"))
            {
                CargaDatosAplicativo(usuario);
            }
            else {
                ActualizarRecetario(usuario);
            }
        }

        public async void CargaDatosAplicativo(Usuario usuario)
        {
            lblTexto.Text = "Cargando perfil..";
            Perfil perfil = await App.PerfilService.Obtener(usuario);
            if (perfil != null)
            {
                App.PerfilDataBase.Guardar(perfil);
            }
            usuario._Perfil = perfil ?? null;

            lblTexto.Text = "Cargando momentos del día..";
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                App.MomentoDiaDataBase.GuardarList(momentos);
            }

            lblTexto.Text = "Cargando estaciones del año..";
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                App.EstacionDataBase.GuardarList(estaciones);
            }
            if (momentos != null && estaciones != null)
            {
                lblTexto.Text = "Cargando recetas..";
                List<Receta> recetas = await App.RecetaService.ObtenerList();
                if (recetas != null)
                {
                    App.RecetaDataBase.GuardarList(recetas);

                    await Navigation.PushAsync(new MasterPage(usuario), true);
                    Navigation.RemovePage(this);

                }
            }
        }

        public async void ActualizarRecetario(Usuario usuario)
        {
          
            lblTexto.Text = "Descargando momentos del día..";
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                lblTexto.Text = "Borrando momentos del día..";
                App.MomentoDiaDataBase.BorrarTodo();
                lblTexto.Text = "Guardando momentos del día..";
                App.MomentoDiaDataBase.GuardarList(momentos);
            }

            lblTexto.Text = "Descargando estaciones del año..";
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                lblTexto.Text = "Borrando estaciones del año..";
                App.EstacionDataBase.BorrarTodo();
                lblTexto.Text = "Guardando estaciones del año..";
                App.EstacionDataBase.GuardarList(estaciones);
            }
            if (momentos != null && estaciones != null)
            {
                lblTexto.Text = "Descargando recetas..";
                List<Receta> recetas = await App.RecetaService.ObtenerList();
                if (recetas != null)
                {
                    lblTexto.Text = "Borrando recetas..";
                    App.RecetaDataBase.BorrarTodo();
                    lblTexto.Text = "Guardando recetas..";
                    App.RecetaDataBase.GuardarList(recetas);

                    await Navigation.PushAsync(new MasterPage(usuario), true);
                    Navigation.RemovePage(this);

                }
            }
        }


    }
}