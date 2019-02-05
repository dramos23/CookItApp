using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Servicios
{
    public class NotificacionService
    {
        public HttpClient client;
        private string Web { get; set; }

        public NotificacionService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Notificacion/";

        }

        public async Task<bool> Modificar(Notificacion obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "CambioEstado";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
            string json = JsonConvert.SerializeObject(obj);
            using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response = await client.PutAsync(Url, stringContent))
                {
                    string JsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResp = response.IsSuccessStatusCode;
                        return ContentResp;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<Notificacion> Obtener(Usuario obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerNotificacion/" + obj._Email;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    Notificacion ContentResp = Deseralizar(JsonResult);
                    return ContentResp;
                }
                catch
                {
                    return null;
                }
            }

        }

        public async Task<List<Notificacion>> ObtenerList(Perfil obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerNotificaciones/" + obj._Email;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    string JsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        List<Notificacion> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        private Notificacion Deseralizar(string jsonResult)
        {

            Notificacion p = JsonConvert.DeserializeObject<Notificacion>(jsonResult);
            return p;

        }

        private List<Notificacion> DeseralizarList(string jsonResult)
        {

            List<Notificacion> p = JsonConvert.DeserializeObject<List<Notificacion>>(jsonResult);
            return p;

        }

    }
}
