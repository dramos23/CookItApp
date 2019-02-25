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
    public class ComentarioRecetaService
    {
        public HttpClient client;
        private string Web { get; set; }


        public ComentarioRecetaService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Recetas/";

        }


        public async Task<Object> Alta(ComentarioReceta obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._IdReceta + "/Comentario"; 

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
                        try { 
                            Object ContentResp = Deseralizar(JsonResult);
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

        public async Task<Object> Obtener(ComentarioReceta obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._IdReceta + "/Comentario";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try { 
                    Object ContentResp = DeseralizarList(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Object> Modificar(ComentarioReceta obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._IdReceta + "/Comentario/" + obj._IdComentario;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
            string json = JsonConvert.SerializeObject(obj);
            using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response = await client.PutAsync(Url, stringContent))
                {
                    string JsonResult = response.Content.ReadAsStringAsync().Result;
                    try { 
                        Object ContentResp = Deseralizar(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        private Object Deseralizar(string jsonResult)
        {
            
            ComentarioReceta p = JsonConvert.DeserializeObject<ComentarioReceta>(jsonResult);
            return p;

        }

        private Object DeseralizarList(string jsonResult)
        {

            List<ComentarioReceta> p = JsonConvert.DeserializeObject<List<ComentarioReceta>>(jsonResult);
            return p;

        }

        //public HttpClient client;

        //public ComentarioRecetaService()
        //{

        //    client = new HttpClient();
        //    client.BaseAddress = new Uri("https://cookitprowebapi.azurewebsites.net/");

        //}

        //public async Task<ComentarioReceta> AltaComentario(ComentarioReceta comentarioReceta)
        //{
        //    var json = JsonConvert.SerializeObject(comentarioReceta);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var Url = "/api/Recetas/" + comentarioReceta._IdReceta + "/Comentario";
        //    ComentarioReceta response = await PostResponse<ComentarioReceta>(Url, content);
        //    return response;
        //}

        //public async Task<ComentarioReceta> ModificarComentario(ComentarioReceta comentarioReceta)
        //{
        //    var json = JsonConvert.SerializeObject(comentarioReceta);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var Url = "/api/Recetas/" + comentarioReceta._IdReceta + "/Comentario/" + comentarioReceta._IdComentario;
        //    ComentarioReceta response = await PutResponse<ComentarioReceta>(Url, content);
        //    return response;

        //}

        //public async Task<ComentarioReceta> BorrarComentario(int IdReceta, int IdComentario)
        //{            
        //    var Url = "/api/Recetas/" + IdReceta + "/Comentario/" + IdComentario;
        //    ComentarioReceta response = await DelResponse<ComentarioReceta>(Url);
        //    return response;
        //}


        //public async Task<T> PostResponse<T>(string weburl, StringContent content) where T : class
        //{
        //    var response = await client.PostAsync(weburl, content);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return responseObject;
        //}

        //public async Task<T> PutResponse<T>(string weburl, StringContent content) where T : class
        //{
        //    var response = await client.PutAsync(weburl, content);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return responseObject;
        //}

        //public async Task<T> DelResponse<T>(string weburl) where T : class
        //{
        //    var response = await client.DeleteAsync(weburl);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return responseObject;
        //}

    }
}
