using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class SupermercadoService
    {
        public HttpClient client;
        private string Web { get; set; }

        public SupermercadoService()
        {
            Web = "http://cookitrestapi.azurewebsites.net/api/Supermercados/";

        }

        public async Task<Supermercado> Obtener(int id)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + id;

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
                        Supermercado ContentResp = Deseralizar(JsonResult);
                        return ContentResp;
                    }
                    catch
                    {
                        return null;
                    }
                }

            }

        }

        public async Task<List<Supermercado>> ObtenerList()
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
                    string JsonResult = await response.Content.ReadAsStringAsync();
                    try
                    {
                        List<Supermercado> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        private Supermercado Deseralizar(string jsonResult)
        {

            Supermercado p = JsonConvert.DeserializeObject<Supermercado>(jsonResult);
            return p;

        }

        private List<Supermercado> DeseralizarList(string jsonResult)
        {

            List<Supermercado> p = JsonConvert.DeserializeObject<List<Supermercado>>(jsonResult);
            return p;

        }
    }
}
