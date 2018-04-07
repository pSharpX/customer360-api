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
    [Route("api/Citas")]
    //[Authorize]
    public class CitasController : Controller
    {
        private IUbigeoService ubigeoService;

        public CitasController(IUbigeoService ubigeoService)
        {
            this.ubigeoService = ubigeoService;
        }

        // GET: api/Citas
        [HttpGet]
        [Route("ListarCitas", Name = "ListarCitas")]
        public ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();
            try
            {

                IEnumerable<UbigeoDTO> lista = ubigeoService.GetAll();

                service.Data = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return service;
        }

        //GET: api/Citas/5
        [HttpGet]
        [Route("ListarCitas/{id}", Name = "Citas")]
        public ServiceResult Get(int id)
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // POST: api/Citas
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/Citas/5
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
