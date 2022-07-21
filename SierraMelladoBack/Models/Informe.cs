using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Informe
    {
        public int NumInforme { get; set; }
        public string? Asunto { get; set; }
        public string? Archivo { get; set; }
        public DateTime? FechaEmi { get; set; }
        public int? IdMedico { get; set; }

        public virtual Medico? IdMedicoNavigation { get; set; }
    }
}
