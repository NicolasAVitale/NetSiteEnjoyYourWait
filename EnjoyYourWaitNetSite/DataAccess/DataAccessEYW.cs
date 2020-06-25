using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using EnjoyYourWaitNetSite.Exceptions;
using java.net;
using Newtonsoft.Json.Linq;
using EnjoyYourWaitNetSite.Entities;
using System.Collections.Generic;
using EnjoyYourWaitNetSite.Models;
using System.Linq;

namespace EnjoyYourWaitNetSite.DataAccess
{
    public class DataAccessEYW
    {
        private readonly string appSettings = ConfigurationManager.AppSettings.Get("EYWService");

        private readonly HttpClient httpClient = new HttpClient();

        private static string tokenMemory = string.Empty;

        private static DateTime dateToken; 

        public DataAccessEYW()
        {
        }

        public async Task<string> GetAuthToken()
        {
            string token = VerificarToken();
            if (token == "expire")
            {
                UserAuthEntity user = new UserAuthEntity();
                user.nombre = ConfigurationManager.AppSettings.Get("UserService");
                user.contrasena = ConfigurationManager.AppSettings.Get("ClaveService");
                //var response = await BuildRequest(HttpMethod.Post, "auth", false, user);

                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(appSettings + "usuarios/auth")
                };
                requestMessage.Headers.Add("accept", "application/json");
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception(JsonConvert.DeserializeObject<ErrorModel>(content).Descripcion);

                var jObj = JObject.Parse(content);

                //Guardo fecha del token
                dateToken = DateTime.Now;
                //Guardo el token
                tokenMemory = jObj["token"].ToString();

                return jObj["token"].ToString();
            }
            return token;  
        }

        public async Task<List<Producto>> GetAllProductosActivos()
        {
            List<Producto> productos = await Request<List<Producto>>(HttpMethod.Get, "productos", true);
            return productos.Where(x => x.activo == 1).ToList();
        }

        public static string VerificarToken()
        {
            if (tokenMemory == string.Empty)
            {
                return "expire";
            }
            TimeSpan diff = DateTime.Now - dateToken;
            if (diff.TotalHours < Int16.Parse(ConfigurationManager.AppSettings.Get("ExpirationTime")))
            {
                return tokenMemory;
            }
            else
            {
                return "expire";
            }
        }

        public async Task<Usuario> Login(UserLoginEntity userCredentials)
        {
            return await Request<Usuario>(HttpMethod.Post, "usuarios/login", true, userCredentials);
        }

        public async Task<bool> IngresarAFila(FilaCliente filaCliente)
        {
            return await Request(HttpMethod.Post, "filaclientes/agregarCliente", true, filaCliente);
        }

        public async Task<bool> ActualizarEstadoClientesEnRestaurante(int tiempo)
        {
            return await Request(HttpMethod.Put, $"filaclientes/{tiempo}", true);
        }

        public async Task<EsperaResponse> CalcularTiempoYPersonasEsperaCliente(int idCliente, int capacidad, int tiempo)
        {
            return await Request<EsperaResponse>(HttpMethod.Get, $"filaclientes/{idCliente}/{capacidad}/{tiempo}", true);
        }

        public async Task<EsperaResponse> CalcularTiempoYPersonasEsperaGeneral(int capacidad, int tiempo)
        {
            return await Request<EsperaResponse>(HttpMethod.Get, $"filaclientes/{capacidad}/{tiempo}", true);
        }

        public async Task<Cliente> RegistrarCliente(Cliente cliente)
        {
            return await Request<Cliente>(HttpMethod.Post, "clientes", true, cliente);
        }

        public async Task<List<Usuario>> GetAllRecepcionistas()
        {
            return await Request<List<Usuario>>(HttpMethod.Get, "usuarios?rol=2", true);
        }

        public async Task<bool> CreateRecepcionista(Usuario recepcionista)
        {
            return await Request(HttpMethod.Post, "usuarios", true, recepcionista);
        }

        public async Task<bool> DisableRecepcionista(int dni)
        {
            return await Request(HttpMethod.Put, $"recepcionistas/{dni}", true);
        }

        public async Task<bool> EnableRecepcionista(int dni)
        {
            return await Request(HttpMethod.Put, $"recepcionistas/{dni}", true);
        }

        public async Task<bool> UpdateRecepcionista(int dni, string email)
        {
            return await Request(HttpMethod.Put, $"recepcionistas/{dni}/{email}", true);
        }

        public async Task<bool> CreateProducto(Producto producto)
        {
            return await Request(HttpMethod.Post, "productos", true, producto);
        }

        public async Task<List<TipoProducto>> GetAllTiposProducto()
        {
            return await Request<List<TipoProducto>>(HttpMethod.Get, "productos", true);
        }

        public async Task<List<Producto>> GetAllProductos()
        {
            return await Request<List<Producto>>(HttpMethod.Get, "productos", true);
        }

        public async Task<bool> DisableProducto(int idProducto)
        {
            return await Request(HttpMethod.Put, $"productos/{idProducto}", true);
        }

        public async Task<bool> EnableProducto(int idProducto)
        {
            return await Request(HttpMethod.Put, $"productos/{idProducto}", true);
        }

        public async Task<bool> UpdateProducto(int idProducto, UpdateProductoApiModel productoApi)
        {
            return await Request(HttpMethod.Put, $"productos/{idProducto}", true, productoApi);
        }

        private async Task<bool> Request(HttpMethod method, string url, bool auth = false, object body = null)
        {
            var response = await BuildRequest(method, url, auth, body);

            return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent;
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
                throw new DataAccessException(JsonConvert.DeserializeObject<ErrorModel>(content).Descripcion);
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
                {
                    string token = await GetAuthToken();
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                    

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
