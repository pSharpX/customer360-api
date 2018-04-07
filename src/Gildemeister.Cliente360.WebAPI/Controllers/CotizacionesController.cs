using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gildemeister.Cliente360.Application;
using NLog;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Common;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Clientes/{clienteId}/Cotizaciones")]
    //[Authorize]
    public class CotizacionesController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());
        private ICotizacionService cotizacionService;
        
        public CotizacionesController(ICotizacionService cotizacionService)
        {
            this.cotizacionService = cotizacionService;            
        }

        // GET: api/Clientes/{clienteId}/Cotizaciones
        [HttpGet]
        //[Route()]
        public async Task<ServiceResult> Get([FromRoute]string clienteId)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<CotizacionDTO> cotizacion = new List<CotizacionDTO>();
                cotizacion = await cotizacionService.BuscarPorClienteAsync(clienteId);
                var jsonData = from c in cotizacion.AsEnumerable()
                               select new {
                                   c.IdCotizacion,                                         
                                   c.CodigoMarca,
                                   c.NombreMarca,        
                                   c.CodigoModelo,
                                   c.NombreModelo,        
                                   c.NombreColor,                                   
                                   c.NombreEstado,
                                   c.AñoModelo,
                                   c.AñoFabricacion,
                                   c.CodigoFamilia,
                                   c.NombreFamilia,
                                   c.NombreComercial,
                                   c.NombreCarroceria,
                                   c.NombreCliente,
                                   c.NumeroDocumento,
                                   c.TipoCliente,
                                   c.NombreEmpleado,
                                   c.NombreJefeVentas,
                                   c.NumeroCotizacion,
                                   c.FechaRegistro,                                   
                                   c.FechaUltimoContacto,
                                   c.MontoPrecioVenta,
                                   c.MontoPrecioCierre,
                                   c.Cantidad,
                                   c.CodigoTipoVenta,
                                   c.NombreTipoVenta,
                                   c.Observacion,                                   
                                   c.IdPuntoVenta,
                                   c.NombrePuntoVenta,
                                   c.CodigoCanalVenta,
                                   c.NombreCanalVenta,        
                                   c.IdUbica,
                                   c.NombreUbica                                                                   
                               };

                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        //GET: api/Clientes/{clienteId}/Cotizaciones/5
        [HttpGet]
        [Route("{id}", Name = "Cotizaciones")]
        public async Task<ServiceResult> Get([FromRoute]string clienteId, [FromRoute]string id)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                CotizacionDTO cotizacion = await cotizacionService.BuscarPorCodigoAsync(clienteId, id);
                var jsonData = cotizacion;
                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        [HttpGet]
        [Route("exportar/{format=excel}")]
        public async Task<ActionResult> ExportAs([FromRoute]string clienteId, [FromRoute]string format)
        {
            FileContentResult _file = null;
            try
            {
                ArchivoReporte workbook = await cotizacionService.ExportarAsync(clienteId);
                _file = File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            return _file;
        }

        // POST: api/Clientes/{clienteId}/Cotizaciones
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clientes/{clienteId}/Cotizaciones/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
