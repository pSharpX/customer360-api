using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class TokenAccesoDTO
    {
        public int TokenAccesoId { get; set; }

        public string AccesToken { get; set; }

        public int UsuarioId { get; set; }

        public string Usuario { get; set; }

        public int PerfilId { get; set; }

        public bool Validado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaCambio { get; set; }

        public string UsuarioRed { get; set; }

        public string EstacionRed { get; set; }
    }
}
