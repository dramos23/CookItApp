using CookItApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public async Task<int> Alta(Receta obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                client.Timeout = TimeSpan.FromMinutes(15);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
                string json = JsonConvert.SerializeObject(obj);

                //JObject receta = JObject.Parse(json);
                //receta.Property("_IdReceta").Remove();

                //string remplazar = '\"' + "_IdReceta\":0,";
                //json = json.Replace(remplazar, string.Empty);
                



               // json = receta.ToString();



                using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (HttpResponseMessage response = await client                        
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        try
                        {
                            string JsonResult = response.Content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<int>(JsonResult);
                        }
                        catch
                        {
                            return -1;
                        }
                    }
                }
            }

        }

        public async Task<List<Receta>> ObtenerList(Usuario obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._Email;

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
