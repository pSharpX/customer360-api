using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class ContactoDTO
    {
        public int Id { get; set; }

        public string Nombres  { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string NumeroDocumento { get; set; }

        public string CodigoTipoDocumento { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string CodigoSexo { get; set; }
    }
}
