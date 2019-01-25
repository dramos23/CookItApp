﻿using Acr.UserDialogs;
using CookItApp.Models;
using CookItApp.Servicios;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
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
    public partial class LoginPage : ContentPage
    {
        private Usuario Usuario { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            Push.SetEnabledAsync(true);

            NavigationPage.SetHasNavigationBar(this, false);
            Init();
        }

        public void Init()
        {

            entryEmail.Completed += (s, e) => entryPass.Focus();
            entryPass.Completed += (s, e) => PrcIngresar(s, e);
            BtnRegistrar();

        }

        public async void PrcIngresar(object sender, EventArgs e)
        {
            btnIngresar.IsEnabled = false;
            if (entryEmail.Text == null)
            {
                //await DisplayAlert("Login", "Debe ingresar un correo.", "Ok");
                await UserDialogs.Instance.AlertAsync("Login", "Debe ingresar un correo.", "Ok");
                btnIngresar.IsEnabled = true;
                return;
            }
            else if (entryPass.Text == null)
            {
                await DisplayAlert("Login", "Debe ingresar una contraseña.", "Ok");
                btnIngresar.IsEnabled = true;
                return;
            }

            UserDialogs.Instance.ShowLoading("Ingresando..");

            System.Guid? uuid = await AppCenter.GetInstallIdAsync();

            if (uuid != null)
            {
                Usuario = new Usuario(entryEmail.Text, entryPass.Text, uuid, DateTime.Now);

                if (Usuario.IsValid())
                {

                    Token token = await App.RestService.Login(Usuario);

                    if (token != null)
                    {
                        if (token._AccessToken != null)
                        {
                            UserDialogs.Instance.HideLoading();
                            //await DisplayAlert("Login", "Ingreso Satisfactorio", "Ok");

                            App.DataBase.Usuario.Guardar(Usuario);
                            App.DataBase.Token.Guardar(token);

                            await Navigation.PushAsync(new CargaRecursos(Usuario, "INS"), true);
                            Navigation.RemovePage(this);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Login", "Error en el servicio.", "Ok");
                    }

                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Login", "Error al ingresar, usuario y/o contraseña incorrectos", "Ok");
                }

                btnIngresar.IsEnabled = true;
            }

            UserDialogs.Instance.HideLoading();
        }

        public void BtnRegistrar()
        {

            //Navigation.PushAsync(new RegisterPage());
            lblRegistro.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                    Navigation.PushAsync(new RegisterPage())
                )
            });

        }

        public async Task<bool> CargaDatosAplicativo(Usuario usuario, Token token)
        {


            bool retorno = false;

            App.DataBase.Usuario.Guardar(usuario);
            App.DataBase.Token.Guardar(token);

            Perfil perfil = await App.PerfilService.Obtener(usuario);
            if (perfil != null)
            {
                App.DataBase.Perfil.Guardar(perfil);
            }
            usuario._Perfil = perfil ?? null;

            List<MomentoDia> momentos = await App.MomentoDiaService.ObtenerList();
            if (momentos != null)
            {
                App.DataBase.MomentoDia.GuardarList(momentos);
            }
            List<Estacion> estaciones = await App.EstacionService.ObtenerList();
            if (estaciones != null)
            {
                App.DataBase.Estacion.GuardarList(estaciones);
            }
            if (momentos != null && estaciones != null)
            {
                List<Receta> recetas = await App.RecetaService.ObtenerList();
                if (recetas != null)
                {
                    App.DataBase.Receta.GuardarList(recetas);
                    retorno = true;
                }
            }

            return retorno;
        }

    }
}