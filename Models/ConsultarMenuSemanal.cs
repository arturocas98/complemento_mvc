using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace complemento_mvc.Models
{
    public class ConsultarMenuSemanal
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public int duracionTarea { get; set; }
        public string tipo { get; set; }
    }

    public class ResponseMenuSemanal
    {
        public ConsultarMenuSemanal @return { get; set; }
    }
}