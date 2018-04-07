using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Infrastructure.Security
{
    public class JwtAuthToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiresOn { get; set; }
    }
}

