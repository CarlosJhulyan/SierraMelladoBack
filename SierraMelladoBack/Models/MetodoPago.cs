using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class MetodoPago
    {
        public int CodMetodo { get; set; }
        public string? Descripcion { get; set; }
        public string? Abreviatura { get; set; }
    }
}
