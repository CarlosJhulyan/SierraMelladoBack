using System;
using System.Collections.Generic;

namespace SierraMelladoBack.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Articulos = new HashSet<Articulo>();
            Mensajes = new HashSet<Mensaje>();
        }

        public int IdAdmin { get; set; }
        public string? Rol { get; set; }
        public int? IdUsuario { get; set; }
        public int? Estado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
