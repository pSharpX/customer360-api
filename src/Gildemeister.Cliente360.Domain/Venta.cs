using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    public class Venta
    {
        public string FechaFacturacion { get; set; }
        public string NombreColor { get; set; }

        public string NumeroNotaPedido { get; set; }
        public string VIN { get; set; }
        public string NombreMarca { get; set; }
        public string CodigoFamiliaCorto { get; set; }
        public string NombreFamiliaCorto { get; set; }
        public string NombreModelo { get; set; }
        public string NombreComercial { get; set; }
        public string CodigoCanalVenta { get; set; }
        public string NombreCanalVenta { get; set; }
        public string NombrePuntoVenta { get; set; }

        public string NombreAsesor { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public string NombreTipoComprobante { get; set; }
        public string ComprobanteSerie { get; set; }
        public string ComprobanteNumero { get; set; }
        public string CodigoTipoCliente { get; set; }
        public string NombreTipoCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCliente { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string NombreMoneda { get; set; }
        public decimal ImporteVenta { get; set; }
        public int Cantidad { get; set; }
        public string NombreFormaPago { get; set; }
        public string AñoFabricacion { get; set; }
        public string AñoModelo { get; set; }
        public decimal MontoPrecioCierre { get; set; }
        public decimal MontoPrecioLista { get; set; }
        public decimal MontoPrecioVenta { get; set; }
        public string CodigoPedido { get; set; }
        public string NombreTipoVenta { get; set; }

        public string NombreEstadoComercial { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public string EstadoNotaPedido { get; set; }
        public string NombreEmpresa { get; set; }
        public string NumeroPlaca { get; set; }
        public string Periodo { get; set; }
        public string NombreClasificacionVenta { get; set; }
        public string MarcaGerencial { get; set; }
        public string EntregaInmediata { get; set; }
        public DateTime? FechaSolicitudPlaca { get; set; }
        public DateTime? FechaAsignacionPlaca { get; set; }
        public string NombreCita { get; set; }
        public string EstadoPDI { get; set; }
        public DateTime? FechaSolicitudPDI { get; set; }
        public DateTime? FechaFinPDI { get; set; }
        public string EstadoDespacho { get; set; }
        public string LibreWarrant { get; set; }
        public string NombreUbicacion { get; set; }
        public DateTime? FechaReserva { get; set; }
        public DateTime? FechaETA { get; set; }
        public DateTime? FechaArribo { get; set; }
        public string Desaduanada { get; set; }
        public DateTime? FechaLiberacionWarrant { get; set; }
        public DateTime? FechaEstimacionInicioPDI { get; set; }
        public DateTime? FechaEstimacionFinPDI { get; set; }
        public DateTime? FechaSolicitudDespacho { get; set; }
        public DateTime? FechaEstimacionDespacho { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public DateTime? FechaCita { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}
