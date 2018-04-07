using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("asesor")]
    public partial class Asesor:BaseEntity
    {
        public Asesor()
        {
            Cliente = new HashSet<Cliente>();
        }

        [Key]
        [Column("nid_asesor")]
        public int NidAsesor { get; set; }

        [Required]
        [Column("no_asesor")]
        [StringLength(250)]
        public string NoAsesor { get; set; }

        [InverseProperty("NidAsesorNavigation")]
        public ICollection<Cliente> Cliente { get; set; }
    }
}
