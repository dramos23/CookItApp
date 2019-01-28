using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class UsuarioService
    {

        public HttpClient client;
        private string Web { get; set; }

        public UsuarioService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Cuenta/";
        }


        public async Task<Token> Registrar(Usuario obj)
        {
            
            string Url = Web + "Registrar";

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {
                
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
                            Token ContentResp = Deseralizar(JsonResult);
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

        public async Task<Token> Login(Usuario obj)
        {            
            string Url = Web + "Ingresar";

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {                
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
                            Token ContentResp = Deseralizar(JsonResult);
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

        private Token Deseralizar(string jsonResult)
        {

            Token p = JsonConvert.DeserializeObject<Token>(jsonResult);
            return p;

        }

    }
}
