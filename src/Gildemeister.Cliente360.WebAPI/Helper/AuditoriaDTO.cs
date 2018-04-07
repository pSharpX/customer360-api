using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI.Helper
{
    public class AuditoriaDTO
    {
        public string UsuarioCambio { get; set; }

        public DateTime FechaCambio { get; set; }

        public string EstacionRed { get; set; }

        public string UsuarioRed { get; set; }
    }
}
