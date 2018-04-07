using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    
    public partial class TablaDetalle
    {
        public int TablaId { get; set; }

        public string Valor1 { get; set; }

        public string Valor2 { get; set; }

        public string Valor3 { get; set; }

        public string Valor4 { get; set; }

        public string Valor5 { get; set; }
    }
}
