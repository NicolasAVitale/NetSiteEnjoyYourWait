using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using EnjoyYourWaitNetSite.Helper;
using EnjoyYourWaitNetSite.Exceptions;
using java.net;
using Newtonsoft.Json.Linq;
using EnjoyYourWaitNetSite.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EnjoyYourWaitNetSite.DataAccess
{
    public class DataAccessEYW
    {
        private readonly string appSettings = ConfigurationManager.AppSettings.Get("EYWService");

        private readonly HttpClient httpClient = new HttpClient();

        public DataAccessEYW()
        {
        }

        public async Task<string> GetHolaMundo()
        {
            var response = await BuildRequest(HttpMethod.Get, "productos", false);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(JsonConvert.DeserializeObject<ErrorModel>(content).ErrorMsg);

            var jObj = JObject.Parse(content);

            return jObj["Mensaje"].ToString();
        }

        public async Task<object[]> GetAuthToken(Usuario userModel)
        {
            var response = await BuildRequest(HttpMethod.Post, "auth", false, userModel);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(JsonConvert.DeserializeObject<ErrorModel>(content).ErrorMsg);

            var jObj = JObject.Parse(content);

            return new List<object>() { jObj["token"].ToString(), JsonConvert.DeserializeObject<Usuario>(jObj["userModel"].ToString()) }.ToArray();
        }

        public async Task<List<Usuario>> GetAllRecepcionistas()
        {
            return await Request<List<Usuario>>(HttpMethod.Get, "recepcionistas", false);
        }

        public async Task<bool> CreateRecepcionista(Usuario recepcionista)
        {
            return await Request(HttpMethod.Post, "recepcionistas", false, recepcionista);
        }

        public async Task<bool> DeleteRecepcionista(int dni)
        {
            return await Request(HttpMethod.Delete, $"recepcionistas/{dni}", false);
        }

        public async Task<bool> UpdateRecepcionista(int dni, string email)
        {
            return await Request(HttpMethod.Put, $"recepcionistas/{dni}/{email}", false);
        }

        public async Task<bool> CreateProducto(Producto producto)
        {
            return await Request(HttpMethod.Post, "productos", false, producto);
        }

        public async Task<List<TipoProducto>> GetAllTiposProducto()
        {
            return await Request<List<TipoProducto>>(HttpMethod.Get, "productos", false);
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            return await Request<List<Producto>>(HttpMethod.Get, "productos", false);
        }

        public async Task<bool> DeleteProducto(int idProducto)
        {
            return await Request(HttpMethod.Delete, $"productos/{idProducto}", false);
        }

        public async Task<bool> UpdateProducto(int idProducto)
        {
            return await Request(HttpMethod.Put, $"productos/{idProducto}", false);
        }

        private async Task<bool> Request(HttpMethod method, string url, bool auth = false, object body = null)
        {
            var response = await BuildRequest(method, url, auth, body);

            return response.StatusCode == HttpStatusCode.OK;
        }

        private async Task<T> Request<T>(HttpMethod method, string url, bool auth = false, object body = null)
        {
            T result;
            var response = await BuildRequest(method, url, auth, body);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonException)
            {
                throw new DataAccessException(JsonConvert.DeserializeObject<ErrorModel>(content).ErrorMsg);
            }

            return result;
        }

        private async Task<HttpResponseMessage> BuildRequest(HttpMethod method, string url, bool auth, object body = null)
        {
            HttpResponseMessage response = null;
            try
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    //Asigno el método
                    Method = method,
                    //Asigno la url
                    RequestUri = new Uri(appSettings + url)
                };
                requestMessage.Headers.Add("accept", "application/json");
                //Valido si se envía contenido
                if (body != null)
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                //Valido si precisa autenticación
                if (auth)
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionHelper.Token);

                response = await httpClient.SendAsync(requestMessage);
            }
            catch (UnknownHostException)
            {
                throw new DataAccessException("Error al conectar con el servicio.");
            }
            finally
            {
                if (response?.StatusCode == HttpStatusCode.Unauthorized)
                    throw new TokenExpiredException();
            }
            return response;
        }

    }
}
