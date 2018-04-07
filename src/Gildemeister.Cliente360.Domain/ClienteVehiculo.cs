using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("cliente_vehiculo")]
    public partial class ClienteVehiculo: BaseEntity    
    {
        [Key]
        [Column("nid_cliente_vehiculo")]
        public int NidClienteVehiculo { get; set; }
        [Column("fl_propietario_actual")]
        public bool FlPropietarioActual { get; set; }

        [Column("nid_contacto")]
        public int NidContacto { get; set; }
        [Column("nid_vehiculo")]
        public int NidVehiculo { get; set; }

        [ForeignKey("NidContacto")]
        [InverseProperty("ClienteVehiculo")]
        public Cliente NidContactoNavigation { get; set; }
        [ForeignKey("NidVehiculo")]
        [InverseProperty("ClienteVehiculo")]
        public Vehiculo NidVehiculoNavigation { get; set; }
    }
}
