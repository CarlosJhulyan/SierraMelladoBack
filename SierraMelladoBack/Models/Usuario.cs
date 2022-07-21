using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Admins = new HashSet<Admin>();
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public int IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? Correo { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaMod { get; set; }
        public string? Usuario1 { get; set; }
        public string? Clave { get; set; }
        public string? ApellidoMaterno { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
