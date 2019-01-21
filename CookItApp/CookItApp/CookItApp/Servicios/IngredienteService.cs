﻿using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class IngredienteService
    {

        public HttpClient client;
        private string Web { get; set; }

        public IngredienteService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/Ingredientes/";

        }

        public async Task<List<MomentoDia>> ObtenerList()
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    List<MomentoDia> ContentResp = DeseralizarList(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<MomentoDia> DeseralizarList(string jsonResult)
        {

            List<MomentoDia> p = JsonConvert.DeserializeObject<List<MomentoDia>>(jsonResult);
            return p;

        }

    }
}