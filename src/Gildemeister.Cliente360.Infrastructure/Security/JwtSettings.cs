using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Infrastructure.Security
{
    public class JwtSettings
    {
        public const string JwtKey = "secretkey_hundr3d2017..*";
        public const string JwtIssuer = "hundrIssuer";
        public const int JwtExpireDays = 30;
    }
}
