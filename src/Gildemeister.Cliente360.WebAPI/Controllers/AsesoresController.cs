using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gildemeister.Cliente360.Application.Interfaces;
using NLog;
using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Transport.SGAPROD;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Asesores")]
    public class AsesoresController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());

        private IAsesorService asesorService;
        
        public AsesoresController(IAsesorService asesorService)            
        {            
            this.asesorService = asesorService;            
        }

        // GET: api/Asesores
        [HttpGet]
        [Route("ListarAsesorComercial", Name = "AsesoresComerciales")]
        public ServiceResult ListarAsesoresComerciales()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<PersonaAsesorDTO> lista = asesorService.ListarAsesorComercial();

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
        [Route("ListarAsesorComercial/{puntoVenta}", Name = "AsesoresComercialesPorPuntoVenta")]
        public ServiceResult ListarAsesoresComercialesPorPuntoVenta(int puntoVenta)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<PersonaAsesorDTO> lista = asesorService.ListarAsesorComercialPorPuntoVenta(puntoVenta);

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
        [Route("ListarAsesorVenta", Name = "AsesoresVenta")]
        public ServiceResult ListarAsesoresServicios()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<PersonaAsesorDTO> lista = asesorService.ListarAsesorVendedor();

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
        [Route("ListarAsesorServicio", Name = "AsesoresServicio")]
        public ServiceResult ListarAsesoresVendedor()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<PersonaAsesorDTO> lista = asesorService.ListarAsesorServicio();

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
        
        // POST: api/Asesores
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Asesores/5
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
