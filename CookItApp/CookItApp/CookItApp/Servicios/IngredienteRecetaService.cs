using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class IngredienteRecetaService
    {
        //public HttpClient client;

        //public IngredienteRecetaService()
        //{

        //    client = new HttpClient();
        //    client.BaseAddress = new Uri("http://cookitrestapi.azurewebsites.net/");

        //}

        //public async Task<List<IngredienteReceta>> ObtenerIngredienteRecetaPorId(int id)
        //{
        //    var Url = "/api/Recetas/";
        //    Url += id + "/IngredienteRecetas/ObtenerIngRecetaID";
        //    List<IngredienteReceta> response = await GetResponse<List<IngredienteReceta>>(Url);
        //    return response;
        //}

        //public async Task<List<IngredienteReceta>> GetResponse<T>(string weburl) where T : class
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
        //                List<IngredienteReceta> ContentResp = JsonConvert.DeserializeObject<List<IngredienteReceta>>(JsonResult);
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

        public HttpClient client;
        private string Web { get; set; }

        public IngredienteRecetaService()
        {
            Web = "http://cookitrestapi.azurewebsites.net/api/Recetas/";

        }

        public async Task<List<IngredienteReceta>> Obtener(Receta receta)
        {
            Token token = App.DataBase.Token.Obtener();
            string Url = Web + receta._IdReceta + "/IngredienteRecetas";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);

            using (HttpResponseMessage response = await client.GetAsync(Url))
            {
                string JsonResult = response.Content.ReadAsStringAsync().Result;
                try
                {
                    List<IngredienteReceta> ContentResp = DeseralizarList(JsonResult);
                    return ContentResp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private List<IngredienteReceta> DeseralizarList(string jsonResult)
        {

            List<IngredienteReceta> p = JsonConvert.DeserializeObject<List<IngredienteReceta>>(jsonResult);
            return p;

        }
    }
}
