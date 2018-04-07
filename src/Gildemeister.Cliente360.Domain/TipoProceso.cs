using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("tipo_proceso")]
    public partial class TipoProceso
    {
        public TipoProceso()
        {
            ProcesoConfiguracion = new HashSet<ProcesoConfiguracion>();
            ProcesoEjecucion = new HashSet<ProcesoEjecucion>();
        }

        [Key]
        [Column("nid_tipo_proceso")]
        public int NidTipoProceso { get; set; }
        [Required]
        [Column("no_tipo_proceso")]
        [StringLength(120)]
        public string NoTipoProceso { get; set; }
        [Column("fe_crea", TypeName = "datetime")]
        public DateTime FeCrea { get; set; }
        [Required]
        [Column("co_usuario_crea", TypeName = "char(20)")]
        public string CoUsuarioCrea { get; set; }
        [Column("fe_cambio", TypeName = "datetime")]
        public DateTime? FeCambio { get; set; }
        [Column("co_usuario_cambio", TypeName = "char(20)")]
        public string CoUsuarioCambio { get; set; }
        [Column("no_usuario_red", TypeName = "char(20)")]
        public string NoUsuarioRed { get; set; }
        [Column("no_estacion_red", TypeName = "char(20)")]
        public string NoEstacionRed { get; set; }
        [Column("fl_inactivo")]
        public bool FlInactivo { get; set; }

        [InverseProperty("NidTipoProcesoNavigation")]
        public ICollection<ProcesoConfiguracion> ProcesoConfiguracion { get; set; }
        [InverseProperty("NidTipoProcesoNavigation")]
        public ICollection<ProcesoEjecucion> ProcesoEjecucion { get; set; }
    }
}
