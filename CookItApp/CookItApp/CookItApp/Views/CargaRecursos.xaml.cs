using Acr.UserDialogs;
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
            
            UserDialogs.Instance.ShowLoading("Iniciando la carga de recursos");
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
            
            UserDialogs.Instance.ShowLoading("Cargando ingredientes..");
            List<Ingrediente> ingredientes = await App.IngredienteService.ObtenerList();
            if (ingredientes != null)
            {
                App.DataBase.Ingrediente.GuardarList(ingredientes);
            }

            //lblTexto.Text = "Cargando momentos del día..";
            UserDialogs.Instance.ShowLoading("Cargando momentos del día..");
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                App.DataBase.MomentoDia.GuardarList(momentos);
            }

            UserDialogs.Instance.ShowLoading("Cargando estados reto..");
            List<EstadoReto> retos = await App.EstadoRetoService.ObtenerList();
            if (retos != null)
            {
                App.DataBase.EstadoReto.GuardarList(retos);
            }

            //lblTexto.Text = "Cargando estaciones del año..";
            UserDialogs.Instance.ShowLoading("Cargando estaciones del año..");
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                App.DataBase.Estacion.GuardarList(estaciones);
            }

            //lblTexto.Text = "Cargando recetas..";
            UserDialogs.Instance.ShowLoading("Cargando recetas..");
            List<Receta> recetas = await App.RecetaService.ObtenerList();
            if (recetas != null)
            {
                App.DataBase.Receta.GuardarList(recetas);
                
            }

            //lblTexto.Text = "Cargando hitorial de recetas..";
            UserDialogs.Instance.ShowLoading("Cargando hitorial de recetas..");
            List<HistorialReceta> historialReceta = await App.HistorialRecetaService.ObtenerList(usuario);
            if (historialReceta != null)
            {                
                App.DataBase.HistorialReceta.GuardarList(historialReceta);
            }

            //lblTexto.Text = "Cargando perfil..";
            UserDialogs.Instance.ShowLoading("Cargando perfil..");
            Perfil perfil = await App.PerfilService.Obtener(usuario);
            if (perfil != null)
            {
                App.DataBase.Perfil.Guardar(perfil);
                perfil.InsertarBD();

                usuario._Perfil = perfil ?? null;


            }

            UserDialogs.Instance.HideLoading();
            await Navigation.PushAsync(new MasterPage(usuario), true);
            Navigation.RemovePage(this);

            


            
        }

        public async void ActualizarRecetario(Usuario usuario)
        {
          
            
            UserDialogs.Instance.ShowLoading("Descargando momentos del día..");
            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                UserDialogs.Instance.ShowLoading("Borrando momentos del día..");
                App.DataBase.MomentoDia.BorrarTodo();
                UserDialogs.Instance.ShowLoading("Guardando momentos del día..");
                App.DataBase.MomentoDia.GuardarList(momentos);
            }

            UserDialogs.Instance.ShowLoading("Descargando estaciones del año..");
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                UserDialogs.Instance.ShowLoading("Borrando estaciones del año..");
                App.DataBase.Estacion.BorrarTodo();
                UserDialogs.Instance.ShowLoading("Guardando estaciones del año..");
                App.DataBase.Estacion.GuardarList(estaciones);
            }

            UserDialogs.Instance.ShowLoading("Descargando recetas..");
            List<Receta> recetas = await App.RecetaService.ObtenerList();
            if (recetas != null)
            {
                UserDialogs.Instance.ShowLoading("Borrando recetas..");
                App.DataBase.Receta.BorrarTodo();
                UserDialogs.Instance.ShowLoading("Guardando recetas..");
                App.DataBase.Receta.GuardarList(recetas);

            }

            UserDialogs.Instance.HideLoading();
            await Navigation.PushAsync(new MasterPage(usuario), true);
            Navigation.RemovePage(this);

        }


    }
}