using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Servicios
{
    public class EstadoRetoService
    {
        public HttpClient client;
        private string Web { get; set; }

        public EstadoRetoService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/EstadoReto/";

        }

        public async Task<List<EstadoReto>> ObtenerList()
        {
            Token token = App.DataBase.Token.Obtener();
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
                        List<EstadoReto> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        private List<EstadoReto> DeseralizarList(string jsonResult)
        {

            List<EstadoReto> p = JsonConvert.DeserializeObject<List<EstadoReto>>(jsonResult);
            return p;

        }
    }
}
