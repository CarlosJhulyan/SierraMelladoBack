using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Etiquetum
    {
        public Etiquetum()
        {
            CodArticulos = new HashSet<Articulo>();
        }

        public int CodEtiqueta { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Articulo> CodArticulos { get; set; }
    }
}
