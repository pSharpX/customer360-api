using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Ubigeos")]
    [Authorize]
    public class UbigeosController : Controller
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());

        private IUbigeoService ubigeoService;
        private IPaisService paisService;

        public UbigeosController(IUbigeoService ubigeoService,
            IPaisService paisService)
        {
            this.ubigeoService = ubigeoService;
            this.paisService = paisService;
        }


        [HttpGet]
        [Route("ListarPais", Name = "Pais")]
        public ServiceResult ListarPais()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<PaisDTO> lista = paisService.GetAll();

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
        [Route("ListarDepartamento", Name = "Departamento")]
        public ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<UbigeoDTO> lista = ubigeoService.ListarDepartamento();

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
        [Route("ListarProvincia/{departamento}", Name = "Provincia")]
        public ServiceResult Get(string departamento)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<UbigeoDTO> lista = ubigeoService.ListarProvincia(departamento);

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
        [Route("ListarDistrito/{departamento}/{provincia}", Name = "Distrito")]
        public ServiceResult Get(string departamento, string provincia)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<UbigeoDTO> lista = ubigeoService.ListarDistrito(departamento, provincia);

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
