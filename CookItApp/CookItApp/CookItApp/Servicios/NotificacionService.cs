using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class NotificacionService
    {
        public HttpClient client;
        private string Web { get; set; }

        public NotificacionService()
        {
            Web = "http://cookitrestapi.azurewebsites.net/api/Notificacion/";

        }

        public async Task<int> Alta(Notificacion obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
                string json = JsonConvert.SerializeObject(obj);
                using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (HttpResponseMessage response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        string JsonResult = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<int>(JsonResult);
                    }
                }
            }

        }

        public async Task<bool> Modificar(Notificacion obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "CambioEstado/" + obj._NotificacionId;

            HttpClient client = new HttpClient();
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    return response.IsSuccessStatusCode;
                }
                
            }

        }


        public async Task<Notificacion> Obtener(int id)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerNotificacion/" + id;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    string JsonResult = await response.Content.ReadAsStringAsync();
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
                    string JsonResult = await response.Content.ReadAsStringAsync();
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
