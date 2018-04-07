using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Contracts.Repository;
using AutoMapper;
using Gildemeister.Cliente360.Domain;
using System.Linq;

namespace Gildemeister.Cliente360.Application
{
    public class ServicioService : IServiceBase<ServicioDTO>, IServicioService
    {
        private IServicioRepository servicioRepository;
        private IMapper _mapper;

        public ServicioService(IServicioRepository _servicioRepository, IMapper mapper)
        {
            this.servicioRepository = _servicioRepository;
            this._mapper = mapper;
        }

        public IEnumerable<ServicioDTO> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServicioDTO>> BuscarPorClienteAsync(string clienteId)
        {
            try
            {
                IEnumerable<Servicio> _servicios = await servicioRepository.BuscarPorClienteAsync(clienteId);
                IEnumerable<ServicioDTO> _serviciosDTO = _mapper.Map<IEnumerable<Servicio>, IEnumerable<ServicioDTO>>(_servicios);
                return _serviciosDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServicioDTO> BuscarPorCodigoAsync(string clienteId, string codigoId)
        {
            try
            {
                Servicio _servicio = await servicioRepository.BuscarPorCodigoAsync(clienteId, codigoId);
                ServicioDTO _servicioDTO = _mapper.Map<Servicio, ServicioDTO>(_servicio);
                return _servicioDTO;
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

        public async Task<ArchivoReporte> ExportarAsync(string clienteId)
        {
            IEnumerable<Servicio> _servicios = await servicioRepository.BuscarPorClienteAsync(clienteId);
            IEnumerable<ServicioDTO> _serviciosDTO = _mapper.Map<IEnumerable<Servicio>, IEnumerable<ServicioDTO>>(_servicios);
            var _serviciosToExport = (from c in _serviciosDTO
                                   select new
                                   {

                                   }).ToList();

            Dictionary<string, string> colNames = null;
            Dictionary<string, int> colWidth = null;

            colNames = new Dictionary<string, string>();
            colNames.Add("NombreMarca", "Marca");
            colNames.Add("NombreModelo", "Modelo");
            colNames.Add("CodigoFamiliaCorto", "Version");
            colNames.Add("NombreCliente", "Cliente");
            colNames.Add("NumeroDocumento", "Numero de Documento");
            colNames.Add("NombreAsesor", "Asesor");
            colNames.Add("AsesorPuntoVenta", "Punto de Venta");
            colNames.Add("FechaPruebaManejo", "Fecha");

            colWidth = new Dictionary<string, int>();
            colWidth.Add("NombreMarca", 20);
            colWidth.Add("NombreModelo", 20);
            colWidth.Add("CodigoFamiliaCorto", 20);
            colWidth.Add("NombreCliente", 60);
            colWidth.Add("NumeroDocumento", 20);
            colWidth.Add("NombreAsesor", 60);
            colWidth.Add("AsesorPuntoVenta", 60);
            colWidth.Add("FechaPruebaManejo", 20);

            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.White.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Black.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte TestDrive",
                Filtros = new List<string>(),
                Datos = _serviciosToExport,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });
            return exportar;
        }

        public IEnumerable<ServicioDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ServicioDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(ServicioDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServicioDTO> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task Update(ServicioDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
