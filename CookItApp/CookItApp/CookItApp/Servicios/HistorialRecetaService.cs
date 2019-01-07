using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class HistorialRecetaService
    {

        public HttpClient client;
        private string Web { get; set; }

        public HistorialRecetaService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/HistorialReceta/";
        }


        public async Task<HistorialReceta> Alta(HistorialReceta obj)
        {
            Token token = App.TokenDatabase.Obtener();
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
                        try
                        {
                            HistorialReceta ContentResp = Deseralizar(JsonResult);
                            return ContentResp;
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }

        }

        public async Task<List<HistorialReceta>> ObtenerList(HistorialReceta obj)
        {
            Token token = App.TokenDatabase.Obtener();
            string Url = Web + obj._Email;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    List<HistorialReceta> ContentResp = DeseralizarList(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<HistorialReceta> Obtener(HistorialReceta obj)
        {
            Token token = App.TokenDatabase.Obtener();
            string Url = Web + obj._Email + "," + obj._IdHistorialReceta;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    HistorialReceta ContentResp = Deseralizar(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        private HistorialReceta Deseralizar(string jsonResult)
        {

            HistorialReceta p = JsonConvert.DeserializeObject<HistorialReceta>(jsonResult);
            return p;

        }

        private List<HistorialReceta> DeseralizarList(string jsonResult)
        {

            List<HistorialReceta> p = JsonConvert.DeserializeObject<List<HistorialReceta>>(jsonResult);
            return p;

        }


        
    }
}
