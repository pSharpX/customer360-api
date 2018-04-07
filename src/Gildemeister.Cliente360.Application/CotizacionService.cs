using AutoMapper;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public class CotizacionService : IServiceBase<CotizacionDTO>, ICotizacionService
    {
        private ICotizacionRepository cotizacionRepository;        
        private IMapper _mapper;

        public CotizacionService(ICotizacionRepository _cotizacionRepository, IMapper mapper)
        {
            this.cotizacionRepository = _cotizacionRepository;            
            this._mapper = mapper;
        }

        public Task Actualizar(Dictionary<string, StoredProcedure> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CotizacionDTO> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CotizacionDTO>> BuscarPorClienteAsync(string clienteId)
        {
            try
            {
                IEnumerable<Cotizacion> _cotizaciones = await cotizacionRepository.BuscarPorClienteAsync(clienteId);
                IEnumerable<CotizacionDTO> _cotizacionesDTO = _mapper.Map<IEnumerable<Cotizacion>, IEnumerable<CotizacionDTO>>(_cotizaciones);                
                return _cotizacionesDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CotizacionDTO> BuscarPorCodigoAsync(string clienteId, string codigoId)
        {
            try
            {
                Cotizacion _cotizacion = await cotizacionRepository.BuscarPorCodigoAsync(clienteId, codigoId);
                CotizacionDTO _cotizacionDTO = _mapper.Map<Cotizacion, CotizacionDTO>(_cotizacion);                
                return _cotizacionDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ArchivoReporte> ExportarAsync(string clienteId)
        {
            IEnumerable<Cotizacion> _cotizaciones = await cotizacionRepository.BuscarPorClienteAsync(clienteId);
            IEnumerable<CotizacionDTO> _cotizacionesDto = _mapper.Map<IEnumerable<Cotizacion>, IEnumerable<CotizacionDTO>>(_cotizaciones);
            var _cotizacionesToExport = (from c in _cotizacionesDto
                                         select new
                                         {
                                             c.NombreMarca,
                                             c.NombreModelo,
                                             c.NombreEstado,
                                             c.AñoModelo,
                                             c.NombreCliente,
                                             c.NumeroDocumento,
                                             c.TipoCliente,
                                             c.NombreEmpleado,
                                             c.NumeroCotizacion,
                                             c.FechaRegistro,
                                             c.MontoPrecioVenta,
                                             c.MontoPrecioCierre,
                                             c.NombrePuntoVenta
                                         }).ToList();

            Dictionary<string, string> colNames = null;
            Dictionary<string, int> colWidth = null;

            colNames = new Dictionary<string, string>();
            colNames.Add("NombreMarca", "Marca");
            colNames.Add("NombreModelo", "Modelo");
            colNames.Add("NombreEstado", "Estado");
            colNames.Add("AñoModelo", "Version");
            colNames.Add("NombreCliente", "Cliente");
            colNames.Add("NumeroDocumento", "Numero de Documento");
            colNames.Add("TipoCliente", "Tipo de Cliente");
            colNames.Add("NombreEmpleado", "Asesor");
            colNames.Add("NumeroCotizacion", "Numero Cotizacion");
            colNames.Add("FechaRegistro", "Fecha");
            colNames.Add("MontoPrecioVenta", "Precio Final");
            colNames.Add("MontoPrecioCierre", "Precio Cierre");
            colNames.Add("NombrePuntoVenta", "Punto de Venta");

            colWidth = new Dictionary<string, int>();
            colWidth.Add("NombreMarca", 20);
            colWidth.Add("NombreModelo", 20);
            colWidth.Add("NombreEstado", 20);
            colWidth.Add("AñoModelo", 10);
            colWidth.Add("NombreCliente", 60);
            colWidth.Add("NumeroDocumento", 20);
            colWidth.Add("TipoCliente", 20);
            colWidth.Add("NombreEmpleado", 60);
            colWidth.Add("NumeroCotizacion", 20);
            colWidth.Add("FechaRegistro", 20);
            colWidth.Add("MontoPrecioVenta", 20);
            colWidth.Add("MontoPrecioCierre", 20);
            colWidth.Add("NombrePuntoVenta", 40);

            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.White.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Black.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte Cotizaciones",
                Filtros = new List<string>(),
                Datos = _cotizacionesToExport,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });
            return exportar;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CotizacionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CotizacionDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(CotizacionDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CotizacionDTO> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task Update(CotizacionDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
