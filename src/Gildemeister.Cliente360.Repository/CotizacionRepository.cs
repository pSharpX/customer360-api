using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Persistence;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Schema;

namespace Gildemeister.Cliente360.Repository
{
    public interface ICotizacionRepository : IRepositoryBase<Cotizacion>
    {
        IEnumerable<Cotizacion> GetCustomer(int page, int pageSize, out int pageCount);
        IEnumerable<Cotizacion> Listar(int page, int pageSize, out int pageCount);
        IEnumerable<Cotizacion> Buscar(int tipofiltro, string textofiltro);
        Task<IEnumerable<Cotizacion>> BuscarPorCodigo(string clienteId,string cotizacionId);
        Task<IEnumerable<Cotizacion>> BuscarPorCliente(string clienteId);
        Task Insertar(Dictionary<string, StoredProcedure> parameters);
        Task Actualizar(Dictionary<string, StoredProcedure> parameters);
    }

    public class CotizacionRepository : RepositoryBase<Cotizacion>, ICotizacionRepository
    {
        private IServiceClient serviceClient;
        
        public CotizacionRepository(Cliente360DbContext cliente360DbContext) : base(cliente360DbContext)
        {
        }

        public CotizacionRepository(Cliente360DbContext cliente360DbContext, IServiceClient _serviceClient)
            : base(cliente360DbContext)
        {
            this.serviceClient = _serviceClient;
        }

        public Task Actualizar(Dictionary<string, StoredProcedure> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cotizacion>> BuscarPorCliente(string clienteId)
        {
            IEnumerable<Cotizacion> _cotizaciones;
            HttpResponseMessage response = await serviceClient.GetAsync($"/clientes/{clienteId}/cotizaciones");            
            JObject respuesta = await response.Content.ReadAsJsonAsync<JObject>();
            
            JArray _cotizacionesJson = (JArray)respuesta.GetValue("data");
            _cotizaciones = _cotizacionesJson.Select(c => new Cotizacion {
                IdCotizacion = (int)c["idCotizacion"],                
                Marca = new Marca {
                    CodigoMarca = (c["codigoMarca"] != null) ? (string)c["codigoMarca"] : string.Empty,
                    NombreMarca = (c["nombreMarca"] != null) ? (string)c["nombreMarca"] : string.Empty
                },                                
                Modelo = new Modelo {
                    CodigoModelo = (c["codigoModelo"] != null) ? (string)c["codigoModelo"]: string.Empty,
                    NombreModelo = (c["nombreModelo"] != null) ? (string)c["nombreModelo"]: string.Empty
                },
                NombreColor = (c["nombreColor"] != null) ? (string)c["nombreColor"]: string.Empty,                
                NombreEstado = (c["nombreEstado"] !=  null) ? (string)c["nombreEstado"]: string.Empty,

                NumeroCotizacion = (string)c["numeroCotizacion"],
                FechaRegistro = (DateTime)c["fechaRegistro"],                

                FechaUltimoContacto = (c["fechaUltimoContacto"] != null) ? (string)c["fechaUltimoContacto"]: string.Empty,
                FechaProximoContacto = (c["fechaProximoContacto"] != null) ? (string)c["fechaProximoContacto"]: string.Empty,

                MontoPrecioLista = (c["montoPrecioLista"] != null) ? (decimal)c["montoPrecioLista"]: (decimal?)null,
                MontoPrecioVenta = (c["montoPrecioVenta"] != null) ? (decimal)c["montoPrecioVenta"]: (decimal?)null,
                MontoPrecioCierre = (c["montoPrecioCierre"] != null) ? (decimal)c["montoPrecioCierre"]: (decimal?)null,

                Cantidad = (c["cantidad"] != null) ? (int)c["cantidad"]: (int?)null,
                
                Cliente = new Cliente {
                    NombreCompleto = (string)c["nombreCliente"],
                    NumeroDocumento = (string)c["numeroDocumento"],
                    TipoPersonaNombre = (c["tipoCliente"] != null) ? (string)c["tipoCliente"]: string.Empty
                },
                
                Empleado = new Empleado {
                    NombreCompleto = (string)c["nombreEmpleado"]
                },

                NombreJefeVentas = (string)c["nombreJefeVentas"],

                NombreComercial = (string)c["nombreComercial"],
                NombreCarroceria = (string)c["nombreCarroceria"],

                CodigoFamilia = (string)c["codigoFamilia"],
                NombreFamilia = (string)c["nombreFamilia"],
                AñoModelo = (string)c["añoModelo"],
                AñoFabricacion = (string)c["añoFabricacion"],

                NombreFormaPago = (c["nombreFormaPago"] != null) ? (string)c["combreFormaPago"]: string.Empty,
                NombreModoCaptacion = (string)c["nombreModoCaptacion"],

                Observacion = (c["observacion"] != null) ? (string)c["observacion"]: string.Empty,

                PuntoVenta = new PuntoVenta {
                    IdPuntoVenta = (int)c["idPuntoVenta"],
                    NombrePuntoVenta = (string)c["nombrePuntoVenta"],
                },
                
                CanalVenta = new CanalVenta {
                    CodigoCanalVenta = (string)c["codigoCanalVenta"],
                    NombreCanalVenta = (string)c["nombreCanalVenta"],
                },

                CodigoTipoVenta = (string)c["codigoTipoVenta"],
                NombreTipoVenta = (string)c["nombreTipoVenta"],

                FechaUltimoLead = (c["fechaUltimoLead"] != null) ? (DateTime)c["fechaUltimoLead"]: (DateTime?)null,

                Empresa = (c["empresa"] != null) ? (string)c["empresa"]: string.Empty,
                
                Ubica = new Ubicacion {
                    IdUbica = (int)c["idUbica"]                    
                }
            }).ToList();
            
            return _cotizaciones;
        }

        public Task<IEnumerable<Cotizacion>> BuscarPorCodigo(string clienteId, string cotizacionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> GetCustomer(int page, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task Insertar(Dictionary<string, StoredProcedure> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> Listar(int page, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}
