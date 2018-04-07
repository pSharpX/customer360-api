using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("proceso_ejecucion_detalle")]
    public partial class ProcesoEjecucionDetalle:BaseEntity
    {
        [Key]
        [Column("nid_proceso_ejecucion_detalle")]
        public int NidProcesoEjecucionDetalle { get; set; }
        [Column("fe_inicio", TypeName = "datetime")]
        public DateTime FeInicio { get; set; }
        [Column("fe_fin", TypeName = "datetime")]
        public DateTime FeFin { get; set; }
        [Required]
        [Column("co_estado", TypeName = "char(4)")]
        public string CoEstado { get; set; }
        [Column("co_referencia")]
        [StringLength(20)]
        public string CoReferencia { get; set; }
       

        [Column("nid_aplicacion_repositorio")]
        public int NidAplicacionRepositorio { get; set; }
        [Column("nid_proceso_ejecucion")]
        public int NidProcesoEjecucion { get; set; }

        [ForeignKey("NidAplicacionRepositorio")]
        [InverseProperty("ProcesoEjecucionDetalle")]
        public AplicacionRepositorio NidAplicacionRepositorioNavigation { get; set; }
        [ForeignKey("NidProcesoEjecucion")]
        [InverseProperty("ProcesoEjecucionDetalle")]
        public ProcesoEjecucion NidProcesoEjecucionNavigation { get; set; }
    }
}
