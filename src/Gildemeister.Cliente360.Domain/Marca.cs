using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    //[Table("marca")]
    public partial class Marca: BaseEntity
    {
        //public Marca()
        //{
        //    Vehiculo = new HashSet<Vehiculo>();
        //}

        //[Key]
        //[Column("nid_marca")]
        //public int NidMarca { get; set; }
        //[Required]
        //[Column("no_marca")]
        //[StringLength(50)]
        //public string NoMarca { get; set; }
        //[Column("co_marca", TypeName = "char(20)")]
        //public string CoMarca { get; set; }


        //[InverseProperty("NidMarcaNavigation")]
        //public ICollection<Vehiculo> Vehiculo { get; set; }

        public int IdMarca { get; set; }
        public string CodigoMarca { get; set; }
        public string NombreMarca { get; set; }
    }
}
