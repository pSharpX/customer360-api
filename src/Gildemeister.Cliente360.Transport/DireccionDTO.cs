using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class DireccionDTO
    {

        public int Id { get; set; }

        public string Direccion { get; set; }

        public string CodigoDepartamento { get; set; }

        public string CodigoProvincia { get; set; }

        public string CodigoDistrito { get; set; }

        public string CodigoPais { get; set; }

        public string CodigoPostal { get; set; }
    }
}
