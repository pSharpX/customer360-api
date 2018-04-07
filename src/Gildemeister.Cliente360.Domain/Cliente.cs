using Gildemeister.Cliente360.Domain.SGAPROD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gildemeister.Cliente360.Domain
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }

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

        public bool VentaVehiculo { get; set; }

        public bool Servicio { get; set; }

        public bool VentaRepuesto { get; set; }

        public DateTime FechaUltimoContacto { get; set; }

        public string CodigoUltimoContacto { get; set; }

        public int IdContacto { get; set; }

        public string SexoContacto { get; set; }

        public string SexoContactoNombre { get; set; }

        public string ContactoDocumento { get; set; }

        public string TipoDocumentoContacto { get; set; }

        public string NombreContacto { get; set; }

        public string ApellidoPaternoContacto { get; set; }

        public string ApellidoMaternoContacto { get; set; }

        public string TelefonoContacto { get; set; }

        public string CelularContacto { get; set; }

        public string CorreoContacto { get; set; }

        public IEnumerable<Marca> ListMarcas { get; set; }

        public IEnumerable<Modelo> ListModelos { get; set; }

        public IEnumerable<int> ListAnioModelo { get; set; }

        public IEnumerable<int> ListAnioFabricacion { get; set; }

        public IEnumerable<PuntoVenta> ListPuntoVenta { get; set; }

        public IEnumerable<PersonaAsesor> ListAsesorComercial { get; set; }

        public IEnumerable<PersonaAsesor> ListAsesorServicio { get; set; }

        public IEnumerable<Ubigeo> ListDepartamento { get; set; }

        public IEnumerable<PersonaAsesor> ListAsesorVendedor { get; set; }
    }
}
