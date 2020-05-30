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
