using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class RequestNewResgistraUser
    {
        public string Clave { get; set; }
        public string LastLogon { get; set; }
        public string Maquina { get; set; }
        public string Sesion { get; set; }
        public string Usuario { get; set; }

    }
    public class RequestNewResgistraUserRoot
    {
        public RequestNewResgistraUser Datos { get; set; }
    }
}