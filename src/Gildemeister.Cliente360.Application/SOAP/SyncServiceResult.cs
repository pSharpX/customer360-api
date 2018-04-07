using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Application
{
    public class SyncServiceResult
    {
        public string Key { get; set; }

        public string RequestType { get; set; }

        public string IdTipoProceso { get; set; }

        public dynamic Data { get; set; }
    }
}
