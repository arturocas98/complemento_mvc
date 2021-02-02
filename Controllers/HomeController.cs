using complemento_mvc.Models;
using Microsoft.SharePoint.Client;
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
        private DatosMarcaciones datosMarcaciones = new DatosMarcaciones();
        [HttpGet]

        public ActionResult Index()
        {
            //.createList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string cliente_id, string client_secret,string label1, string label2)
        {
            var resultado = datosMarcaciones.obtenerToken(cliente_id, client_secret);
            ViewBag.acces_token = resultado.Access_token;

            OfficeDevPnP.Core.AuthenticationManager authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            ClientContext clientContext = authenticationManager.GetWebLoginClientContext("https://pacificobp.sharepoint.com/sites/Css_Intranetbp", null);
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
                datosMarcaciones.ActualizarListaGenericaSharePoint(clientContext, resultado.Access_token);

            }
            else
            {

                datosMarcaciones.CrearListaGenericaSharePoint(clientContext);
                datosMarcaciones.ActualizarListaGenericaSharePoint(clientContext, resultado.Access_token);
            }
            //string siteUrl = "https://pacificobp.sharepoint.com/sites/Css_Intranetbp";

            //ClientContext clientContext = new ClientContext(siteUrl);
            datosMarcaciones.ActualizarListaGenericaSharePoint(clientContext, resultado.Access_token);
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