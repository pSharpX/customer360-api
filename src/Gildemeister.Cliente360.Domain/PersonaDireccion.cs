using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("persona_direccion")]
    public partial class PersonaDireccion:BaseEntity
    {
        [Key]
        [Column("nid_direccion")]
        public int NidDireccion { get; set; }

        [Required]
        [Column("no_direccion")]
        [StringLength(260)]
        public string NoDireccion { get; set; }

        [Column("co_fax", TypeName = "char(20)")]
        public string CoFax { get; set; }

        [Column("co_postal", TypeName = "char(12)")]
        public string CoPostal { get; set; }

     
        [Column("nid_persona")]
        public int NidPersona { get; set; }

        [Required]
        [Column("coddpto", TypeName = "char(2)")]
        public string Coddpto { get; set; }

        [Required]
        [Column("codprov", TypeName = "char(2)")]
        public string Codprov { get; set; }

        [Required]
        [Column("coddist", TypeName = "char(2)")]
        public string Coddist { get; set; }

        [Column("nid_pais")]
        public int NidPais { get; set; }

        [ForeignKey("NidPais")]
        [InverseProperty("PersonaDireccion")]
        public Pais NidPaisNavigation { get; set; }

        [ForeignKey("NidPersona")]
        [InverseProperty("PersonaDireccion")]
        public Persona NidPersonaNavigation { get; set; }
    }
}
