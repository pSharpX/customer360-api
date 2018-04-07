using Gildemeister.Cliente360.WebAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Gildemeister.Cliente360.WebAPI
{
    public class BaseController : Controller
    {
        private StringValues authorizationToken;
        private readonly IHttpContextAccessor httpContext;

        public BaseController(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public AuditoriaDTO getUserAuthen()
        {
            AuditoriaDTO auditoria = new AuditoriaDTO();

            string accesToken = this.GetAccessToken();
            string ipAddress = this.GetUserIP();

            var tokenHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(accesToken))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(accesToken);
                var tokenClaims = jwtSecurityToken.Claims;

                auditoria.UsuarioCambio = tokenClaims.First().Value;
                auditoria.FechaCambio = DateTime.Now;
                auditoria.UsuarioRed = tokenClaims.First().Value;
                auditoria.EstacionRed = this.GetUserIP();
            }
            else
            {
                auditoria.UsuarioCambio = "test";
                auditoria.FechaCambio = DateTime.Now;
                auditoria.UsuarioRed = "test";
                auditoria.EstacionRed = this.GetUserIP();
            }

            return auditoria;
        }

        private string GetAccessToken()
        {
            var authorizationHeaders = httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationToken);
            if (authorizationHeaders)
            {
                var header = authorizationToken.First().Trim();
                if (header.StartsWith("Bearer"))
                {
                    var value = header.Substring("Bearer".Length).Trim();
                    if (value != null && value.Length > 0)
                    {
                        return value;
                    }
                }
            }
            return string.Empty;
        }

        private string GetUserIP()
        {
            var remoteIpAddress = httpContext.HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }



    }
}
