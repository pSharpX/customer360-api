using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Transport;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/TablaGenerales")]
    //[Authorize]
    public class TablaGeneralesController : Controller
    {

        private ITablaDetalleService tablaDetalleService;

        public TablaGeneralesController(ITablaDetalleService tablaDetalleService)
        {
            this.tablaDetalleService = tablaDetalleService;
        }

      
        [HttpGet]
        [Route("ListarEstadoCivil", Name = "EstadoCivil")]
        public ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<TablaDetalleDTO> lista = tablaDetalleService.ListarEstadoCivil();

                var jsonData = from x in lista.AsEnumerable()
                               select x;

                service.Data = jsonData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return service;
        }

        [HttpGet]
        [Route("ListarGenero", Name = "Genero")]
        public ServiceResult Genero()
        {
            ServiceResult service = new ServiceResult();
            try
            {
                IEnumerable<TablaDetalleDTO> lista = tablaDetalleService.ListarGenero();

                var jsonData = from x in lista.AsEnumerable()
                               select x;

                service.Data = jsonData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return service;
        }



    }
}
