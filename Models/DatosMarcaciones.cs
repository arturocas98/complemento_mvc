using Microsoft.SharePoint.Client;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class DatosMarcaciones
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public int NumeroEmpleado { get; set; }
        public string Fecha { get; set; }
        public string FechaSinHora { get; set; }
        public string TipoMarcacion { get; set; }
        public string Procesado { get; set; }
        public string NombresEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public string NombresCompleto { get; set; }
        public string CodMotivoFyA { get; set; }
        public string CodigoTemporal { get; set; }
        public string MensajeTemporal { get; set; }
        public string Observacion { get; set; }
        public string Leyenda { get; set; }
        public string ValidaIp { get; set; }
        public string Extension { get; set; }
        public string DescripcionArea { get; set; }
        public int CodigoOrganigrama { get; set; }
        public string DescripcionOrganigrama { get; set; }
        public string CodigoEmpresa { get; set; }
        public string NumReportesFyA { get; set; }
        public int CodigoAgencia { get; set; }
        public string DescripcionAgencia { get; set; }
        public string Piso { get; set; }
        public string UrsSeguridadBancaria { get; set; }
        public string Estado { get; set; }
        public string DescripcionDepartamento { get; set; }
        public string UsuarioMedico { get; set; }
        public int CodDpto { get; set; }
        public int CodigoLocalidad { get; set; }
        public string DescripcionLocalidad { get; set; }
        public int CodigoLocalidadUbic { get; set; }
        public int CodigoEdificio { get; set; }
        public string DescripcionEdificio { get; set; }
        public string CodigoZona { get; set; }
        public string CodigoOpident { get; set; }
        public string DescripcionCargo { get; set; }
        public string CodigoArea { get; set; }
        public string UsrRrhh { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string EstadoEmpleado { get; set; }
        public string Access_token { get; set; }


        //public void createList()
        //{
        //    string siteUrl = "https://pacificobp.sharepoint.com/sites/Css_Intranetbp";

        //    ClientContext clientContext = new ClientContext(siteUrl);
        //    Web site = clientContext.Web;
        //    ListCreationInformation newListInfo = new ListCreationInformation();
        //    newListInfo.Title = "New Discussion Board";
        //    newListInfo.TemplateType = (int)ListTemplateType.DiscussionBoard;
        //    List newList = site.Lists.Add(newListInfo);

        //    clientContext.Load(newList);
        //    clientContext.ExecuteQuery();

        //    Console.WriteLine("Added Discussion Board: " + newList.Title);

        //}


        public void CrearListaGenericaSharePoint(ClientContext clientContext)
        {
            //Crear lista generica
            ListCreationInformation creationInfo = new ListCreationInformation();
            creationInfo.Title = "CSLComplementoToken";
            creationInfo.TemplateType = (int)ListTemplateType.GenericList;
            List list = clientContext.Web.Lists.Add(creationInfo);
            list.Description = "";
            list.Update();

            //Agregar campos a lista generica

            Field field = list.Fields.AddFieldAsXml("<Field DisplayName='Fecha' Type='DateTime' Format='DateTime' Required='TRUE' />",
                                           true,
                                           AddFieldOptions.DefaultValue);
            FieldDateTime fldDateTime = clientContext.CastTo<FieldDateTime>(field);

            fldDateTime.Update();
            clientContext.ExecuteQuery();
        }

        public void ActualizarListaGenericaSharePoint(ClientContext clientContext, string accessToken)
        {
            //Agregar Items a lista generica
            List TokenList = clientContext.Web.Lists.GetByTitle("CSLComplementoToken");

            CamlQuery query = CamlQuery.CreateAllItemsQuery(100);
            Microsoft.SharePoint.Client.ListItemCollection items = TokenList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();

            foreach (Microsoft.SharePoint.Client.ListItem listItemToken in items)
            {
                // We have all the list item data. For example, Title.
                //label1 = label1 + ", " + listItemToken["ID"];
                //label2 = label2 + ", " + listItemToken["Title"];

                Microsoft.SharePoint.Client.ListItem listItem = TokenList.GetItemById(Convert.ToInt32(listItemToken["ID"]));

                listItem.DeleteObject();
            }


            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            Microsoft.SharePoint.Client.ListItem newItem = TokenList.AddItem(itemCreateInfo);
            newItem["Title"] = accessToken;
            newItem["Fecha"] = DateTime.Now;
            newItem.Update();
            clientContext.ExecuteQuery();
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