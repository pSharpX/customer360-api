using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Transport
{
    public class ServicioDTO
    {
        public string CodigoEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string NumeroDocumento { get; set; }
        public string ClienteNombre { get; set; }
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Seccion { get; set; }
        public string CodigoOT { get; set; }
        public string TipoOT { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string FechaApertura { get; set; }
        public string Periodo { get; set; }
        public int? Año { get; set; }
        public int? Mes { get; set; }
        public int? Dia { get; set; }
        public int? KilometrosRecepcion { get; set; }
        public string NombreAsesor { get; set; }
        public string NombreEstado { get; set; }
        public string Placa { get; set; }
        public string Vin { get; set; }

        public string NombreMarca { get; set; }
        public string NombreModelo { get; set; }
        public string MarcaGerencial { get; set; }

        public string Cia { get; set; }
        public string Comentario { get; set; }
        public string Facturable { get; set; }
        public string NoFacturable { get; set; }
        public string Negocio { get; set; }
    }
}
