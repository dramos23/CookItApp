using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class EstacionService
    {
        public HttpClient client;
        private string Web { get; set; }

        public EstacionService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Estacion/";

        }

        public async Task<List<Estacion>> ObtenerList()
        {
            Token token = App.TokenDatabase.Obtener();
            string Url = Web;

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
                        List<Estacion> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        private List<Estacion> DeseralizarList(string jsonResult)
        {

            List<Estacion> p = JsonConvert.DeserializeObject<List<Estacion>>(jsonResult);
            return p;

        }
    }
}
