using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("aplicacion_repositorio")]
    public partial class AplicacionRepositorio
    {
        public AplicacionRepositorio()
        {
            ProcesoConfiguracionDetalle = new HashSet<ProcesoConfiguracionDetalle>();
            ProcesoEjecucionDetalle = new HashSet<ProcesoEjecucionDetalle>();
            ApplicacionLlave = new HashSet<ApplicacionLlave>();
        }

        [Key]
        [Column("nid_aplicacion_repositorio")]
        public int NidAplicacionRepositorio { get; set; }
        [Required]
        [Column("no_repositorio")]
        [StringLength(120)]
        public string NoRepositorio { get; set; }
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

        [InverseProperty("NidAplicacionRepositorioNavigation")]
        public ICollection<ProcesoConfiguracionDetalle> ProcesoConfiguracionDetalle { get; set; }
        [InverseProperty("NidAplicacionRepositorioNavigation")]
        public ICollection<ProcesoEjecucionDetalle> ProcesoEjecucionDetalle { get; set; }

        public ICollection<ApplicacionLlave> ApplicacionLlave { get; set; }
    }
}
