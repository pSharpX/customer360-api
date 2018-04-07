using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    //[Table("modelo")]
    public partial class Modelo:BaseEntity
    {
        //public Modelo()
        //{
        //    Vehiculo = new HashSet<Vehiculo>();
        //}

        //[Key]
        //[Column("nid_modelo", TypeName = "char(10)")]
        //public string NidModelo { get; set; }
        //[Required]
        //[Column("no_modelo", TypeName = "char(10)")]
        //public string NoModelo { get; set; }


        //[InverseProperty("NidModeloNavigation")]
        //public ICollection<Vehiculo> Vehiculo { get; set; }

        public int IdModelo { get; set; }
        public string CodigoModelo { get; set; }
        public string NombreModelo { get; set; }
    }
}
