using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    [Table("token_acceso")]
    public partial class TokenAcceso
    {
        public TokenAcceso()
        {

        }

        [Key]
        [Column("nid_token_acceso")]
        public int TokenAccesoId { get; set; }


        [Column("co_token")]
        public string AccesToken { get; set; }


        [Column("nid_usuario")]
        public int UsuarioId { get; set; }


        [Column("no_usuario")]
        public string Usuario { get; set; }

        [Column("nid_perfil")]
        public int PerfilId { get; set; }

        [Column("fl_validado")]
        public bool Validado { get; set; }

        [Column("no_fecha")]
        public DateTime FechaRegistro { get; set; }

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
    }
}
