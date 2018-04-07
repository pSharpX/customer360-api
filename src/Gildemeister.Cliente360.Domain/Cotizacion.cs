using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    public partial class Cotizacion
    {
        public int IdCotizacion { get; set; }

        //public string CodigoNegocio { get; set; }
        //public string NombreNegocio { get; set; }
        //public Negocio Negocio { get; set; }

        public string CodigoMarca { get; set; }
        public string NombreMarca { get; set; }
        //public Marca Marca { get; set; }

        public string CodigoModelo { get; set; }
        public string NombreModelo { get; set; }
        //public Modelo Modelo { get; set; }

        //public int CodigoColor { get; set; }
        public string NombreColor { get; set; }

        //public string CodigoEstado { get; set; }
        public string NombreEstado { get; set; }

        public string NumeroCotizacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        //public DateTime? FechaVencimiento { get; set; }

        public string FechaUltimoContacto { get; set; }
        public string FechaProximoContacto { get; set; }

        public decimal? MontoPrecioLista { get; set; }
        public decimal? MontoPrecioVenta { get; set; }
        public decimal? MontoPrecioCierre { get; set; }

        public int? Cantidad { get; set; }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TipoCliente { get; set; }
        public string NumeroDocumento { get; set; }
        //public Cliente Cliente { get; set; }

        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        //public Empleado Empleado { get; set; }

        public string NombreJefeVentas { get; set; }

        public string NombreComercial { get; set; }
        public string NombreCarroceria { get; set; }

        public string CodigoFamilia { get; set; }
        public string NombreFamilia { get; set; }
        public string AñoModelo { get; set; }
        public string AñoFabricacion { get; set; }

        public string NombreFormaPago { get; set; }
        public string NombreModoCaptacion { get; set; }

        public string Observacion { get; set; }

        //public DateTime? FechaEstado { get; set; }

        public int IdPuntoVenta { get; set; }
        public string NombrePuntoVenta { get; set; }
        //public PuntoVenta PuntoVenta { get; set; }

        public string CodigoCanalVenta { get; set; }
        public string NombreCanalVenta { get; set; }
        //public CanalVenta CanalVenta { get; set; }

        public string CodigoTipoVenta { get; set; }
        public string NombreTipoVenta { get; set; }

        public DateTime? FechaUltimoLead { get; set; }

        public string Empresa { get; set; }

        public int IdUbica { get; set; }
        public string NombreUbica { get; set; }
        //public Ubicacion Ubica { get; set; }

        //public int IdMoneda { get; set; }
        //public string NombreMoneda { get; set; }
        //public Moneda Moneda { get; set; }

        public int IdContacto { get; set; }
        //public Contacto Contacto { get; set; }
    }
}
