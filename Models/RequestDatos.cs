using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class RequestDatos
    {
        public string Canal { get; set; }
        public int IdAplicacion { get; set; }
        public int IdOrganizacion { get; set; }
        public string IdUsuario { get; set; }
        public string Maquina { get; set; }
        public string Otros { get; set; }
        public string RecContable { get; set; }
        public string RecRoles { get; set; }
        public string RecupPerfil { get; set; }
        public string RetConexion { get; set; }
        public string Switch { get; set; }
        public string Token { get; set; }
    }
    public class RequestDatosRoot
    {
        public RequestDatos Datos { get; set; }
    }
}