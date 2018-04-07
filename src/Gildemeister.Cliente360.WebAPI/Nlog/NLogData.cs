using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public class NLogData
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string RequestUrl { get; set; }

        public string Browser { get; set; }

        public string Method { get; set; }

        public string ErrorMessage { get; set; }

        public string Email { get; set; }

        public string RemoteIPAddress { get; set; }

        public string ServerIPAddress { get; set; }
    }
}
