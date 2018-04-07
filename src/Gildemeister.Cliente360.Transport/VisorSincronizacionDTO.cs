using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class VisorSincronizacionDTO
    {
        public string Fecha { get; set; }
        public string TipoSincronizacion { get; set; }
        public int IdProceso { get; set; }
        public int IdDetalleProceso { get; set; }
        public string TipoProceso { get; set; }
        public string Aplicacion { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string Data { get; set; }
    }
}
