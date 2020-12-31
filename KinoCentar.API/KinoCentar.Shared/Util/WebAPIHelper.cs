using KinoCentar.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KinoCentar.Shared.Util
{
    public class WebAPIHelper
    {
        private HttpClient client { get; set; }
        private string route { get; set; }

        public WebAPIHelper(string uri, string route, string username = null, string password = null)
        {
            this.route = route;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
                client = new HttpClient()
                {
                    DefaultRequestHeaders = { Authorization = authValue }
                };
            }
            else
            {
                client = new HttpClient();
            }
            
            client.BaseAddress = new Uri(uri);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public WebAPIHelper(string uri, string route, KorisnikModel korisnik) : this(uri, route, korisnik?.KorisnickoIme, korisnik?.Lozinka)
        {
            
        }

        #region GET

        public HttpResponseMessage GetResponse(string parameter = "")
        {
            return GetResponseAsync(parameter).Result;
        }

        public Task<HttpResponseMessage> GetResponseAsync(string parameter = "")
        {
            return client.GetAsync(route + "/" + parameter);
        }

        public HttpResponseMessage GetActionResponse(string action, string parameter = "")
        {
            return GetActionResponseAsync(action, parameter).Result;
        }

        public Task<HttpResponseMessage> GetActionResponseAsync(string action, string parameter = "")
        {
            return client.GetAsync(route + "/" + action + "/" + parameter);
        }

        public HttpResponseMessage GetActionResponse(string action, string p1 = "", string p2 = "", string p3 = "")
        {
            return GetActionResponseAsync(action, p1, p2, p3).Result;
        }

        public Task<HttpResponseMessage> GetActionResponseAsync(string action, string p1 = "", string p2 = "", string p3 = "")
        {
            if (!string.IsNullOrEmpty(p3))
            {
                return client.GetAsync(route + "/" + action + "/" + p1 + "/" + p2 + "/" + p3);
            }
            else
            {
                return client.GetAsync(route + "/" + action + "/" + p1 + "/" + p2);
            }
        }

        public HttpResponseMessage GetActionSearchResponse(string action, string p1 = "*", string p2 = "*", string p3 = "")
        {
            return GetActionSearchResponseAsync(action, p1, p2, p3).Result;
        }

        public Task<HttpResponseMessage> GetActionSearchResponseAsync(string action, string p1 = "*", string p2 = "*", string p3 = "")
        {
            if (string.IsNullOrEmpty(p1))
            {
                p1 = "*";
            }
            if (string.IsNullOrEmpty(p2))
            {
                p2 = "*";
            }

            return client.GetAsync(route + "/" + action + "/" + p1 + "/" + p2 + "/" + p3);
        }

        #endregion

        #region POST

        public HttpResponseMessage PostResponse(Object newObject)
        {
            return PostResponseAsync(newObject).Result;
        }

        public Task<HttpResponseMessage> PostResponseAsync(Object newObject)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(newObject), Encoding.UTF8, "application/json");
            return client.PostAsync(route, jsonObject);
        }

        public HttpResponseMessage PostActionResponse(string action, Object newObject)
        {
            return PostActionResponseAsync(action, newObject).Result;
        }

        public Task<HttpResponseMessage> PostActionResponseAsync(string action, Object newObject)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(newObject), Encoding.UTF8, "application/json");
            return client.PostAsync(route + "/" + action, jsonObject);
        }

        #endregion

        #region PUT

        public HttpResponseMessage PutResponse(int id, Object existingObject)
        {
            return PutResponseAsync(id, existingObject).Result;
        }

        public Task<HttpResponseMessage> PutResponseAsync(int id, Object existingObject)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(existingObject), Encoding.UTF8, "application/json");
            return client.PutAsync(route + "/" + id, jsonObject);
        }

        public HttpResponseMessage PutActionResponse(string action, int id)
        {
            return PutActionResponseAsync(action, id).Result;
        }

        public Task<HttpResponseMessage> PutActionResponseAsync(string action, int id)
        {
            return client.PutAsync(route + "/" + action + "/" + id, null);
        }

        public HttpResponseMessage PutActionResponse(string action, int id1, int id2)
        {
            return PutActionResponseAsync(action, id1, id2).Result;
        }

        public Task<HttpResponseMessage> PutActionResponseAsync(string action, int id1, int id2)
        {
            return client.PutAsync(route + "/" + action + "/" + id1 + "/" + id2, null);
        }

        public HttpResponseMessage PutActionResponse(string action, Object existingObject, int id)
        {
            return PutActionResponseAsync(action, existingObject, id).Result;
        }

        public Task<HttpResponseMessage> PutActionResponseAsync(string action, Object existingObject, int id)
        {
            var jsonObject = new StringContent(JsonConvert.SerializeObject(existingObject), Encoding.UTF8, "application/json");
            return client.PutAsync(route + "/" + action + "/" + id, jsonObject);
        }

        #endregion

        #region DELETE

        public HttpResponseMessage DeleteResponse(int id)
        {
            return DeleteResponseAsync(id).Result;
        }

        public Task<HttpResponseMessage> DeleteResponseAsync(int id)
        {
            return client.DeleteAsync(route + "/" + id);
        }

        #endregion
    }
}
