using CookItApp.Models;
using System.Collections.Generic;

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
            lblTexto.Text = "Cargando ingredientes..";
            List<Ingrediente> ingredientes = await App.IngredienteService.ObtenerList();
            if (ingredientes != null)
            {
                App.DataBase.Ingrediente.GuardarList(ingredientes);
            }

            lblTexto.Text = "Cargando momentos del día..";
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                App.DataBase.MomentoDia.GuardarList(momentos);
            }

            lblTexto.Text = "Cargando estaciones del año..";
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                App.DataBase.Estacion.GuardarList(estaciones);
            }

            lblTexto.Text = "Cargando recetas..";
            List<Receta> recetas = await App.RecetaService.ObtenerList();
            if (recetas != null)
            {
                App.DataBase.Receta.GuardarList(recetas);                    
            }

            lblTexto.Text = "Cargando hitorial de recetas..";
            List<HistorialReceta> historialReceta = await App.HistorialRecetaService.ObtenerList(usuario);
            if (historialReceta != null)
            {
                usuario._ListaHistorialRecetas = historialReceta;
                App.DataBase.HistorialReceta.GuardarList(historialReceta);
            }

            lblTexto.Text = "Cargando perfil..";
            Perfil perfil = await App.PerfilService.Obtener(usuario);
            if (perfil != null)
            {
                App.DataBase.Perfil.Guardar(perfil);
                perfil.InsertarBD();

                usuario._Perfil = perfil ?? null;
                await Navigation.PushAsync(new MasterPage(usuario), true);
                Navigation.RemovePage(this);

            }


            
        }

        public async void ActualizarRecetario(Usuario usuario)
        {
          
            lblTexto.Text = "Descargando momentos del día..";
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                lblTexto.Text = "Borrando momentos del día..";
                App.DataBase.MomentoDia.BorrarTodo();
                lblTexto.Text = "Guardando momentos del día..";
                App.DataBase.MomentoDia.GuardarList(momentos);
            }

            lblTexto.Text = "Descargando estaciones del año..";
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                lblTexto.Text = "Borrando estaciones del año..";
                App.DataBase.Estacion.BorrarTodo();
                lblTexto.Text = "Guardando estaciones del año..";
                App.DataBase.Estacion.GuardarList(estaciones);
            }
            if (momentos != null && estaciones != null)
            {
                lblTexto.Text = "Descargando recetas..";
                List<Receta> recetas = await App.RecetaService.ObtenerList();
                if (recetas != null)
                {
                    lblTexto.Text = "Borrando recetas..";
                    App.DataBase.Receta.BorrarTodo();
                    lblTexto.Text = "Guardando recetas..";
                    App.DataBase.Receta.GuardarList(recetas);

                    await Navigation.PushAsync(new MasterPage(usuario), true);
                    Navigation.RemovePage(this);

                }
            }
        }


    }
}