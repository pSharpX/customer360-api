using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    public partial class Logs
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string CallSite { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string StackTrace { get; set; }
        public string Method { get; set; }
        [Required]
        public string InnerException { get; set; }
        [Required]
        public string AdditionalInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LoggedOnDate { get; set; }
        public string Browser { get; set; }
        [StringLength(120)]
        public string User { get; set; }
        public string RequestUrl { get; set; }
        [Column("IPAddressServer")]
        public string IpaddressServer { get; set; }
        [Column("ClientIPAddress")]
        public string ClientIpaddress { get; set; }
    }
}
