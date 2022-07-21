using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Horario
    {
        public int CodHorario { get; set; }
        public int? HoraInicio { get; set; }
        public int? HoraFin { get; set; }
        public string? Dia { get; set; }
        public int? IdMedico { get; set; }

        public virtual Medico? IdMedicoNavigation { get; set; }
    }
}
