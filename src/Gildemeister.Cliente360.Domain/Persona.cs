using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    [Table("persona")]
    public partial class Persona: BaseEntity
    {
        public Persona()
        {
            PersonaDireccion = new HashSet<PersonaDireccion>();
        }

        [Key]
        [Column("nid_persona")]
        public int NidPersona { get; set; }

        [Column("no_persona")]
        [StringLength(80)]
        public string NoPersona { get; set; }

        [Column("no_apellido_paterno")]
        [StringLength(60)]
        public string NoApellidoPaterno { get; set; }

        [Column("no_apellido_materno")]
        [StringLength(60)]
        public string NoApellidoMaterno { get; set; }

        [Column("no_razon_social")]
        [StringLength(260)]
        public string NoRazonSocial { get; set; }

        [Column("fe_nacimiento", TypeName = "datetime")]
        public DateTime? FeNacimiento { get; set; }

        [Required]
        [Column("co_tipo_persona", TypeName = "char(4)")]
        public string CoTipoPersona { get; set; }

        [Required]
        [Column("co_tipo_documento", TypeName = "char(4)")]
        public string CoTipoDocumento { get; set; }

        [Required]
        [Column("nu_documento", TypeName = "char(20)")]
        public string NuDocumento { get; set; }

        [Column("no_correo")]
        [StringLength(260)]
        public string NoCorreo { get; set; }

        [Column("nu_telefono")]
        [StringLength(20)]
        public string NuTelefono { get; set; }

        [Column("nu_celular")]
        [StringLength(20)]
        public string NuCelular { get; set; }

        [Column("co_sexo", TypeName = "char(4)")]
        public string CoSexo { get; set; }

        [Column("co_estado_civil", TypeName = "char(10)")]
        public string CoEstadoCivil { get; set; }


        [InverseProperty("NidContactoNavigation")]
        public Cliente Cliente { get; set; }

        [InverseProperty("NidContactoNavigation")]
        public Contacto Contacto { get; set; }

        [InverseProperty("NidPersonaNavigation")]
        public ICollection<PersonaDireccion> PersonaDireccion { get; set; }
    }
}
