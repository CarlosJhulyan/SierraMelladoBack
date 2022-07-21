using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Orden
    {
        public Orden()
        {
            Cita = new HashSet<Citum>();
        }

        public int NumOrden { get; set; }
        public double? MontoTotal { get; set; }
        public string? Descripcion { get; set; }
        public int? CodMetodo { get; set; }
        public string? Estado { get; set; }
        public double? Vuelto { get; set; }
        public double? ImporteTotal { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
    }
}
