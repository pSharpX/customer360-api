using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
   public class UsuarioDTO
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string NombreUsuario { get; set; }

        public int PerfilId { get; set; }

        public string Perfil { get; set; }

        public string Opcion { get; set; }

        public string Menu { get; set; }

        public string Pagina { get; set; }
    }
}
