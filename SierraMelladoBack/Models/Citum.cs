using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Citum
    {
        public Citum()
        {
            InformePacientes = new HashSet<InformePaciente>();
        }

        public int IdCita { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Hora { get; set; }
        public int? Medico { get; set; }
        public int? Paciente { get; set; }
        public int? Servicio { get; set; }
        public int? NumOrden { get; set; }

        public virtual Medico? MedicoNavigation { get; set; }
        public virtual Orden? NumOrdenNavigation { get; set; }
        public virtual Paciente? PacienteNavigation { get; set; }
        public virtual Servicio? ServicioNavigation { get; set; }
        public virtual ICollection<InformePaciente> InformePacientes { get; set; }
    }
}
