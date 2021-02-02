using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class RequestMenuSemanal
    {
        public int agencia { get; set; }
        public string canal { get; set; }
        public string fechaHora { get; set; }
        public string hostName { get; set; }
        public string idMensaje { get; set; }
        public string idUsuario { get; set; }
        public string ip { get; set; }
        public int localidad { get; set; }
        public string macAddress { get; set; }
        public string token { get; set; }
        public int localidadMenu { get; set; }

    }

    public class RequestMenuSemanalRoot
    {
        public RequestMenuSemanal peticion { get; set; }
    }
}