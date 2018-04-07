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
    public class TestDriveService : IServiceBase<TestDriveDTO>, ITestDriveService
    {
        private ITestDriveRepository testdriveRepository;
        private IMapper _mapper;

        public TestDriveService(ITestDriveRepository _testdriveRepository, IMapper mapper)
        {
            this.testdriveRepository = _testdriveRepository;
            this._mapper = mapper;
        }

        public IEnumerable<TestDriveDTO> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestDriveDTO>> BuscarPorClienteAsync(string clienteId)
        {
            try
            {
                IEnumerable<TestDrive> _testsdrive = await testdriveRepository.BuscarPorClienteAsync(clienteId);
                IEnumerable<TestDriveDTO> _testsdriveDTO = _mapper.Map<IEnumerable<TestDrive>, IEnumerable<TestDriveDTO>>(_testsdrive);
                return _testsdriveDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TestDriveDTO> BuscarPorSolicitudAsync(string clienteId, string solicitudId)
        {
            try
            {
                TestDrive _testdrive = await testdriveRepository.BuscarPorSolicitudAsync(clienteId, solicitudId);
                TestDriveDTO testdriveDTO = _mapper.Map<TestDrive, TestDriveDTO>(_testdrive);
                return testdriveDTO;
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
            IEnumerable<TestDrive> _testsdrive = await testdriveRepository.BuscarPorClienteAsync(clienteId);
            IEnumerable<TestDriveDTO> _testsdriveDto = _mapper.Map<IEnumerable<TestDrive>, IEnumerable<TestDriveDTO>>(_testsdrive);
            var _testsdriveToExport = (from c in _testsdriveDto
                                       select new
                                       {
                                           c.NombreMarca,
                                           c.NombreModelo,
                                           c.CodigoFamiliaCorto,
                                           c.NombreCliente,
                                           c.NumeroDocumento,
                                           c.NombreAsesor,
                                           c.AsesorPuntoVenta,
                                           c.FechaPruebaManejo
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
                Datos = _testsdriveToExport,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });
            return exportar;
        }

        public IEnumerable<TestDriveDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TestDriveDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(TestDriveDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestDriveDTO> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task Update(TestDriveDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
