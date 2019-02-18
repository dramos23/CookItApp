using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CookItApp.Models;
using Xamarin.Forms;

namespace CookItApp.ViewModels
{
    public class FBLoginVM
    {
        private Token token { get; set; }

        public FBLoginVM() { }

        internal async Task<Usuario> Ingresar(UsuarioFacebook usuarioFacebook)
        {
            Usuario usuario = await Crear_Registrar_Usuario(usuarioFacebook);

            if (usuario != null)
            {

                bool creado = await Crear_Perfil_Usuario(usuarioFacebook);

                if (creado)
                {                    
                    return usuario;
                }

            }

            return null;
        }

        private async Task<bool> Crear_Perfil_Usuario(UsuarioFacebook usuarioFacebook)
        {
            Perfil perfil = new Perfil()
            {

                _Email = usuarioFacebook.id,
                _Apellido = usuarioFacebook.last_name,
                _Nombre = usuarioFacebook.first_name,
                _NombreUsuario = usuarioFacebook.first_name + " " + usuarioFacebook.last_name,
                _Foto = GenerarBitmap(usuarioFacebook.picture.data.url)

            };

            if (perfil != null)
            {
                bool creado = await App.PerfilService.AltaFB(perfil, token);

                if (creado) {
                   
                    return true;

                }

            }

            return false;
        }
        
        private async Task<Usuario> Crear_Registrar_Usuario(UsuarioFacebook usuarioFacebook)
        {
            usuarioFacebook.ExisteEmail();

            Usuario usuario = new Usuario()
            {

                _Email = usuarioFacebook.id,                
                _DeviceId = null,
                _TipoCuenta = Usuario.TipoCuenta.Facebook,
                _TipoUsuario = Usuario.TipoUsuario.Cliente,
                _Password = null,
                _UltimoIngreso = DateTime.Now

            };

            if (usuario != null)
            {
                Token token = await App.RestService.RegistrarFB(usuario);

                if (token._AccessToken != null)
                {
                    this.token = token;
                    return usuario;
                }
            }

            return null;


        }

        private byte[] GenerarBitmap(string url)
        {
            WebClient webClient = new WebClient();

            byte[] imageBytes = webClient.DownloadData(url);

            return imageBytes;

        }
    }
}
