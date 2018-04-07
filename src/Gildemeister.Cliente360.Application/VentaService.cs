using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using Gildemeister.Cliente360.Common;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Contracts.Repository;
using AutoMapper;
using Gildemeister.Cliente360.Domain;
using System.Linq;

namespace Gildemeister.Cliente360.Application
{
    public class VentaService : IServiceBase<VentaDTO>, IVentaService
    {
        private IVentaRepository ventaRepository;
        private IMapper _mapper;

        public VentaService(IVentaRepository _ventaRepository, IMapper mapper)
        {
            this.ventaRepository = _ventaRepository;
            this._mapper = mapper;
        }

        public IEnumerable<VentaDTO> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VentaDTO>> BuscarPorClienteAsync(string clienteId)
        {
            try
            {
                IEnumerable<Venta> _ventas = await ventaRepository.BuscarPorClienteAsync(clienteId);
                IEnumerable<VentaDTO> _ventasDTO = _mapper.Map<IEnumerable<Venta>, IEnumerable<VentaDTO>>(_ventas);
                return _ventasDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VentaDTO> BuscarPorCodigoAsync(string clienteId, string codigoId)
        {
            try
            {
                Venta _venta = await ventaRepository.BuscarPorCodigoAsync(clienteId, codigoId);
                VentaDTO _ventaDTO = _mapper.Map<Venta, VentaDTO>(_venta);
                return _ventaDTO;
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
            IEnumerable<Venta> _ventas = await ventaRepository.BuscarPorClienteAsync(clienteId);
            IEnumerable<VentaDTO> _ventasDTO = _mapper.Map<IEnumerable<Venta>, IEnumerable<VentaDTO>>(_ventas);
            var _ventasToExport = (from c in _ventasDTO
                                       select new
                                       {
                                          c.NombreComercial,
                                          c.NumeroNotaPedido,
                                          c.NombreTipoVenta,
                                          c.FechaFacturacion,
                                          c.FechaEntrega,
                                          c.EstadoDespacho,
                                          c.FechaEstimacionDespacho,                                          
                                       }).ToList();

            Dictionary<string, string> colNames = null;
            Dictionary<string, int> colWidth = null;

            colNames = new Dictionary<string, string>();
            colNames.Add("NombreComercial", "Descripcion");
            colNames.Add("NumeroNotaPedido", "Codigo");
            colNames.Add("NombreTipoVenta", "Tipo Venta");
            colNames.Add("FechaFacturacion", "Fecha Facturacion");
            colNames.Add("FechaEntrega", "Fecha Entrega");
            colNames.Add("EstadoDespacho", "Estado");
            colNames.Add("FechaEstimacionDespacho", "Fecha Estado");
            
            colWidth = new Dictionary<string, int>();
            colWidth.Add("NombreComercial", 20);
            colWidth.Add("NumeroNotaPedido", 20);
            colWidth.Add("NombreTipoVenta", 20);
            colWidth.Add("FechaFacturacion", 60);
            colWidth.Add("FechaEntrega", 20);
            colWidth.Add("EstadoDespacho", 60);
            colWidth.Add("FechaEstimacionDespacho", 60);
            
            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.White.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Black.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte Ventas",
                Filtros = new List<string>(),
                Datos = _ventasToExport,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });
            return exportar;
        }

        public IEnumerable<VentaDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<VentaDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(VentaDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VentaDTO> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task Update(VentaDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
