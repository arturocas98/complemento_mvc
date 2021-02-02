using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{

    public class NewRegistraUserResponse
    {
        public string CodError { get; set; }
        public FechaIngreso FechaIngreso { get; set; }
        public IdAplicaciones IdAplicaciones { get; set; }
        public string IdUsuario { get; set; }
        public string LastLogon { get; set; }
        public string Maquina { get; set; }
        public string MsgError { get; set; }
        public string Token { get; set; }

    }

    public class FechaIngreso
    {
        public string @nil { get; set; }
    }
    public class IdNombre
    {
        public string Descripcion { get; set; }
        public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
    }
    public class IdAplicaciones
    {
        public List<IdNombre> IdNombres { get; set; }
    }
    

    public class ResponseRegistraUser
    {
        public NewRegistraUserResponse NewRegistraUserResult { get; set; }
    }
}