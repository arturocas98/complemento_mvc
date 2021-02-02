using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace complemento_mvc.Models
{
    public class Login
    {
        public  void btnLogin(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            try
            {
                string nombreUsuario = ConsultarCredenciales();
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                ServiceLogin service = new ServiceLogin();
                RequestNewResgistraUser DatosRegistraUser = new RequestNewResgistraUser();
                string Usuario = "Erosado";//idUsuario.Value;
                string Password = "25";//idPassword.Value;
                String Maquina = Convert.ToString(localIPs[2]);

                if ((Usuario != null && Usuario != "") && (Password != null && Password != ""))
                {
                    DatosRegistraUser.Usuario = Usuario;
                    DatosRegistraUser.Clave = Password;
                    DatosRegistraUser.Maquina = Maquina;//"10.32.100.25";//ObtenerIpCliente();
                    DatosRegistraUser.LastLogon = "S";
                    DatosRegistraUser.Sesion = "";
                    var obtenerToken = service.ObtenerTokenUser();
                    //Numtoken.Text = obtenerToken.Access_token;
                    var accessToken = obtenerToken.Access_token;


                    #region Registra User

                    /*
                    var responseRegistraUser = service.RegistraUser(new RequestNewResgistraUserRoot { Datos = DatosRegistraUser }, obtenerToken.Access_token).Result;
                    if (responseRegistraUser.NewRegistraUserResult.MsgError != "Ok")
                    {
                        mensaje = responseRegistraUser.NewRegistraUserResult.MsgError;
                        //ViewData.Add("Mensaje", mensaje);
                        Msg.Text = mensaje;
                    }
                    else
                    {
                        HttpContext context = HttpContext.Current;
                        context.Session.Remove("SessionRegistraUser");
                        context.Session.Add("SessionRegistraUser", responseRegistraUser);
                        RequestDatos DatosVerificaUser = new RequestDatos();
                        DatosVerificaUser.Token = responseRegistraUser.NewRegistraUserResult.Token;
                        DatosVerificaUser.Canal = "PC";
                        DatosVerificaUser.Maquina = "10.1.100.235";//ObtenerIpCliente();
                        DatosVerificaUser.IdUsuario = Usuario;
                        DatosVerificaUser.IdAplicacion = 182;
                        DatosVerificaUser.IdOrganizacion = 274;
                        DatosVerificaUser.Otros = "S";
                        DatosVerificaUser.RecContable = "N";
                        DatosVerificaUser.RecRoles = "N";
                        DatosVerificaUser.RecupPerfil = "S";
                        DatosVerificaUser.RetConexion = "N";
                        DatosVerificaUser.Switch = "N";

                        var responseVerificaUser = service.ValidaUsuario(new RequestDatosRoot { Datos = DatosVerificaUser }).Result;
                        if (responseVerificaUser == null)
                        {
                            mensaje = responseVerificaUser.NewVerificaUserResult.MsgError;

                            Msg.Text = mensaje;
                        }
                        else
                        {
                            context.Session.Remove("VerificaUserSession");
                            context.Session.Add("VerificaUserSession", responseVerificaUser);
                            HttpContext.Current.Response.Redirect("http://srvsp:82/SitePages/PostValidacion.aspx", false);
                        }
                    }*/
                    #endregion


                    /* var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);


                     using (var clientContext = spContext.CreateUserClientContextForSPHost())
                     {
                         List<string> listasDelSitio = new List<string>();

                         clientContext.Load(clientContext.Web.Lists,
                         lists => lists.Include(list => list.Title,
                                                list => list.Id));


                         clientContext.ExecuteQuery();


                         foreach (List list in clientContext.Web.Lists)
                         {

                             if (list.Title == "CSLComplementoToken")
                             {

                                 listasDelSitio.Add(list.Title);
                                 break;
                             }


                         }

                         if (listasDelSitio.Count > 0)
                         {
                             ActualizarListaGenericaSharePoint(clientContext, accessToken);

                         }
                         else
                         {

                             CrearListaGenericaSharePoint(clientContext);
                             ActualizarListaGenericaSharePoint(clientContext, accessToken);
                         }



                     }*/





                }
                else
                {
                    mensaje = "El campo usuario y contraseña no debe estar vacio";

                    //Msg.Text = mensaje;
                }
            }
            catch (Exception ex)
            {

                mensaje = ex.Message;

                //Msg.Text = mensaje;
            }

        }

        protected string ConsultarCredenciales()
        {

            string nombreUsuario = string.Empty;
            /*var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);


           using (var clientContext = spContext.CreateUserClientContextForSPHost())
           {
               Web web = clientContext.Web;
               clientContext.Load(web);
               clientContext.ExecuteQuery();

               clientContext.Load(web.CurrentUser);
               clientContext.ExecuteQuery();
               nombreUsuario = clientContext.Web.CurrentUser.Title;
           }*/

            return nombreUsuario;
        }

        public DatosMarcaciones obtenerToken(string cliente_id, string client_secret)
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
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }
    }
}