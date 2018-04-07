using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Transport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Logger")]
    //[Authorize]
    public class LoggerController : Controller
    {

        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());

        // GET: api/Logger
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Logger/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Logger
        [HttpPost]
        public void Post([FromBody] LogDTO log)
        {
            if (log.Level == "Error") {
                Exception ex = new ApplicationException(log.Message + ". " + log.StackTrace);
                _logger.LogError(ex);
            }
            if (log.Level == "Trace")
            {
                _logger.LogTrace(log.Message);
            }
            
        }
        
        // PUT: api/Logger/5
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
