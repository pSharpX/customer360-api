using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gildemeister.Cliente360.Application;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Operaciones")]
    //[Authorize]
    public class OperacionesController : Controller
    {
        // GET: api/Operaciones
        [HttpGet]
        [Route("ListarOperaciones", Name = "Operaciones")]
        public  ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // GET: api/Operaciones/5

        [HttpGet]
        [Route("ListarOperaciones/{id}", Name = "Operacion")]
        public ServiceResult Get(int id)
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // POST: api/Operaciones
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Operaciones/5
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
