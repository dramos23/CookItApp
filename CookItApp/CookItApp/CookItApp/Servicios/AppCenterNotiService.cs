using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class AppCenterNotiService
    {

        private string Web { get; set; }
        private string Token { get; set; }

        public AppCenterNotiService()
        {
            Token = "8446c17b1645b71ed877b4c10fc25efe5ca312d5";
            Web = "https://appcenter.ms/api/v0.1/apps/daniel.r.23-gmail.com/CookIt/push/notifications";

        }


        public async Task<bool> Enviar(NotificacionAppCenter obj)
        {
            
            string Url = Web;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                client.DefaultRequestHeaders.Add("X-API-Token", Token); ;
                string json = JsonConvert.SerializeObject(obj);
                using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (HttpResponseMessage response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        return response.IsSuccessStatusCode;
                        
                    }
                }
            }

        }

    }
}
