using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Infrastructure.Security;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.WebAPI.Helper;
using Gildemeister.Cliente360.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Seguridad")]
    //[EnableCors("cors")]
    public class SeguridadController : BaseController
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());

        private ISeguridadService seguridadService;
        private IUsuarioService usuarioService;
        private IHttpContextAccessor httpContext;
        private ITokenAccesoService tokenAccesoService;
        private IApplicacionLlaveService applicacionLlaveService;

        public SeguridadController(ISeguridadService seguridadService,
            IUsuarioService usuarioService,
            ITokenAccesoService tokenAccesoService,
            IHttpContextAccessor httpContext,
            IApplicacionLlaveService applicacionLlaveService)
            : base(httpContext)
        {
            this.seguridadService = seguridadService;
            this.usuarioService = usuarioService;
            this.tokenAccesoService = tokenAccesoService;
            this.httpContext = httpContext;
            this.applicacionLlaveService = applicacionLlaveService;
        }

        [HttpGet]
        [Route("AutenticacionUsuario/{usuario}", Name = "AutenticationUser")]
        [AllowAnonymous]
        public ServiceResult Get(string usuario)
        {
            ServiceResult service = new ServiceResult();

            try
            {
                IEnumerable<SeguridadDTO> seguridadList = seguridadService
                    .AutenticationUser(usuario);

                var jsonData = from x in seguridadList.AsEnumerable()
                               select x;

                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }

            return service;
        }

        [HttpGet]
        [Route("GenerarToken/{usuario}", Name = "GetToken")]
        [AllowAnonymous]
        public JwtAuthToken GetToken(string usuario)
        {
            return new Authentication().GenerateJwtToken(usuario);
        }

        [HttpGet]
        [Route("RefrescarToken/{refreshToken}", Name = "RefreshToken")]
        [AllowAnonymous]
        public JwtAuthToken RefreshToken(string refreshToken)
        {
            return new Authentication().RefreshJwtToken(refreshToken);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UsuarioLogin")]
        public ActionResult UsuarioLogin([FromBody]ApplicationKey aplicacion)
        {
            ServiceResult service = new ServiceResult();

            try
            {
                if (!string.IsNullOrEmpty(aplicacion.key))
                {
                    var aplicacionKey = StringCipher.Decrypt(aplicacion.key);
                    string[] keyUser = aplicacionKey.Split(':');
                    string usuario = keyUser.FirstOrDefault();
                    string llaveApp = keyUser.LastOrDefault();
                    ApplicacionLlaveDTO applicacionLlave = applicacionLlaveService.LlaveApplicacion(llaveApp);

                    if (applicacionLlave != null)
                    {
                        UsuarioDTO usuarioDTO = usuarioService.BuscarUsuario(usuario);

                        if (usuarioDTO != null)
                        {
                            JwtAuthToken jwtAuthToken = new Authentication()
                                .GenerateJwtToken(usuarioDTO.NombreUsuario);

                            UsuarioDTO usuarioModel = usuarioDTO;
                            TokenAccesoDTO tokenAcceso = new TokenAccesoDTO
                            {
                                AccesToken = jwtAuthToken.AccessToken,
                                Usuario = usuarioModel.NombreUsuario,
                                UsuarioId = usuarioModel.UsuarioId,
                                PerfilId = usuarioModel.PerfilId,
                                Validado = true,
                                FechaRegistro = DateTime.Now,
                                Activo = true,
                                FechaCreacion = DateTime.Now,
                                FechaCambio = DateTime.Now,
                                UsuarioRed = string.Empty,
                                EstacionRed = string.Empty
                            };

                            tokenAccesoService.Insert(tokenAcceso);

                            var jsonData = new
                            {
                                usuario = usuarioModel.NombreUsuario,
                                token = jwtAuthToken,
                                url = applicacionLlave.Url,
                                pagina = usuarioDTO.Pagina
                            };
                            service.Data = jsonData;
                            return Json(service);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                service.Success = false;
                service.Error = ex.Message;
                _logger.LogError(ex);
            }
            return Json(service);
        }



        [Authorize]
        [HttpGet]
        [Route("ListarRoles/{usuario}", Name = "ListarRoles")]
        public ServiceResult ListarRoles(string usuario)
        {
            ServiceResult service = new ServiceResult();

            try
            {

                IEnumerable<UsuarioDTO> listarRole = usuarioService.UsuarioRol(usuario);

                var jsonData = from f in listarRole.AsEnumerable()
                               select new
                               {
                                   f.UsuarioId,
                                   f.Nombre,
                                   Usuario = f.NombreUsuario,
                                   f.Menu,
                                   f.Pagina,
                                   f.Perfil
                               };

                service.Data = jsonData;
            }
            catch (Exception ex)
            {
                service.Success = false;
                service.Errors(ex);
                service.Message = ex.Message;
            }
            return service;
        }


    }
}
