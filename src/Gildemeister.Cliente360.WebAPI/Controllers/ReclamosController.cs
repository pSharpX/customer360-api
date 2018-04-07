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
    [Route("api/Reclamos")]
    //[Authorize]
    public class ReclamosController : Controller
    {
        // GET: api/Reclamos
        [HttpGet]
        [Route("ListarReclamos", Name = "Reclamos")]
        public ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // GET: api/Reclamos/5
        [HttpGet]
        [Route("ListarReclamos/{id}", Name = "Reclamo")]
        public ServiceResult Get(int id)
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // POST: api/Reclamos
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Reclamos/5
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
