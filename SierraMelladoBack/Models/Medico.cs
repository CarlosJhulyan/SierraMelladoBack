using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Medico
    {
        public Medico()
        {
            Cita = new HashSet<Citum>();
            Horarios = new HashSet<Horario>();
            InformePacientes = new HashSet<InformePaciente>();
            Informes = new HashSet<Informe>();
            CodEspecialidads = new HashSet<Especialidad>();
        }

        public int IdMedico { get; set; }
        public string? CodColegiado { get; set; }
        public string? Dni { get; set; }
        public int? IdUsuario { get; set; }
        public string? Celular { get; set; }
        public DateTime? FechaNac { get; set; }
        public string? Avatar { get; set; }
        public int? Estado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Citum> Cita { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<InformePaciente> InformePacientes { get; set; }
        public virtual ICollection<Informe> Informes { get; set; }

        public virtual ICollection<Especialidad> CodEspecialidads { get; set; }
    }
}
