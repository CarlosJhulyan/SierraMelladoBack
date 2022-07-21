using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Cita = new HashSet<Citum>();
        }

        public int IdServicio { get; set; }
        public string? Abreviatura { get; set; }
        public string? Descripcion { get; set; }
        public double? Precio { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
    }
}
