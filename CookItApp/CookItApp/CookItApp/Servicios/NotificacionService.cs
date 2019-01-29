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

    }
}
