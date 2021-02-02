using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class PeticionServices
    {
        //public PeticionServices(); 
        public string NombreAgencia { get; set; }
        public string IdOrganizacion { get; set; }
        public string IdAplicativo { get; set; }
        public string Aplicativo { get; set; }
        public string Token { get; set; }
        public string MacAddress { get; set; }
        public string NombreLocalidad { get; set; }
        public int Localidad { get; set; }
        public string IdUsuario { get; set; }
        public string IdMensaje { get; set; }
        public string HostName { get; set; }
        public DateTime FechaHora { get; set; }
        public string Canal { get; set; }
        public int Agencia { get; set; }
        public string Ip { get; set; }
        public string NombreUsuario { get; set; }
    }
}