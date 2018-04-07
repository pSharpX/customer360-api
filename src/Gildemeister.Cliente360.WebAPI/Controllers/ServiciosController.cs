using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gildemeister.Cliente360.Application.Interfaces;
using NLog;
using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Common;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Clientes/{clienteId}/Servicios/")]
    //[Authorize]
    public class ServiciosController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());
        private IServicioService servicioService;

        public ServiciosController(IServicioService _servicioService)
        {
            this.servicioService = _servicioService;
        }

        // GET: api/Clientes/{clienteId}/Servicios
        [HttpGet]
        public async Task<ServiceResult> Get([FromRoute]string clienteId)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<ServicioDTO> servicios = new List<ServicioDTO>();
                servicios = await servicioService.BuscarPorClienteAsync(clienteId);
                var jsonData = from c in servicios.AsEnumerable()
                               select new
                               {
                                   c.CodigoEmpresa,
                                   c.NombreEmpresa,
                                   c.NumeroDocumento,
                                   c.ClienteNombre,
                                   c.CodigoSucursal,
                                   c.NombreSucursal,
                                   c.Seccion,
                                   c.CodigoOT,
                                   c.TipoOT,
                                   c.FechaEmision,
                                   c.FechaApertura,
                                   c.Periodo,
                                   c.Año,
                                   c.Mes,
                                   c.Dia,
                                   c.KilometrosRecepcion,
                                   c.NombreAsesor,
                                   c.NombreEstado,
                                   c.Placa,
                                   c.Vin,
                                   c.NombreMarca,
                                   c.NombreModelo,
                                   c.MarcaGerencial,
                                   c.Cia,
                                   c.Comentario,
                                   c.Facturable,
                                   c.NoFacturable,
                                   c.Negocio,
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

        // GET: api/Clientes/{clienteId}/Servicios/5
        [HttpGet]
        [Route("{id}", Name = "Servicios")]
        public async Task<ServiceResult> Get([FromRoute]string clienteId, [FromQuery]string id)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                ServicioDTO servicio = new ServicioDTO();
                servicio = await servicioService.BuscarPorCodigoAsync(clienteId, id);
                service.Data = servicio;
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
                ArchivoReporte workbook = await servicioService.ExportarAsync(clienteId);
                _file = File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            return _file;
        }

        // POST: api/Servicios
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Servicios/5
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
