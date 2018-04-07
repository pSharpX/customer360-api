using AutoMapper;
using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gildemeister.DWProd.Persistence.Repository;
using Gildemeister.Cliente360.Contracts.Repository.DWProd;
using System.Data;
using Gildemeister.Cliente360.Domain.SGAPROD;
using Gildemeister.Cliente360.Transport.SGAPROD;
using Gildemeister.Cliente360.Contracts.Repository.SGAPROD;

namespace Gildemeister.Cliente360.Application
{

    public class ClienteService : IServiceBase<ClienteDTO>, IClienteService
    {
        private IClienteRepository clienteRepository;
        private IClienteDWProdRepository clienteDWProdRepository;
        private IMarcaRepository marcaRepository;
        private IModeloRepository modeloRepository;
        private IAsesorRepository asesorRepository;
        private IPuntoVentaRepository puntoVentaRepository;
        private IUbigeoRepository ubigeoRepository;

        private IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository,
            IClienteDWProdRepository clienteDWProdRepository,
            IMarcaRepository marcaRepository,
            IModeloRepository modeloRepository,
            IAsesorRepository asesorRepository,
            IPuntoVentaRepository puntoVentaRepository,
            IUbigeoRepository ubigeoRepository,
            IMapper mapper)
        {
            this.clienteRepository = clienteRepository;
            this.clienteDWProdRepository = clienteDWProdRepository;
            this.marcaRepository = marcaRepository;
            this.modeloRepository = modeloRepository;
            this.asesorRepository = asesorRepository;
            this.puntoVentaRepository = puntoVentaRepository;            
            this.ubigeoRepository = ubigeoRepository;            
            _mapper = mapper;
        }
        public async Task Insert(ClienteDTO entity)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(entity);
                await clienteRepository.Insert(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(ClienteDTO entity)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(entity);
                await clienteRepository.Update(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ClienteDTO> GetById(int ind)
        {
            try
            {
                Cliente cliente = await clienteRepository.GetById(ind);
                ClienteDTO clienteDTO = _mapper.Map<ClienteDTO>(cliente);
                return clienteDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            try
            {
                IEnumerable<Cliente> clienteList = clienteRepository.GetAll();
                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<ClienteDTO> GetCustomer(int page, int pageSize, out int pageCount)
        {
            try
            {


                IEnumerable<Cliente> clienteList = clienteRepository.GetCustomer(page, pageSize, out pageCount);
                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<ClienteDTO> ListarCliente(int page, int pageSize, out int pageCount)
        {

            try
            {
                IEnumerable<Cliente> clienteList = clienteRepository.ListarCliente(page, pageSize, out pageCount);
                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<ClienteDTO> BuscarPorCodigo(int clienteId)
        {
            try
            {
                IEnumerable<Cliente> clienteList = clienteRepository.BuscarPorCodigo(clienteId);
                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task ActualizarCliente(Dictionary<string, StoredProcedure> parameters)
        {

            try
            {

                await clienteRepository.ActualizarCliente(parameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task InsertarCliente(Dictionary<string, StoredProcedure> parameters)
        {
            try
            {
                await clienteRepository.InsertarCliente(parameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<ClienteDTO> BuscarCliente(int tipofiltro, string textofiltro)
        {
            try
            {
                IEnumerable<Cliente> clienteList = clienteRepository.BuscarCliente(tipofiltro, textofiltro);
                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArchivoReporte ExportarCliente(int tipofiltro, string textofiltro)
        {
            IEnumerable<Cliente> clienteList = clienteRepository.BuscarCliente(tipofiltro, textofiltro);
            var clienteDTOList = (from x in clienteList
                                  select new
                                  {
                                      x.TipoPersona,
                                      x.NumeroDocumento,
                                      x.NombreCompleto,
                                      x.ApellidoPaterno,
                                      x.ApellidoMaterno,
                                      x.Genero,
                                      x.FechaNacimiento,
                                      x.Telefono,
                                      x.Celular,
                                      x.Correo,
                                      x.Direccion,
                                      x.Departamento,
                                      x.Provincia,
                                      x.Distrito
                                  }).ToList();

            Dictionary<string, string> colNames = new Dictionary<string, string>();
            Dictionary<string, int> colWidth = new Dictionary<string, int>();

            colNames.Add("TipoPersona", "Tipo cliente");
            colNames.Add("NumeroDocumento", "Doc cliente");
            colNames.Add("NombreCompleto", "Cliente Nombre");
            colNames.Add("ApellidoPaterno", "Cliente Ape Paterno");
            colNames.Add("ApellidoMaterno", "Cliente Ape Materno");
            colNames.Add("Genero", "Sexo");
            colNames.Add("FechaNacimiento", "Fecha Nacimiento");
            colNames.Add("Telefono", "Teléfono");
            colNames.Add("Celular", "Celular");
            colNames.Add("Correo", "Correo");
            colNames.Add("Direccion", "Dirección");
            colNames.Add("Departamento", "Departamento");
            colNames.Add("Provincia", "Provincia");
            colNames.Add("Distrito", "Distrito");

            colWidth.Add("TipoPersona", 20);
            colWidth.Add("NumeroDocumento", 40);
            colWidth.Add("NombreCompleto", 40);
            colWidth.Add("ApellidoPaterno", 40);
            colWidth.Add("ApellidoMaterno", 40);
            colWidth.Add("Genero", 20);
            colWidth.Add("FechaNacimiento", 20);
            colWidth.Add("Telefono", 20);
            colWidth.Add("Celular", 20);
            colWidth.Add("Correo", 40);
            colWidth.Add("Direccion", 40);
            colWidth.Add("Departamento", 40);
            colWidth.Add("Provincia", 40);
            colWidth.Add("Distrito", 40);

            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.LightBlue.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte Clientes",
                Filtros = new List<string>(),
                Datos = clienteDTOList,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });

            return exportar;
        }

        public IEnumerable<Cliente> ObtenerClienteAvanzada(string aniofabricacion, string aniomodelo, string sucursal, string asesorcomercial,
            string fechaentregaDe, string fechaentregaHasta, string asesorservicio, string fechaservicioDe, string fechaservicioHasta, string marca, string modelo,
            string departamento, string provincia, string distrito, bool? porventavehiculo, bool? porservicio, bool? porrepuesto,
            string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin)
        {
            DataTable dtCliente = new DataTable();
            DataRow row;

            dtCliente.Columns.Add("NumeroDocumento", typeof(String));
            dtCliente.Columns.Add("TipoPersona", typeof(String));

            IEnumerable<Cliente> clienteDWList = clienteDWProdRepository.BuscarClienteAdvancedDWProd(aniofabricacion, aniomodelo, sucursal, asesorcomercial,
                 fechaentregaDe, fechaentregaHasta, asesorservicio, fechaservicioDe, fechaservicioHasta,
                 asesorVendedor, fechaVentaDe, fechaVentaHasta, vin);

            clienteDWList.ToList().ForEach(x =>
            {
                row = dtCliente.NewRow();
                row["NumeroDocumento"] = x.NumeroDocumento;
                row["TipoPersona"] = x.TipoPersona;
                dtCliente.Rows.Add(row);
            });

            bool usafiltro01 = false;

            if (aniofabricacion != null)
            {
                usafiltro01 = true;
            }
            else if (aniomodelo != null)
            {
                usafiltro01 = true;
            }
            else if (sucursal != null)
            {
                usafiltro01 = true;
            }
            else if (asesorcomercial != null)
            {
                usafiltro01 = true;
            }
            else if (fechaentregaDe != null)
            {
                usafiltro01 = true;
            }
            else if (fechaentregaHasta != null)
            {
                usafiltro01 = true;
            }
            else if (asesorservicio != null)
            {
                usafiltro01 = true;
            }
            else if (fechaservicioDe != null)
            {
                usafiltro01 = true;
            }
            else if (fechaservicioHasta != null)
            {
                usafiltro01 = true;
            }

            IEnumerable<Cliente> clienteList = clienteRepository.BuscarClienteAdvanced(dtCliente,
                marca, modelo, departamento, provincia, distrito, usafiltro01, porventavehiculo, porservicio, porrepuesto);

            return clienteList;
        }

        public IEnumerable<ClienteDTO> BuscarClienteAdvanced(string aniofabricacion, string aniomodelo, string sucursal, string asesorcomercial,
            string fechaentregaDe, string fechaentregaHasta, string asesorservicio,
            string fechaservicioDe, string fechaservicioHasta, string marca, string modelo,
            string departamento, string provincia, string distrito, bool? porventavehiculo,
            bool? porservicio, bool? porrepuesto,
            string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin)
        {
            try
            {
                var clienteList = ObtenerClienteAvanzada(aniofabricacion, aniomodelo, sucursal, asesorcomercial,
             fechaentregaDe, fechaentregaHasta, asesorservicio, fechaservicioDe, fechaservicioHasta, marca, modelo,
             departamento, provincia, distrito, porventavehiculo, porservicio, porrepuesto, asesorVendedor,
             fechaVentaDe, fechaVentaHasta, vin);

                IEnumerable<ClienteDTO> clienteDTOList = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clienteList);

                return clienteDTOList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArchivoReporte ExportarClientesAdvanced(string aniofabricacion, string aniomodelo, string sucursal, string asesorcomercial,
            string fechaentregaDe, string fechaentregaHasta, string asesorservicio,
            string fechaservicioDe, string fechaservicioHasta, string marca, string modelo,
            string departamento, string provincia, string distrito, bool? porventavehiculo,
            bool? porservicio, bool? porrepuesto,
            string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin)
        {
            var clienteList = ObtenerClienteAvanzada(aniofabricacion, aniomodelo, sucursal, asesorcomercial,
             fechaentregaDe, fechaentregaHasta, asesorservicio, fechaservicioDe, fechaservicioHasta, marca, modelo,
             departamento, provincia, distrito, porventavehiculo, porservicio, porrepuesto,
             asesorVendedor, fechaVentaDe, fechaVentaHasta, vin);

            var clienteDTOList = (from x in clienteList
                                  select new
                                  {
                                      x.TipoPersona,
                                      x.NumeroDocumento,
                                      x.NombreCompleto,
                                      x.ApellidoPaterno,
                                      x.ApellidoMaterno,
                                      x.Genero,
                                      x.FechaNacimiento,
                                      x.Telefono,
                                      x.Celular,
                                      x.Correo,
                                      x.Direccion,
                                      x.Departamento,
                                      x.Provincia,
                                      x.Distrito
                                  }).ToList();

            Dictionary<string, string> colNames = null;
            Dictionary<string, int> colWidth = null;

            colNames = new Dictionary<string, string>();
            colNames.Add("TipoPersona", "Tipo cliente");
            colNames.Add("NumeroDocumento", "Doc cliente");
            colNames.Add("NombreCompleto", "Cliente Nombre");
            colNames.Add("ApellidoPaterno", "Cliente Ape Paterno");
            colNames.Add("ApellidoMaterno", "Cliente Ape Materno");
            colNames.Add("Genero", "Sexo");
            colNames.Add("FechaNacimiento", "Fecha Nacimiento");
            colNames.Add("Telefono", "Teléfono");
            colNames.Add("Celular", "Celular");
            colNames.Add("Correo", "Correo");
            colNames.Add("Direccion", "Dirección");
            colNames.Add("Departamento", "Departamento");
            colNames.Add("Provincia", "Provincia");
            colNames.Add("Distrito", "Distrito");

            colWidth = new Dictionary<string, int>();
            colWidth.Add("TipoPersona", 20);
            colWidth.Add("NumeroDocumento", 40);
            colWidth.Add("NombreCompleto", 40);
            colWidth.Add("ApellidoPaterno", 40);
            colWidth.Add("ApellidoMaterno", 40);
            colWidth.Add("Genero", 20);
            colWidth.Add("FechaNacimiento", 20);
            colWidth.Add("Telefono", 20);
            colWidth.Add("Celular", 20);
            colWidth.Add("Correo", 40);
            colWidth.Add("Direccion", 40);
            colWidth.Add("Departamento", 40);
            colWidth.Add("Provincia", 40);
            colWidth.Add("Distrito", 40);

            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.LightBlue.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte Clientes",
                Filtros = new List<string>(),
                Datos = clienteDTOList,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });

            return exportar;
        }

        public ClienteDTO ListarFilterAdvanced()
        {
            ClienteDTO clienteDTO = new ClienteDTO();

            IEnumerable<Marca> marcaList = marcaRepository.Listar();
            IEnumerable<MarcaDTO> marcaDTOList = _mapper.Map<IEnumerable<Marca>, IEnumerable<MarcaDTO>>(marcaList);

            IEnumerable<Modelo> modeloList = modeloRepository.Listar();
            IEnumerable<ModeloDTO> modeloDTOList = _mapper.Map<IEnumerable<Modelo>, IEnumerable<ModeloDTO>>(modeloList);

            IEnumerable<PuntoVenta> puntoventaList = puntoVentaRepository.Listar();
            IEnumerable<PuntoVentaDTO> puntoventaDTOList = _mapper.Map<IEnumerable<PuntoVenta>, IEnumerable<PuntoVentaDTO>>(puntoventaList);

            IEnumerable<PersonaAsesor> asesorcomercialList = asesorRepository.ListarAsesorComercial();
            IEnumerable<PersonaAsesorDTO> asesorcomercialDTOList = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(asesorcomercialList);

            IEnumerable<PersonaAsesor> asesorservicioList = asesorRepository.ListarAsesorServicio();
            IEnumerable<PersonaAsesorDTO> asesorservicioDTOList = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(asesorservicioList);

            IEnumerable<Ubigeo> departamentoList = ubigeoRepository.ListarDepartamento();
            IEnumerable<UbigeoDTO> departamentoDTOList = _mapper.Map<IEnumerable<Ubigeo>, IEnumerable<UbigeoDTO>>(departamentoList);

            IEnumerable<PersonaAsesor> asesorvendedorList = asesorRepository.ListarAsesorVendedor();
            IEnumerable<PersonaAsesorDTO> asesorvendedorDTOList = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(asesorvendedorList);

            clienteDTO.ListMarcas = new List<MarcaDTO>();
            clienteDTO.ListMarcas = marcaDTOList;

            clienteDTO.ListModelos = new List<ModeloDTO>();
            clienteDTO.ListModelos = modeloDTOList;

            clienteDTO.ListPuntoVenta = new List<PuntoVentaDTO>();
            clienteDTO.ListPuntoVenta = puntoventaDTOList;

            clienteDTO.ListAsesorComercial = new List<PersonaAsesorDTO>();
            clienteDTO.ListAsesorComercial = asesorcomercialDTOList;

            clienteDTO.ListAsesorServicio = new List<PersonaAsesorDTO>();
            clienteDTO.ListAsesorServicio = asesorservicioDTOList;

            clienteDTO.ListDepartamento = new List<UbigeoDTO>();
            clienteDTO.ListDepartamento = departamentoDTOList;

            clienteDTO.ListAsesorVendedor = new List<PersonaAsesorDTO>();
            clienteDTO.ListAsesorVendedor = asesorvendedorDTOList;

            return clienteDTO;
        }
    }
}
