using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class TablaGeneralDTO
    {
        public int IdTablaGeneral { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Pagina { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string TipoListado { get; set; }
    }
}
