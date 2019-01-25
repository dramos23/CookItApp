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
            Web = "https://cookitprowebapi.azurewebsites.net/api/Recetas/";
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

        //public HttpClient client;

        //public RecetaService()
        //{

        //    client = new HttpClient
        //    {
        //        BaseAddress = new Uri("https://cookitprowebapi.azurewebsites.net/")
        //    };

        //}

        //public async Task<List<Receta>> ObtenerRecetas()
        //{
        //    var Url = "/api/Recetas/";
        //    List<Receta> response = await GetResponse<List<Receta>>(Url);
        //    return response;

        //}

        //public async Task<Receta> ObtenerRecetaPorId(int id)
        //{
        //    var Url = "/api/Recetas/";
        //    Url += id + ""; 
        //    List<Receta> response = await GetResponse<List<Receta>>(Url);
        //    return response[0];
        //}

        //public async Task<List<Receta>> ObtenerRecetasFiltro(Dictionary<string, string> obj)
        //{
        //    var json = JsonConvert.SerializeObject(obj);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var Url = "/api/Recetas/RecetasFiltro";
        //    List<Receta> response = await PostResponse<List<Receta>>(Url, content);
        //    return response;

        //}

        //public async Task<T> PostResponse<T>(string weburl, StringContent content) where T : class
        //{
        //    var response = await client.PostAsync(weburl, content);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return responseObject;

        //}

        //public async Task<List<Receta>> GetResponse<T>(string weburl) where T : class
        //{
        //    var token = App.DataBase.TokenDataBase.Obtener();
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token._AccessToken);
        //    try
        //    {
        //        var Response = await client.GetAsync(weburl);
        //        if (Response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var JsonResult = Response.Content.ReadAsStringAsync().Result;

        //            try
        //            {
        //                List<Receta> ContentResp = JsonConvert.DeserializeObject<List<Receta>>(JsonResult);
        //                return ContentResp;
        //            }
        //            catch
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return null;

        //}

    }
}
