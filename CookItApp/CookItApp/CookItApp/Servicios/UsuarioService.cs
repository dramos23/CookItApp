using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Web = "http://cookitrestapi.azurewebsites.net/api/Cuenta/";
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

        public async Task<Token> RegistrarFB(Usuario obj)
        {

            string Url = Web + "RegistrarFB";

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

        public async Task<bool> UpdateUUID(Usuario obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "UpdateUUID";

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Url))
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
                        return response.IsSuccessStatusCode;
                    }
                }
            }

        }

        public async Task<Guid?> ObtenerUUID(string idUsuario)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerUUID/" + idUsuario;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);


                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<Guid?>(response.Content.ReadAsStringAsync().Result);
                }

            }

        }


        public async Task<bool> RestablecerContraseña(string idUsuario)
        {
            string Url = Web + "Restablecer/" + idUsuario;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url))
            {                                   

                    using (HttpResponseMessage response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        return response.IsSuccessStatusCode;
                    }
                
            }

        }

        public async Task<bool> CambiarCont(Dictionary<string, string> obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "CambiarCont";

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Url))
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
                        return response.IsSuccessStatusCode;
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
