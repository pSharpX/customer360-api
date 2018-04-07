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
    [Route("api/Clientes/{clienteId}/TestsDrive")]
    //[Authorize]
    public class TestsDriveController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());
        private ITestDriveService testdriveService;

        public TestsDriveController(ITestDriveService testdriveService)
        {
            this.testdriveService = testdriveService;
        }

        // GET: api/Clientes/{clienteId}/TestsDrive
        [HttpGet]
        public async Task<ServiceResult> Get([FromRoute]string clienteId)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<TestDriveDTO> testsdrive = new List<TestDriveDTO>();
                testsdrive = await testdriveService.BuscarPorClienteAsync(clienteId);
                var jsonData = from c in testsdrive.AsEnumerable()
                               select new
                               {
                                   c.IdSolicitud,
                                   c.Periodo,
                                   c.NumeroSolicitud,
                                   c.NombreMarca,
                                   c.NombreModelo,
                                   c.CodigoEstado,
                                   c.NombreEstado,
                                   c.CodigoFamiliaCorto,
                                   c.VIN,
                                   c.NombreCliente,
                                   c.NumeroDocumento,
                                   c.RUCCliente,
                                   c.RazonSocial,
                                   c.NombreAsesor,
                                   c.AsesorPuntoVenta,
                                   c.Cantidad,
                                   c.FechaPruebaManejo,
                                   c.FechaInicio,
                                   c.FechaFin,
                                   c.HoraInicio,
                                   c.HoraFin,
                                   c.NombreCanalVenta,
                                   c.NombreUbicacion
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

        // GET: api/Clientes/{clienteId}/TestsDrive/5
        [HttpGet]
        [Route("{id}", Name = "TestsDrive")]
        public async Task<ServiceResult> Get([FromRoute]string clienteId, [FromRoute]string id)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                TestDriveDTO testdrive = new TestDriveDTO();
                testdrive = await testdriveService.BuscarPorSolicitudAsync(clienteId, id);
                service.Data = testdrive;
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
                ArchivoReporte workbook = await testdriveService.ExportarAsync(clienteId);
                _file = File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            return _file;
        }

        // POST: api/Clientes/{clienteId}/TestsDrive
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clientes/{clienteId}/TestsDrive/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clientes/{clienteId}/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
