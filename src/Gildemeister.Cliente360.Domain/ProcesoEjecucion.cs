using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("proceso_ejecucion")]
    public partial class ProcesoEjecucion : BaseEntity
    {
        public ProcesoEjecucion()
        {
            ProcesoEjecucionDetalle = new HashSet<ProcesoEjecucionDetalle>();
        }

        [Key]
        [Column("nid_proceso_ejecucion")]
        public int NidProcesoEjecucion { get; set; }
        [Required]
        [Column("data")]
        [StringLength(8000)]
        public string Data { get; set; }
        [Required]
        [Column("co_estado", TypeName = "char(4)")]
        public string CoEstado { get; set; }
        [Column("fe_ejecucion", TypeName = "datetime")]
        public DateTime FeEjecucion { get; set; }
     
        [Column("nid_tipo_proceso")]
        public int NidTipoProceso { get; set; }

        [ForeignKey("NidTipoProceso")]
        [InverseProperty("ProcesoEjecucion")]
        public TipoProceso NidTipoProcesoNavigation { get; set; }
        [InverseProperty("NidProcesoEjecucionNavigation")]
        public ICollection<ProcesoEjecucionDetalle> ProcesoEjecucionDetalle { get; set; }
    }
}
