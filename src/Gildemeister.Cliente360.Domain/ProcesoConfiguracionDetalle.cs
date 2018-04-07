using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("proceso_configuracion_detalle")]
    public partial class ProcesoConfiguracionDetalle : BaseEntity
    {
        [Key]
        [Column("nid_proceso_configuracion_detalle")]
        public int NidProcesoConfiguracionDetalle { get; set; }
        [Column("fl_iniciador")]
        public bool? FlIniciador { get; set; }
    
        [Column("nid_proceso_configuracion")]
        public int NidProcesoConfiguracion { get; set; }
        [Column("nid_aplicacion_repositorio")]
        public int NidAplicacionRepositorio { get; set; }

        [ForeignKey("NidAplicacionRepositorio")]
        [InverseProperty("ProcesoConfiguracionDetalle")]
        public AplicacionRepositorio NidAplicacionRepositorioNavigation { get; set; }
        [ForeignKey("NidProcesoConfiguracion")]
        [InverseProperty("ProcesoConfiguracionDetalle")]
        public ProcesoConfiguracion NidProcesoConfiguracionNavigation { get; set; }
    }
}
