using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string SecondayDomain { get; set; }

        public int SecondaryPort { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public string CcEmail { get; set; }
    }
}
