using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{

    public partial class Pais
    {
        public int IdPais { get; set; } 

        public string Nombre { get; set; }

        public string CodigoPais { get; set; }
    }
}
