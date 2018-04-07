using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public class SyncResponse
    {
        public bool success { get; set; }

        public string message { get; set; }

        public dynamic data { get; set; }

        public string error { get; set; }
    }
}
