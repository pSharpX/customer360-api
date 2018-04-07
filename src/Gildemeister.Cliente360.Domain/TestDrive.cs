using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    public class TestDrive
    {
        public int IdSolicitud { get; set; }
        public string Periodo { get; set; }
        public int Cantidad { get; set; }
        public string NumeroSolicitud { get; set; }
        public string NombreMarca { get; set; }
        public string NombreModelo { get; set; }
        public string CodigoFamiliaCorto { get; set; }
        public string VIN { get; set; }
        public string NombreUbicacion { get; set; }
        public string NombreCanalVenta { get; set; }
        public string AsesorPuntoVenta { get; set; }
        public string NombreAsesor { get; set; }

        public string NumeroDocumento { get; set; }
        public string NombreCliente { get; set; }
        public string RUCCliente { get; set; }
        public string RazonSocial { get; set; }

        public string CodigoEstado { get; set; }
        public string NombreEstado { get; set; }
        public DateTime? FechaPruebaManejo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
    }
}
