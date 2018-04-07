using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    
    public partial class TablaGeneral
    {
        [Key]
        [Column("nid_tabla_gen")]
        public int IdTablaGeneral { get; set; }

        [Column("no_tabla")]
        public string Codigo { get; set; }

        [Column("de_tabla")]
        public string Descripcion { get; set; }

        [Column("fl_tipo_tabla")]
        public string Tipo { get; set; }

        [Column("no_pagina_html")]
        public string Pagina { get; set; }

        [Column("fl_inactivo")]
        public string Estado { get; set; }

        [Column("fe_crea")]
        public DateTime? FechaCreacion { get; set; }

        [Column("co_usuario_crea")]
        public string UsuarioCreacion { get; set; }

        [Column("fe_cambio")]
        public DateTime? FechaModificacion { get; set; }


        [Column("co_usuario_cambio")]
        public string UsuarioModificacion { get; set; }

        [Column("fl_tipo_listado")]
        public string TipoListado { get; set; }
    }
}
