using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            IdMedicos = new HashSet<Medico>();
        }

        public int CodEspecialidad { get; set; }
        public string? Descripcion { get; set; }
        public string? Abreviatura { get; set; }

        public virtual ICollection<Medico> IdMedicos { get; set; }
    }
}
