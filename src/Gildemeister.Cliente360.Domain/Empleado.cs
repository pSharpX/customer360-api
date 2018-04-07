using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Domain
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroDocumentoOriginal { get; set; }
        public string Ruc { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RazonSocial { get; set; }
        public string TipoPersona { get; set; }
        public string TipoPersonaNombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string TipoDocumento { get; set; }
        public string Asesor { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string CodigoEstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        public string CodigoGenero { get; set; }
        public string Genero { get; set; }
        public int IdDireccion { get; set; }
        public string Direccion { get; set; }
        public string CodigoDistrito { get; set; }
        public string Distrito { get; set; }
        public string CodigoProvincia { get; set; }
        public string Provincia { get; set; }
        public string CodigoDepartamento { get; set; }
        public string Departamento { get; set; }
        public string CodigoPostal { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string CodigoPais { get; set; }
    }
}
