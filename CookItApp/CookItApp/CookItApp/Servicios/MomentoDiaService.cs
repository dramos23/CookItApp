using CookItApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CookItApp.Data
{
    public class MomentoDiaService
    {

        public HttpClient client;
        private string Web { get; set; }

        public MomentoDiaService()
        {
            Web = "https://cookitprowebapi.azurewebsites.net/api/MomentoDia/";

        }

        public async Task<List<MomentoDia>> ObtenerList()
        {
            Token token = App.TokenDatabase.Obtener();
            string Url = Web;

            HttpClient client = new HttpClient();            
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token._AccessToken);
                using (HttpResponseMessage response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(true))
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
        }

        private List<MomentoDia> DeseralizarList(string jsonResult)
        {

            List<MomentoDia> p = JsonConvert.DeserializeObject<List<MomentoDia>>(jsonResult);
            return p;

        }
    }
}
