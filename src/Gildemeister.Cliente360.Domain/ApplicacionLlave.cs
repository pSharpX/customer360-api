using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    [Table("aplicacion_llave")]
    public class ApplicacionLlave
    {
        public ApplicacionLlave()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("aplicacion_id")]
        public int AplicacionId { get; set; }

        [Column("nid_aplicacion_repositorio")]
        public int NidAplicacionRepositorio { get; set; }

        [Column("app_llave")]
        public string Llave { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("fl_inactivo")]
        public bool Activo { get; set; }

        [Column("fe_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("fe_cambio")]
        public DateTime FechaCambio { get; set; }

        [Column("no_usuario_red")]
        public string UsuarioRed { get; set; }

        [Column("no_estacion_red")]
        public string EstacionRed { get; set; }

        public virtual AplicacionRepositorio AplicacionRepositorio { get; set; }
    }
}
