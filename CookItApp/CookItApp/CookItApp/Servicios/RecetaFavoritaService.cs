using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class RecetaFavoritaService
    {

        public HttpClient client;
        private string Web { get; set; }

        public RecetaFavoritaService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/RecetaFavorita/";
        }


        public async Task<RecetaFavorita> Alta(RecetaFavorita obj)
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
                        try
                        {
                            RecetaFavorita ContentResp = Deseralizar(JsonResult);
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

        public async Task<List<RecetaFavorita>> ObtenerList(Perfil obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._Email;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    List<RecetaFavorita> ContentResp = DeseralizarList(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> Eliminar(RecetaFavorita obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._Email + "," + obj._IdReceta;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
            
            using (HttpResponseMessage response = await client.DeleteAsync(Url))
            {
                
                return response.IsSuccessStatusCode;
                
            }
            
        }

        private RecetaFavorita Deseralizar(string jsonResult)
        {

            RecetaFavorita p = JsonConvert.DeserializeObject<RecetaFavorita>(jsonResult);
            return p;

        }

        private List<RecetaFavorita> DeseralizarList(string jsonResult)
        {

            List<RecetaFavorita> p = JsonConvert.DeserializeObject<List<RecetaFavorita>>(jsonResult);
            return p;

        }






        //public HttpClient client;

        //public RecetaFavoritaService()
        //{

        //    client = new HttpClient
        //    {
        //        BaseAddress = new Uri("https://cookitprowebapi.azurewebsites.net/")
        //    };

        //}

        //public async Task<RecetaFavorita> AltaFavorito(RecetaFavorita recetaFavorita)
        //{
        //    var json = JsonConvert.SerializeObject(recetaFavorita);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var Url = "/api/RecetaFavorita";
        //    RecetaFavorita response = await PostResponse<RecetaFavorita>(Url, content);
        //    return response;

        //}

        //public async Task<List<RecetaFavorita>> ObtenerRecetasFavoritas(string EmailUsuario)
        //{

        //    var Url = "/api/RecetaFavorita/" + EmailUsuario;
        //    List<RecetaFavorita> response = await GetResponse<List<RecetaFavorita>>(Url);
        //    return response;

        //}

        //public async Task<ComentarioReceta> BorrarRecetasFavoritas(string EmailUsuario, int IdReceta)
        //{
        //    var Url = "/api/RecetaFavorita/" + EmailUsuario + "," + IdReceta;
        //    ComentarioReceta response = await DelResponse<ComentarioReceta>(Url);
        //    return response;
        //}

        //public async Task<T> DelResponse<T>(string weburl) where T : class
        //{
        //    var response = await client.DeleteAsync(weburl);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return responseObject;

        //}

        //public async Task<T> PostResponse<T>(string weburl, StringContent content) where T : class
        //{
        //    var response = new HttpResponseMessage();
        //    try
        //    {
        //        response = await client.PostAsync(weburl, content);
        //        var jsonResult = response.Content.ReadAsStringAsync().Result;
        //        var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //        return responseObject;
        //    }
        //    catch(Exception) {

        //        return null;
        //    }



        //}

        //public async Task<List<RecetaFavorita>> GetResponse<T>(string weburl) where T : class
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
        //                List<RecetaFavorita> ContentResp = JsonConvert.DeserializeObject<List<RecetaFavorita>>(JsonResult);
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
