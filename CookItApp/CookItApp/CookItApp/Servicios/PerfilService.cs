﻿using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class PerfilService
    {
        public HttpClient client;
        private string Web { get; set; }

        public PerfilService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Perfiles/";

        }


        public async Task<Perfil> Alta(Perfil obj)
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
                            Perfil ContentResp = Deseralizar(JsonResult);
                            return ContentResp;
                        }
                        catch(Exception)
                        {
                            return null;
                        }
                    }
                }
            }

        }


        public async Task<Perfil> Obtener(Usuario obj)
        {
            Token token = App.TokenDatabase.Obtener();
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
                        Perfil ContentResp = Deseralizar(JsonResult);
                        return ContentResp;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                
            }

        }    

        public async Task<Perfil> Modificar(Perfil obj)
        {
            Token token = App.TokenDatabase.Obtener();
            string Url = Web + obj._Email;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
            string json = JsonConvert.SerializeObject(obj);
            using (StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
            {
                using (HttpResponseMessage response = await client.PutAsync(Url, stringContent))
                {
                    string JsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        Perfil ContentResp = Deseralizar(JsonResult);
                        return ContentResp;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        private Perfil Deseralizar(string jsonResult)
        {

            Perfil p = JsonConvert.DeserializeObject<Perfil>(jsonResult);
            return p;

        }

    }
}
