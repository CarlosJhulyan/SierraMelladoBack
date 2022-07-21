using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Mensaje
    {
        public int IdMensaje { get; set; }
        public string? Contenido { get; set; }
        public DateTime? FechaEmi { get; set; }
        public string? CelularRem { get; set; }
        public string? CorreoRem { get; set; }
        public string? NombreApellidoRem { get; set; }
        public string? Tipo { get; set; }
        public int? IdAdmin { get; set; }
        public string? Estado { get; set; }

        public virtual Admin? IdAdminNavigation { get; set; }
    }
}
