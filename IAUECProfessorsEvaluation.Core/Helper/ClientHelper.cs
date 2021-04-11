using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IAUECProfessorsEvaluation.Model.Models;
using Newtonsoft.Json;

namespace IAUECProfessorsEvaluation.Core.Helper
{
    public class ClientHelper
    {
        public static async Task<HttpClient> GetHttpClientAsync()
        {
            using (var client = new HttpClient())
            {
                Task<HttpResponseMessage> response =
                    client.PostAsync(StaticValue.SyncApiDomain+ StaticValue.SyncToken, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", StaticValue.SyncUserName),
                        new KeyValuePair<string, string>("password", StaticValue.SyncPassword),
                        new KeyValuePair<string, string>("grant_type", "password")
                    }));

                if (!response.Result.IsSuccessStatusCode) return null;

                var resualt = await response.Result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<TokenModel>(resualt);
                var resualtClient = new HttpClient { Timeout = Timeout.InfiniteTimeSpan };
                resualtClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {model.access_token}");
                return resualtClient;

            }

        }

        public static List<T> GetValue<T>(string relativeAddress)
        {

            var httpClient = GetHttpClientAsync().Result;

            if (httpClient == null)
                throw new UnauthorizedAccessException("امکان دسترسی به این منبع برای شما مهیا نمی باشد");

            HttpResponseMessage response = httpClient.GetAsync(StaticValue.SyncApiDomain + relativeAddress).Result;

            if (!response.IsSuccessStatusCode) return null;

            var resualt = JsonConvert.DeserializeObject<List<T>>(response.Content.ReadAsStringAsync().Result);
            return resualt;
        }

        public static string GetStringValue(string relativeAddress)
            {

            var httpClient = GetHttpClientAsync().Result;

            if (httpClient == null)
                throw new UnauthorizedAccessException("امکان دسترسی به این منبع برای شما مهیا نمی باشد");

            HttpResponseMessage response = httpClient.GetAsync(StaticValue.SyncApiDomain + relativeAddress).Result;

            if (!response.IsSuccessStatusCode) return null;
            
            return response.Content.ReadAsStringAsync().Result;
        }

        public static T GetScalarValue<T>(string relativeAddress)
        {

            var httpClient = GetHttpClientAsync().Result;

            if (httpClient == null)
                throw new UnauthorizedAccessException("امکان دسترسی به این منبع برای شما مهیا نمی باشد");

            HttpResponseMessage response = httpClient.GetAsync(StaticValue.SyncApiDomain + relativeAddress).Result;

            var resualt = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            return resualt;
        }

    }

}
