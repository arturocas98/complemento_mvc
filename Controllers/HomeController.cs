using complemento_mvc.Models;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace complemento_mvc.Controllers
{
    public class HomeController : Controller
    {
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
                Console.WriteLine($"Error newRegistraUser {ex.InnerException.InnerException.Message}");
            }
            return lista;
        }

        public ActionResult Index()
        {
            var obtenerToken = this.ObtenerTokenUser();
            return View();
        }


       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}