using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("proceso_configuracion")]
    public partial class ProcesoConfiguracion:BaseEntity
    {
        public ProcesoConfiguracion()
        {
            ProcesoConfiguracionDetalle = new HashSet<ProcesoConfiguracionDetalle>();
        }

        [Key]
        [Column("nid_proceso_configuracion")]
        public int NidProcesoConfiguracion { get; set; }
  
        [Column("nid_tipo_proceso")]
        public int NidTipoProceso { get; set; }

        [ForeignKey("NidTipoProceso")]
        [InverseProperty("ProcesoConfiguracion")]
        public TipoProceso NidTipoProcesoNavigation { get; set; }
        [InverseProperty("NidProcesoConfiguracionNavigation")]
        public ICollection<ProcesoConfiguracionDetalle> ProcesoConfiguracionDetalle { get; set; }
    }
}
