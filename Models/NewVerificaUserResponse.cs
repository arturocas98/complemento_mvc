using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class NewVerificaUserResponse
    {
        public object AliaSwitch { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public AreaContable AreaContable { get; set; }
        public object Base { get; set; }
        public object Cargo { get; set; }
        public string CodError { get; set; }
        public string CodOrganigrama { get; set; }
        public object IdAgencia { get; set; }
        public object IdEmpresa { get; set; }
        public object IdLocalidad { get; set; }
        public IdRol IdRol { get; set; }
        public IdTransacciones IdTransacciones { get; set; }
        public string IdUsuario { get; set; }
        public string Identificacion { get; set; }
        public string MsgError { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public NombreUsr NombreUsr { get; set; }
        public string NumEmpleado { get; set; }
        public object Opident { get; set; }
        public object Servidor { get; set; }
        public object Terminal { get; set; }
        public object UserBD { get; set; }
        public object pwdBD { get; set; }
    }
    public class AreaContable
    {
        public string @nil { get; set; }
    }

    public class IdRol
    {
        public string @nil { get; set; }
    }

    public class IdTransacciones
    {
        public string @nil { get; set; }
    }

    public class NombreUsr
    {
        public string @nil { get; set; }
    }

    
    public class ResponseVerificaUser
    {
        public NewVerificaUserResponse NewVerificaUserResult { get; set; }
    }
}