using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    public abstract class BaseEntity
    {

        public bool Inactivo { get; set; } = false;

        public string UsuarioCreacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioRed { get; set; }

        public string EstacionRed { get; set; }
    }
}
