using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class RecetaService
    {
        public HttpClient client;
        private string Web { get; set; }

        public RecetaService()
        {
            Web = "http://cookitrestapi.azurewebsites.net/api/Recetas/";
        }

        public async Task<List<Receta>> ObtenerList()
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
                        List<Receta> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        public async Task<Receta> Obtener(Receta obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._IdReceta;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    Receta ContentResp = Deseralizar(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<Receta> DeseralizarList(string jsonResult)
        {

            List<Receta> p = JsonConvert.DeserializeObject<List<Receta>>(jsonResult);
            return p;

        }

        private Receta Deseralizar(string jsonResult)
        {
            try
            {
                Receta p = JsonConvert.DeserializeObject<Receta>(jsonResult);
                return p;
            }
            catch(Exception e)
            {
                var msj = e.Message;
                return null;
            }

        }

    }
}
