using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            CodEtiqueta = new HashSet<Etiquetum>();
        }

        public int CodArticulo { get; set; }
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public DateTime? FechaCrea { get; set; }
        public string? Imagen { get; set; }
        public string? Autor { get; set; }
        public DateTime? FechaMod { get; set; }
        public int? IdAdmin { get; set; }

        public virtual Admin? IdAdminNavigation { get; set; }

        public virtual ICollection<Etiquetum> CodEtiqueta { get; set; }
    }
}
