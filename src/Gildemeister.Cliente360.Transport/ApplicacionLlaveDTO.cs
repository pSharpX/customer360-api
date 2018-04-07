using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class ApplicacionLlaveDTO
    {
        public int AplicacionId { get; set; }

        public int NidAplicacionRepositorio { get; set; }

        public string Llave { get; set; }

        public string Url { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaCambio { get; set; }

        public string UsuarioRed { get; set; }

        public string EstacionRed { get; set; }
    }
}
