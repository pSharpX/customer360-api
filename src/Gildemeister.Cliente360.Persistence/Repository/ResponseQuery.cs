using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class ResponseQuery
    {
        public string message { get; set; }
        public dynamic data { get; set; }
        public bool status { get; set; }
        public ResponseQuery()
        {
            this.status = true;
        }
    }
}
