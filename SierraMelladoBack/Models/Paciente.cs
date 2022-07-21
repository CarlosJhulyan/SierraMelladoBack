using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Cita = new HashSet<Citum>();
            InformePacientes = new HashSet<InformePaciente>();
        }

        public int IdPaciente { get; set; }
        public string? Dni { get; set; }
        public int? IdUsuario { get; set; }
        public string? Celular { get; set; }
        public DateTime? FechaNac { get; set; }
        public string? Avatar { get; set; }
        public int? Estado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<InformePaciente> InformePacientes { get; set; }
    }
}
