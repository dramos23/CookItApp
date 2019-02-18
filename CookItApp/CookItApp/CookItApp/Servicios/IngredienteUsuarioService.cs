using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class IngredienteUsuarioService
    {
        public HttpClient client;
        private string Web { get; set; }

        public IngredienteUsuarioService()
        {
            Web = "http://cookitrestapi.azurewebsites.net/api/IngredientesUsuario/";

        }


        public async Task<bool> Alta(IngredienteUsuario obj)
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
                        return response.IsSuccessStatusCode;
                    }
                }
            }

        }

        public async Task<bool> Modificar(IngredienteUsuario obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web;

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

        public async Task<bool> Eliminar(IngredienteUsuario obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + obj._Email + "," + obj._IdIngrediente;

            using (HttpClient client = new HttpClient())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
               
                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {
                    return response.IsSuccessStatusCode;                        
                }

            }

        }


        private IngredienteUsuario Deseralizar(string jsonResult)
        {

            IngredienteUsuario p = JsonConvert.DeserializeObject<IngredienteUsuario>(jsonResult);
            return p;

        }


    }
}

