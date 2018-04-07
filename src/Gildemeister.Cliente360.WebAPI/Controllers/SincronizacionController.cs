using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Gildemeister.Cliente360.Application;
using Microsoft.AspNetCore.Authorization;
using NLog;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Sincronizacion")]
    public class SincronizacionController : BaseController
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());
        private IHttpContextAccessor httpContext;
        private IServiceClient serviceClient;
        private ISincronizarService sincronizarService;

        public SincronizacionController(
            IServiceClient serviceClient,
            IHttpContextAccessor httpContext,
            ISincronizarService sincronizarService
            ) : base(httpContext)
        {
            this.httpContext = httpContext;
            this.serviceClient = serviceClient;
            this.sincronizarService = sincronizarService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("ExportarSincronizacion/{fechainicio}/{fechafin}/{codestadosyndet}")]
        public ActionResult Export(string fechainicio, string fechafin, string codestadosyndet)
        {
            fechainicio = (fechainicio == "-" ? null : fechainicio);
            fechafin = (fechafin == "-" ? null : fechafin);
            codestadosyndet = (codestadosyndet == "00" ? null : codestadosyndet);

            var lista = (List<VisorSincronizacionDTO>)ObtenerDatosSincronizacion((string)fechainicio, (string)fechafin, (string)codestadosyndet).Result;

            var workbook = sincronizarService.ExportarSincronizacion(lista);

            return File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
        }

        private async Task<List<VisorSincronizacionDTO>> ObtenerDatosSincronizacion(string fechainicio, string fechafin, string codestadosyndet)
        {
            string param = string.Concat(fechainicio, "|", fechafin, "|", codestadosyndet);

            var response = await serviceClient.GetAsync("/sincronizacion/visorsincronizacion/buscar", param);
            var respuesta = await response.Content.ReadAsJsonAsync<SyncResponse>();

            var cadena = ((object)respuesta.data).ToString();

            var lista = JsonConvert.DeserializeObject<List<VisorSincronizacionDTO>>(cadena);

            return lista;
        }

        [HttpPut]
        [Route("Reprocesar/{lista}")]
        public async Task<ServiceResult> Reprocesar(string lista)
        {
            ServiceResult service = new ServiceResult();

            try
            {
                SyncServiceResult synService = new SyncServiceResult();
                synService.Key = Guid.NewGuid().ToString();
                synService.RequestType = "1";
                synService.IdTipoProceso = "1";
                synService.Data = lista;

                var response = await serviceClient.PostAsync("/sincronizacion/cliente/reprocesar", synService);
                var respuesta = await response.Content.ReadAsJsonAsync<SyncResponse>();

            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }

            return service;
        }

        [HttpGet]
        [Route("BuscarSincronizacion/{fechainicio}/{fechafin}/{codestadosyndet}")]
        public ServiceResult Get(string fechainicio, string fechafin, string codestadosyndet)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                fechainicio = (fechainicio == "-" ? null : fechainicio);
                fechafin = (fechafin == "-" ? null : fechafin);
                codestadosyndet = (codestadosyndet == "00" ? null : codestadosyndet);

                var lista = (List<VisorSincronizacionDTO>)ObtenerDatosSincronizacion(fechainicio, fechafin, codestadosyndet).Result;

                var jsonData = from x in lista.AsEnumerable()
                               select x;

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
        [Route("ListarEstadoSynDetalle")]
        public async Task<ServiceResult> Get()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                var response = await serviceClient.GetAsync("/sincronizacion/listaestadosincronizaciondetalle");
                var respuesta = await response.Content.ReadAsJsonAsync<SyncResponse>();

                var cadena = ((object)respuesta.data).ToString();

                var lista = JsonConvert.DeserializeObject<List<EstadoSynDetalleDTO>>(cadena);

                var jsonData = from x in lista.AsEnumerable()
                               select x;

                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }
    }
}