using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("vehiculo")]
    public partial class Vehiculo : BaseEntity
    {
        public Vehiculo()
        {
            ClienteVehiculo = new HashSet<ClienteVehiculo>();
        }

        [Key]
        [Column("nid_vehiculo")]
        public int NidVehiculo { get; set; }

        [Column("nid_vin")]
        public int? NidVin { get; set; }

        [Column("nu_placa")]
        [StringLength(50)]
        public string NuPlaca { get; set; }
      
        [Column("nid_marca")]
        public int NidMarca { get; set; }

        [Required]
        [Column("nid_modelo", TypeName = "char(10)")]
        public string NidModelo { get; set; }

        [ForeignKey("NidMarca")]
        [InverseProperty("Vehiculo")]
        public Marca NidMarcaNavigation { get; set; }

        [ForeignKey("NidModelo")]
        [InverseProperty("Vehiculo")]
        public Modelo NidModeloNavigation { get; set; }

        [InverseProperty("NidVehiculoNavigation")]
        public ICollection<ClienteVehiculo> ClienteVehiculo { get; set; }
    }
}
