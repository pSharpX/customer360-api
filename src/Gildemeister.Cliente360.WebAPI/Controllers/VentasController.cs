using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Common;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Clientes/{clienteId}/Ventas")]
    //[Authorize]
    public class VentasController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());
        private IVentaService ventaService;

        public VentasController(IVentaService _ventaService)
        {
            this.ventaService = _ventaService;
        }

        // GET: api/Clientes/{clienteId}/Ventas
        [HttpGet]
        public async Task<ServiceResult> Get([FromRoute]string clienteId)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<VentaDTO> ventas = new List<VentaDTO>();
                ventas = await ventaService.BuscarPorClienteAsync(clienteId);
                var jsonData = ventas;
                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        // GET: api/Clientes/{clienteId}/Ventas/5
        [HttpGet]
        [Route("{id}", Name = "Ventas")]
        public async Task<ServiceResult> Get([FromRoute]string clienteId, [FromRoute]string id)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                VentaDTO venta = new VentaDTO();
                venta = await ventaService.BuscarPorCodigoAsync(clienteId, id);
                service.Data = venta;
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
        public async Task<ActionResult> ExportAs([FromRoute]string clienteId, [FromQuery]string format)
        {
            FileContentResult _file = null;
            try
            {
                ArchivoReporte workbook = await ventaService.ExportarAsync(clienteId);
                _file = File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            return _file;
        }

        // POST: api/Ventas
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Ventas/5
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
