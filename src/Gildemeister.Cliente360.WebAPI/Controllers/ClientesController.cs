using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.WebAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Clientes")]
    //[Authorize]
    public class ClientesController : BaseController
    {
        private static NLogManager _logger = new NLogManager(LogManager.GetCurrentClassLogger());

        private IClienteService clienteService;
        private IServiceClient serviceClient;
        private IHttpContextAccessor httpContext;

        public ClientesController(IClienteService clienteService,
            IServiceClient serviceClient,
            IHttpContextAccessor httpContext)
            : base(httpContext)
        {
            this.httpContext = httpContext;
            this.clienteService = clienteService;
            this.serviceClient = serviceClient;
        }

        public List<int> CargarAniosFiltros()
        {
            List<int> lista = new List<int>();
            int fechainicio = 1960;
            int fechafin = DateTime.Now.Year + 1;

            for (int i = fechainicio; i <= fechafin; i++)
            {
                lista.Add(i);
            }

            return lista.OrderByDescending(x => x).ToList();
        }

        [HttpGet]
        [Route("CargarListaFilter")]
        public ServiceResult ListaFilterAdvanced()
        {
            ServiceResult service = new ServiceResult();
            ClienteDTO clienteDTO = new ClienteDTO();

            try
            {
                clienteDTO = clienteService.ListarFilterAdvanced();

                IEnumerable<ClienteDTO> listCliente;

                listCliente = new List<ClienteDTO>() { new ClienteDTO() {
                   ListMarcas = clienteDTO.ListMarcas,
                   ListModelos = clienteDTO.ListModelos,
                   ListAnioModelo = CargarAniosFiltros(),
                   ListAnioFabricacion = CargarAniosFiltros(),
                   ListPuntoVenta = clienteDTO.ListPuntoVenta,
                   ListAsesorComercial = clienteDTO.ListAsesorComercial,
                   ListAsesorServicio = clienteDTO.ListAsesorServicio,
                   ListDepartamento = clienteDTO.ListDepartamento,
                   ListAsesorVendedor = clienteDTO.ListAsesorVendedor
                }};

                var jsonData = from x in listCliente.AsEnumerable()
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


        // GET: api/Cliente/1/alejandro
        [Authorize]
        [HttpGet]
        [Route("BuscarClientes/{tipofiltro}/{textofiltro}", Name = "SearchCliente")]
        public ServiceResult Get(int tipofiltro, string textofiltro)
        {
            ServiceResult service = new ServiceResult();

            List<ClienteDTO> list = new List<ClienteDTO>();

            try
            {
                IEnumerable<ClienteDTO> clienteList = clienteService.BuscarCliente(tipofiltro, textofiltro);

                var countReg = clienteList.Count();

                var jsonData = from x in clienteList.AsEnumerable()
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

        [Authorize]
        [HttpGet]
        [Route("BuscarClientesAdvanced/{filtro}", Name = "SearchClienteAdvanced")]
        public ServiceResult Get(string filtro)
        {
            ServiceResult service = new ServiceResult();

            List<ClienteDTO> list = new List<ClienteDTO>();

            string formatfiltro = filtro.Substring(1, filtro.Length - 1);
            var listafiltro = formatfiltro.Split('|');

            var aniofabricacion = (listafiltro[0] == "00" ? null : listafiltro[0]);
            var aniomodelo = (listafiltro[1] == "00" ? null : listafiltro[1]);
            var sucursal = (listafiltro[2] == "00" ? null : listafiltro[2]);
            var asesorcomercial = (listafiltro[3] == "00" ? null : listafiltro[3]);
            var fechaentregaDe = (listafiltro[4] == "-" ? null : listafiltro[4]);
            var fechaentregaHasta = (listafiltro[5] == "-" ? null : listafiltro[5]);
            var asesorservicio = (listafiltro[6] == "00" ? null : listafiltro[6]);
            var fechaservicioDe = (listafiltro[7] == "-" ? null : listafiltro[7]);
            var fechaservicioHasta = (listafiltro[8] == "-" ? null : listafiltro[8]);
            var marca = (listafiltro[9] == "00" ? null : listafiltro[9]);
            var modelo = (listafiltro[10] == "00" ? null : listafiltro[10]);
            var departamento = (listafiltro[11] == "00" ? null : listafiltro[11]);
            var provincia = (listafiltro[12] == "00" ? null : listafiltro[12]);
            var distrito = (listafiltro[13] == "00" ? null : listafiltro[13]);
            var porventavehiculo = (listafiltro[14] == "0" ? false : true);
            var porservicio = (listafiltro[15] == "0" ? false : true);
            var porrepuesto = (listafiltro[16] == "0" ? false : true);
            var asesorVendedor = (listafiltro[17] == "00" ? null : listafiltro[17]);
            var fechaVentaDe = (listafiltro[18] == "-" ? null : listafiltro[18]);
            var fechaVentaHasta = (listafiltro[19] == "-" ? null : listafiltro[19]);
            var vin = (listafiltro[20] == "-" ? null : listafiltro[20]);

            try
            {
                IEnumerable<ClienteDTO> clienteList = clienteService.BuscarClienteAdvanced(aniofabricacion, aniomodelo, sucursal, asesorcomercial,
             fechaentregaDe, fechaentregaHasta, asesorservicio, fechaservicioDe, fechaservicioHasta, marca, modelo,
             departamento, provincia, distrito, porventavehiculo, porservicio, porrepuesto,
             asesorVendedor, fechaVentaDe, fechaVentaHasta, vin);

                var countReg = clienteList.Count();

                var jsonData = from x in clienteList.AsEnumerable()
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
        [Route("ExportarClientes/{tipofiltro}/{textofiltro}", Name = "ExportCliente")]
        public ActionResult Get(string tipofiltro, string textofiltro)
        {
            var workbook = clienteService.ExportarCliente(int.Parse(tipofiltro), textofiltro);

            return File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
        }

        [HttpGet]
        [Route("ExportarClientesAdvanced/{filtro}",
            Name = "ExportarClientesAdvanced")]
        public ActionResult ExportAdvanced(string filtro)
        {
            string formatfiltro = filtro.Substring(1, filtro.Length - 1);
            var listafiltro = formatfiltro.Split('|');

            var aniofabricacion = (listafiltro[0] == "00" ? null : listafiltro[0]);
            var aniomodelo = (listafiltro[1] == "00" ? null : listafiltro[1]);
            var sucursal = (listafiltro[2] == "00" ? null : listafiltro[2]);
            var asesorcomercial = (listafiltro[3] == "00" ? null : listafiltro[3]);
            var fechaentregaDe = (listafiltro[4] == "-" ? null : listafiltro[4]);
            var fechaentregaHasta = (listafiltro[5] == "-" ? null : listafiltro[5]);
            var asesorservicio = (listafiltro[6] == "00" ? null : listafiltro[6]);
            var fechaservicioDe = (listafiltro[7] == "-" ? null : listafiltro[7]);
            var fechaservicioHasta = (listafiltro[8] == "-" ? null : listafiltro[8]);
            var marca = (listafiltro[9] == "00" ? null : listafiltro[9]);
            var modelo = (listafiltro[10] == "00" ? null : listafiltro[10]);
            var departamento = (listafiltro[11] == "00" ? null : listafiltro[11]);
            var provincia = (listafiltro[12] == "00" ? null : listafiltro[12]);
            var distrito = (listafiltro[13] == "00" ? null : listafiltro[13]);
            var porventavehiculo = (listafiltro[14] == "0" ? false : true);
            var porservicio = (listafiltro[15] == "0" ? false : true);
            var porrepuesto = (listafiltro[16] == "0" ? false : true);
            var asesorVendedor = (listafiltro[17] == "00" ? null : listafiltro[17]);
            var fechaVentaDe = (listafiltro[18] == "-" ? null : listafiltro[18]);
            var fechaVentaHasta = (listafiltro[19] == "-" ? null : listafiltro[19]);
            var vin = (listafiltro[20] == "-" ? null : listafiltro[20]);

            var workbook = clienteService.ExportarClientesAdvanced(aniofabricacion, aniomodelo, sucursal, asesorcomercial,
                 fechaentregaDe, fechaentregaHasta, asesorservicio, fechaservicioDe, fechaservicioHasta, marca, modelo,
                 departamento, provincia, distrito, porventavehiculo, porservicio, porrepuesto,
                 asesorVendedor, fechaVentaDe, fechaVentaHasta, vin);

            return File(workbook.ArchivoByte, workbook.ContentType, workbook.NombreArchivo);
        }

        // GET: api/Cliente/5
        [HttpGet]
        [Route("Listar/{id}", Name = "Clientes")]
        [Authorize]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ServiceResult Get(int id)
        {
            ServiceResult service = new ServiceResult();
            try
            {
                //TODO: Listar cliente
                IEnumerable<ClienteDTO> cliente = new List<ClienteDTO>();
                cliente = clienteService.BuscarPorCodigo(id);

                var jsonData = from f in cliente.AsEnumerable()
                               select new
                               {
                                   f.IdCliente,
                                   f.Nombre,
                                   f.NombreCompleto,
                                   f.ApellidoPaterno,
                                   f.ApellidoMaterno,
                                   f.RazonSocial,
                                   f.TipoPersona,
                                   f.TipoDocumento,
                                   f.TipoPersonaNombre,
                                   FechaNacimiento = f.FechaNacimiento == null ? string.Empty : Convert.ToDateTime(f.FechaNacimiento).ToShortDateString(),
                                   f.Correo,
                                   f.NumeroDocumento,
                                   f.NumeroDocumentoOriginal,
                                   f.Ruc,
                                   f.CodigoGenero,
                                   Genero = f.Genero == null ? string.Empty : f.Genero.ToUpper(),
                                   f.Asesor,
                                   f.Telefono,
                                   f.Celular,
                                   f.CodigoEstadoCivil,
                                   EstadoCivil = f.EstadoCivil == null ? string.Empty : f.EstadoCivil.ToUpper(),
                                   f.IdDireccion,
                                   f.Direccion,
                                   f.CodigoDistrito,
                                   f.Distrito,
                                   f.CodigoProvincia,
                                   f.Provincia,
                                   f.CodigoDepartamento,
                                   f.Departamento,
                                   f.CodigoPostal,
                                   f.IdPais,
                                   f.Pais,
                                   f.CodigoPais,
                                   f.IdContacto,
                                   f.SexoContacto,
                                   SexoContactoNombre = f.SexoContactoNombre == null ? string.Empty : f.SexoContactoNombre.ToUpper(),
                                   f.ContactoDocumento,
                                   f.TipoDocumentoContacto,
                                   f.NombreContacto,
                                   f.ApellidoPaternoContacto,
                                   f.ApellidoMaternoContacto,
                                   f.TelefonoContacto,
                                   f.CelularContacto,
                                   f.CorreoContacto
                               };

                service.Data = jsonData.FirstOrDefault();
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        //// POST: api/Cliente
        [HttpPost]
        [Route("Insertar")]
        public async Task<ServiceResult> Post([FromBody]ClienteDTO cliente)
        {
            ServiceResult service = new ServiceResult();
            Dictionary<string, StoredProcedure> parameters = new Dictionary<string, StoredProcedure>();
            try
            {
                parameters.Add("nid_persona ", new StoredProcedure() { Value = 5 });
                parameters.Add("no_persona", new StoredProcedure() { Value = "RETH CORDOVA" });
                parameters.Add("no_apellido_paterno", new StoredProcedure() { Value = "APACHE" });
                parameters.Add("no_apellido_materno", new StoredProcedure() { Value = "CORDOVA" });
                parameters.Add("no_razon_social", new StoredProcedure() { Value = "IONIC DEVELOPER" });
                parameters.Add("fe_nacimiento", new StoredProcedure() { Value = DateTime.Now });
                parameters.Add("co_tipo_persona", new StoredProcedure() { Value = "0001" });
                parameters.Add("co_tipo_documento", new StoredProcedure() { Value = "01" });
                parameters.Add("nu_documento", new StoredProcedure() { Value = "123456" });
                parameters.Add("no_correo", new StoredProcedure() { Value = "jcardenaspruebas@qnet.pe" });
                parameters.Add("nu_telefono", new StoredProcedure() { Value = "545445454" });
                parameters.Add("nu_celular", new StoredProcedure() { Value = "656565" });
                parameters.Add("co_sexo", new StoredProcedure() { Value = "0002" });
                parameters.Add("co_estado_civil", new StoredProcedure() { Value = "01" });
                parameters.Add("co_usuario_crea", new StoredProcedure() { Value = "admin" });
                parameters.Add("fe_crea", new StoredProcedure() { Value = DateTime.Now });
                parameters.Add("co_usuario_cambio", new StoredProcedure() { Value = "usuario" });
                parameters.Add("fe_cambio", new StoredProcedure() { Value = DateTime.Now });
                parameters.Add("no_estacion_red", new StoredProcedure() { Value = "127.0.0.1" });
                parameters.Add("no_usuario_red", new StoredProcedure() { Value = "red" });
                parameters.Add("fl_inactivo", new StoredProcedure() { Value = true });

                await clienteService.InsertarCliente(parameters);

                service.Message = "Cliente insertado.";
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        // PUT: api/Cliente/5
        [HttpPut]
        [Route("Actualizar")]
        public async Task<ServiceResult> Put([FromBody]ClienteDTO cliente)
        {
            ServiceResult service = new ServiceResult();
            Dictionary<string, StoredProcedure> parameters = new Dictionary<string, StoredProcedure>();
            try
            {

                parameters.Add("nid_persona ", new StoredProcedure() { Value = cliente.IdCliente });
                parameters.Add("no_persona", new StoredProcedure() { Value = cliente.Nombre });
                parameters.Add("no_apellido_paterno", new StoredProcedure() { Value = cliente.ApellidoPaterno });
                parameters.Add("no_apellido_materno", new StoredProcedure() { Value = cliente.ApellidoMaterno });
                parameters.Add("no_razon_social", new StoredProcedure() { Value = cliente.RazonSocial });
                parameters.Add("fe_nacimiento", new StoredProcedure() { Value = cliente.FechaNacimiento });
                parameters.Add("nu_documento", new StoredProcedure() { Value = cliente.NumeroDocumento });
                parameters.Add("no_correo", new StoredProcedure() { Value = cliente.Correo });
                parameters.Add("nu_telefono", new StoredProcedure() { Value = cliente.Telefono });
                parameters.Add("nu_celular", new StoredProcedure() { Value = cliente.Celular });
                parameters.Add("co_sexo", new StoredProcedure() { Value = cliente.CodigoGenero });
                parameters.Add("co_estado_civil", new StoredProcedure() { Value = cliente.CodigoEstadoCivil });

                //contacto cliente
                parameters.Add("idContacto", new StoredProcedure() { Value = cliente.IdContacto });
                parameters.Add("nombreContacto", new StoredProcedure() { Value = cliente.NombreContacto });
                parameters.Add("apellidoPaternoContacto", new StoredProcedure() { Value = cliente.ApellidoPaternoContacto });
                parameters.Add("apellidoMaternoContacto", new StoredProcedure() { Value = cliente.ApellidoMaternoContacto });
                parameters.Add("sexoContacto", new StoredProcedure() { Value = cliente.SexoContacto });
                parameters.Add("nuDocumentoContacto", new StoredProcedure() { Value = cliente.ContactoDocumento });
                parameters.Add("noCorreoContacto", new StoredProcedure() { Value = cliente.CorreoContacto });
                parameters.Add("nuTelefonoContacto", new StoredProcedure() { Value = cliente.TelefonoContacto });
                parameters.Add("nuCelularContacto ", new StoredProcedure() { Value = cliente.CelularContacto });

                //Datos de direccion
                parameters.Add("nid_direccion", new StoredProcedure() { Value = cliente.IdDireccion });
                parameters.Add("no_direccion", new StoredProcedure() { Value = cliente.Direccion });
                parameters.Add("co_postal", new StoredProcedure() { Value = cliente.CodigoPostal });
                parameters.Add("coddpto", new StoredProcedure() { Value = cliente.CodigoDepartamento });
                parameters.Add("codprov", new StoredProcedure() { Value = cliente.CodigoProvincia });
                parameters.Add("coddist", new StoredProcedure() { Value = cliente.CodigoDistrito });
                parameters.Add("nid_pais", new StoredProcedure() { Value = cliente.IdPais });

                //Auditoria
                AuditoriaDTO auditoria = this.getUserAuthen();
                parameters.Add("co_usuario_cambio", new StoredProcedure() { Value = auditoria.UsuarioCambio });
                parameters.Add("fe_cambio", new StoredProcedure() { Value = auditoria.FechaCambio });
                parameters.Add("no_estacion_red", new StoredProcedure() { Value = auditoria.EstacionRed });
                parameters.Add("no_usuario_red", new StoredProcedure() { Value = auditoria.UsuarioCambio });

                await clienteService.ActualizarCliente(parameters);

                var jsonData = new
                {
                    Id = cliente.IdCliente,
                    Nombres = cliente.Nombre,
                    cliente.ApellidoPaterno,
                    cliente.ApellidoMaterno,
                    cliente.RazonSocial,
                    cliente.FechaNacimiento,
                    CodigoTipoPersona = cliente.TipoPersona,
                    cliente.NumeroDocumento,
                    CodigoTipoDocumento = cliente.TipoDocumento,
                    cliente.NumeroDocumentoOriginal,
                    Email = cliente.Correo,
                    Telefono = cliente.Telefono,
                    cliente.Celular,
                    CodigoSexo = cliente.CodigoGenero,
                    cliente.CodigoEstadoCivil,
                    Direccion = new DireccionDTO()
                    {
                        Id = cliente.IdDireccion,
                        Direccion = cliente.Direccion,
                        CodigoDepartamento = cliente.CodigoDepartamento,
                        CodigoProvincia = cliente.CodigoProvincia,
                        CodigoDistrito = cliente.CodigoDistrito,
                        CodigoPais = cliente.IdPais.ToString(),
                        CodigoPostal = cliente.CodigoPostal
                    },
                    Contacto = new ContactoDTO()
                    {
                        Id = cliente.IdContacto,
                        Nombres = cliente.NombreContacto,
                        ApellidoPaterno = cliente.ApellidoPaternoContacto,
                        ApellidoMaterno = cliente.ApellidoMaternoContacto,
                        NumeroDocumento = cliente.ContactoDocumento,
                        CodigoTipoDocumento = cliente.TipoDocumentoContacto,
                        Email = cliente.CorreoContacto,
                        Telefono = cliente.TelefonoContacto,
                        Celular = cliente.CelularContacto,
                        CodigoSexo = cliente.SexoContacto,
                    }
                };

                SyncServiceResult syncService = new SyncServiceResult();
                syncService.Key = Guid.NewGuid().ToString();
                syncService.RequestType = "1";
                syncService.IdTipoProceso = "1";
                syncService.Data = jsonData;
                var response = await serviceClient.PostAsync("/sincronizacion/cliente", syncService);
                // var respuesta = await response.Content.ReadAsJsonAsync<SyncResponse>();
                var respuesta = await response.Content.ReadAsStringAsync();

                service.Message = response.ToString();
            }
            catch (Exception ex)
            {
                service.Errors(ex);
                _logger.LogError(ex);
            }
            return service;
        }

        [HttpDelete("{id}")]
        public ServiceResult Delete(int id)
        {
            ServiceResult service = new ServiceResult();
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return service;
        }
    }
}
