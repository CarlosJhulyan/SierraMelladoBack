using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class InformePaciente
    {
        public int NumInforme { get; set; }
        public string? Resumen { get; set; }
        public DateTime? FechaEmi { get; set; }
        public string? Archivo { get; set; }
        public int? Medico { get; set; }
        public int? Paciente { get; set; }
        public int? Cita { get; set; }

        public virtual Citum? CitaNavigation { get; set; }
        public virtual Medico? MedicoNavigation { get; set; }
        public virtual Paciente? PacienteNavigation { get; set; }
    }
}
