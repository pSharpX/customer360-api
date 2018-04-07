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
    [Route("api/Promociones")]
    //[Authorize]
    public class PromocionesController : Controller
    {
        // GET: api/Promociones
        [HttpGet]
        [Route("ListarPromociones", Name = "Promociones")]
        public ServiceResult Get()
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // GET: api/Promociones/5
        [HttpGet]
        [Route("ListarPromociones/{id}", Name = "Promocion")]
        public ServiceResult Get(int id)
        {
            ServiceResult service = new ServiceResult();

            return service;
        }

        // POST: api/Promociones
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Promociones/5
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
