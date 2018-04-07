using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("cliente_contacto")]
    public partial class ClienteContacto: BaseEntity
    {
        [Key]
        [Column("nid_cliente_contacto")]
        public int NidClienteContacto { get; set; }
        [Column("nid_contacto")]
        public int NidContacto { get; set; }

        [ForeignKey("NidContacto")]
        [InverseProperty("ClienteContacto")]
        public Contacto NidContacto1 { get; set; }
        [ForeignKey("NidContacto")]
        [InverseProperty("ClienteContacto")]
        public Cliente NidContactoNavigation { get; set; }
    }
}
