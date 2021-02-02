using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;
using RestSharp.Serialization.Json;

namespace complemento_mvc.Models
{
    public class ServiceLogin
    {

        HttpClient client;
        static string urlBaseSeg = "https://172.30.254.58:8443/intranet/v1/api/seguridades/";

        public ServiceLogin()
        {
            client = new HttpClient();
            TimeSpan time = new TimeSpan(0, 0, 60);
            client.Timeout = time;
        }
        public async Task<ResponseVerificaUser> ValidaUsuario(RequestDatosRoot requestValida)
        {
            ResponseVerificaUser respuesta = null;
            string url = urlBaseSeg + "NewVerificaUser";
            /* string url = "https://172.30.254.58:8443/intranet/v1/api/seguridades/registrarUsuario";*/

            var request = JsonConvert.SerializeObject(requestValida);
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            var uri = new Uri(String.Format(url, string.Empty));
            try
            {
                var response = client.PostAsync(uri, content);
                if (response.Result.IsSuccessStatusCode)
                {
                    var result = await response.Result.Content.ReadAsStringAsync();

                    respuesta = JsonConvert.DeserializeObject<ResponseVerificaUser>(result);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("peticion cancelada NewVerificaUser");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error NewVerificaUser {e.Message}");
            }

            return respuesta;
        }

        public async Task<ResponseRegistraUser> RegistraUser(RequestNewResgistraUserRoot requestValida, string AccessToken)
        {
            ResponseRegistraUser respuesta = null;
            string acceso = "Bearer " + AccessToken;
            string url = "https://172.30.254.58:8443/intranet/v1/api/seguridades/registrarUsuario";
            var request = JsonConvert.SerializeObject(requestValida);
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AccessToken);
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            //content.Headers.Add("Authorization", acceso);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var uri = new Uri(String.Format(url, string.Empty));
            try
            {
                var response = client.PostAsync(uri, content);
                if (response.Result.IsSuccessStatusCode)
                {
                    var result = await response.Result.Content.ReadAsStringAsync();

                    respuesta = JsonConvert.DeserializeObject<ResponseRegistraUser>(result);
                }
            }
            catch (OperationCanceledException)
            {

                Console.WriteLine("peticion cancelada newRegistraUser");
            }
            catch (Exception e)
            {
                respuesta = new ResponseRegistraUser();
                respuesta.NewRegistraUserResult = new NewRegistraUserResponse();
                respuesta.NewRegistraUserResult.MsgError = e.InnerException.InnerException.Message;
                Console.WriteLine($"Error newRegistraUser {e.InnerException.InnerException.Message}");
            }

            return respuesta;
        }

        public async Task<ResponseMenuSemanal> ConsultarMenuSemanal(RequestMenuSemanalRoot requestValida, string AccessToken)
        {
            ResponseMenuSemanal respuesta = null;
            string acceso = "Bearer " + AccessToken;
            string url = "https://172.30.254.58:8443/intranet/v1/api/cafeteriaService/consultarMenuSemanal";
            var request = JsonConvert.SerializeObject(requestValida);
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AccessToken);
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            //content.Headers.Add("Authorization", acceso);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var uri = new Uri(String.Format(url, string.Empty));
            try
            {
                var response = client.PostAsync(uri, content);
                if (response.Result.IsSuccessStatusCode)
                {
                    var result = await response.Result.Content.ReadAsStringAsync();

                    respuesta = JsonConvert.DeserializeObject<ResponseMenuSemanal>(result);
                }
            }
            catch (OperationCanceledException)
            {

                Console.WriteLine("peticion cancelada ConsultarMenuSemanal");
            }
            catch (Exception e)
            {
                respuesta = new ResponseMenuSemanal();
                respuesta.@return = new ConsultarMenuSemanal();
                respuesta.@return.descripcion = e.InnerException.InnerException.Message;
                Console.WriteLine($"Error ConsultarMenuSemanal {e.InnerException.InnerException.Message}");
            }

            return respuesta;
        }

        public async Task<DatosMarcaciones> ConsultarToken()
        {
            DatosMarcaciones Consulta = null;
            DatosMarcaciones lista = new DatosMarcaciones();
            try
            {
                var urlts = ConfigurationManager.AppSettings["ApimUrl"];
                var ClientId = ConfigurationManager.AppSettings["ClientIdMarcaciones"];
                var ClientSecret = ConfigurationManager.AppSettings["ClientSecretMarcaciones"];
                var client = new RestClient(new Uri(urlts));
                var request = new RestRequest("auth/oauth/v2/token", Method.POST, DataFormat.Json);
                request.AddQueryParameter("client_id", ClientId);
                request.AddQueryParameter("client_secret", ClientSecret);
                request.AddQueryParameter("grant_type", "client_credentials");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Postman-Token", "<calculated when request is sent>");
                request.AddHeader("Content-Length", "0");
                //request.AddHeader("Host", "<calculated when request is sent>");
                request.AddHeader("User-Agent", "PostmanRuntime/7.26.3");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Connection", "keep-alive");
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var ConsultaEmpleado = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                JsonDeserializer Deserial = new JsonDeserializer();
                if (ConsultaEmpleado.StatusCode.ToString() == "OK")
                {
                    Consulta = Deserial.Deserialize<DatosMarcaciones>(ConsultaEmpleado);
                    lista.Codigo = "0000";
                    lista.Mensaje = "Ok";
                    lista.Access_token = Consulta.Access_token;
                }
                else
                {
                    if (ConsultaEmpleado.ErrorMessage != "")
                    {
                        lista.Mensaje = ConsultaEmpleado.ErrorMessage.ToString();
                    }
                    else
                    {
                        lista.Mensaje = "Error en consumo de servicio token";
                    }
                    lista.Codigo = "9999";
                }
            }

            catch (Exception ex)
            {
                //BPE.Centric.Dominio.TrackPoint.Interfaces.ITrackPoint _rep_log = new BPE.Centric.Infraestructura.Persistencia.TrackPoint.Repositorios.TrackPoint();
                //EntidadDomGral.ParamGenerico lg = new EntidadDomGral.ParamGenerico()
                //{
                //    ParamGenerico1 = "Error en Servicio Consultar Token"
                //};

                //_rep_log.RegistrarLogExceptionCCentric(ex, petDom, lg);
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }

        public async Task<DatosMarcaciones> ConsultarEmpleado(string usuario, string token)
        {
            DatosMarcaciones Consulta = null;
            DatosMarcaciones lista = new DatosMarcaciones();
            try
            {
                string Acceso = "Bearer " + token;
                var urlts = ConfigurationManager.AppSettings["ApimUrl"];
                var client = new RestClient(new Uri(urlts));
                var request = new RestRequest("registroEntrada/internal/v1/api/empleadoMarcacion/consultarEmpleado/{idUsuario}", Method.GET, DataFormat.Json);
                request.AddHeader("Authorization", Acceso);
                request.AddUrlSegment("idUsuario", usuario);
                var ConsultaEmpleado = await client.ExecuteTaskAsync(request).ConfigureAwait(false);
                JsonDeserializer Deserial = new JsonDeserializer();
                if (ConsultaEmpleado.StatusCode.ToString() == "OK")
                {
                    if (ConsultaEmpleado.Content != "")
                    {
                        Consulta = Deserial.Deserialize<DatosMarcaciones>(ConsultaEmpleado);
                        lista = Consulta; lista.Codigo = "0000"; lista.Mensaje = Consulta.Mensaje;
                    }
                    else { lista.Codigo = "20"; }
                }
                else
                {
                    lista.Codigo = "9999";
                    if (ConsultaEmpleado.ErrorMessage == "")
                    {
                        lista.Mensaje = "Error en consumo de servicio ConsultarEmpleado";
                    }
                    else
                    {
                        lista.Mensaje = ConsultaEmpleado.ErrorMessage.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //BPE.Centric.Dominio.TrackPoint.Interfaces.ITrackPoint _rep_log = new BPE.Centric.Infraestructura.Persistencia.TrackPoint.Repositorios.TrackPoint();
                //EntidadDomGral.ParamGenerico lg = new EntidadDomGral.ParamGenerico()
                //{ ParamGenerico1 = "Error en Servicio Consultar Token" };
                //_rep_log.RegistrarLogExceptionCCentric(ex, petDom, lg);
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }

        public DatosMarcaciones ObtenerTokenUser()
        {
            DatosMarcaciones Consulta = null;
            DatosMarcaciones lista = new DatosMarcaciones();
            try
            {
                var client = new RestClient("https://172.30.254.58:8443/auth/oauth/v2/token?client_id=94688657-6044-48d9-9732-2e672793f157&client_secret=99e39d6d-cdfe-492e-9456-cec6ef26ac04&grant_type=client_credentials");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse response = client.Execute(request);
                JsonDeserializer Deserial = new JsonDeserializer();
                Consulta = Deserial.Deserialize<DatosMarcaciones>(response);
                lista.Access_token = Consulta.Access_token;
            }

            catch (Exception ex)
            {
                //BPE.Centric.Dominio.TrackPoint.Interfaces.ITrackPoint _rep_log = new BPE.Centric.Infraestructura.Persistencia.TrackPoint.Repositorios.TrackPoint();
                //EntidadDomGral.ParamGenerico lg = new EntidadDomGral.ParamGenerico()
                //{
                //    ParamGenerico1 = "Error en Servicio Consultar Token"
                //};

                //_rep_log.RegistrarLogExceptionCCentric(ex, petDom, lg);
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }

        public DatosMarcaciones ObtenerTokenMenu()
        {
            DatosMarcaciones Consulta = null;
            DatosMarcaciones lista = new DatosMarcaciones();
            try
            {
                var client = new RestClient("https://172.30.254.58:8443/auth/oauth/v2/token?client_id=6d3a4e5a-a7be-4f11-96ba-9c6c49feee2a&client_secret=08eabb62-767a-4777-a998-ad969800b76b&grant_type=client_credentials");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse response = client.Execute(request);
                JsonDeserializer Deserial = new JsonDeserializer();
                Consulta = Deserial.Deserialize<DatosMarcaciones>(response);
                lista.Access_token = Consulta.Access_token;
            }

            catch (Exception ex)
            {
                //BPE.Centric.Dominio.TrackPoint.Interfaces.ITrackPoint _rep_log = new BPE.Centric.Infraestructura.Persistencia.TrackPoint.Repositorios.TrackPoint();
                //EntidadDomGral.ParamGenerico lg = new EntidadDomGral.ParamGenerico()
                //{
                //    ParamGenerico1 = "Error en Servicio Consultar Token"
                //};

                //_rep_log.RegistrarLogExceptionCCentric(ex, petDom, lg);
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }
    }
}