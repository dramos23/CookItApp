﻿using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class RetoService
    {
        public HttpClient client;
        private string Web { get; set; }

        public RetoService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Retos/";

        }


        public async Task<int> Alta(Reto obj)
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
                        return JsonConvert.DeserializeObject<int>(JsonResult);                                             
                    }
                }
            }

        }


        public async Task<List<Reto>> ObtenerList(Perfil obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerRetos/" + obj._Email;

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
                        List<Reto> ContentResp = DeseralizarList(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        public async Task<Reto> Obtener(int idReto)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + "ObtenerReto/" + idReto;

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
                        Reto ContentResp = Deseralizar(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            }

        }

        public async Task<bool> Modificar(Reto obj)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
            string json = JsonConvert.SerializeObject(obj);
            using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response = await client.PutAsync(Url, stringContent))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        private Reto Deseralizar(string jsonResult)
        {

            Reto p = JsonConvert.DeserializeObject<Reto>(jsonResult);
            return p;

        }

        private List<Reto> DeseralizarList(string jsonResult)
        {

            List<Reto> p = JsonConvert.DeserializeObject<List<Reto>>(jsonResult);
            return p;

        }
    }
}