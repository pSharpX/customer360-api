using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("log")]
    public partial class Log
    {
        [Key]
        [Column("nid_log")]
        public int NidLog { get; set; }
        [Column("co_nivel", TypeName = "char(10)")]
        public string CoNivel { get; set; }
        [Column("no_source")]
        [StringLength(50)]
        public string NoSource { get; set; }
        [Column("no_tipo")]
        [StringLength(20)]
        public string NoTipo { get; set; }
        [Column("mensaje", TypeName = "char(4000)")]
        public string Mensaje { get; set; }
        [Column("stack_trace")]
        [StringLength(10)]
        public string StackTrace { get; set; }
        [Column("metodo")]
        [StringLength(120)]
        public string Metodo { get; set; }
        [Column("exception")]
        [StringLength(4000)]
        public string Exception { get; set; }
        [Column("referencia")]
        [StringLength(400)]
        public string Referencia { get; set; }
        [Column("fe_log", TypeName = "datetime")]
        public DateTime? FeLog { get; set; }
        [Column("browser")]
        [StringLength(50)]
        public string Browser { get; set; }
        [Column("co_usuario", TypeName = "char(20)")]
        public string CoUsuario { get; set; }
        [Column("request_url")]
        [StringLength(250)]
        public string RequestUrl { get; set; }
        [Column("servidor_ip", TypeName = "char(30)")]
        public string ServidorIp { get; set; }
        [Column("cliente_ip", TypeName = "char(30)")]
        public string ClienteIp { get; set; }
    }
}
