using AutoMapper;
using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Transport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface ISincronizarService
    {
        Task PostAsync(string content);

        Task PutAsync(string content);

        ArchivoReporte ExportarSincronizacion(IEnumerable<VisorSincronizacionDTO> lista);
    }
    public class SincronizarService : ISincronizarService
    {
        private IMapper _mapper;

        //private readonly IServiceClient serviceClient;

        public SincronizarService(
            IMapper mapper)
        {
            _mapper = mapper;
        }
        public ArchivoReporte ExportarSincronizacion(IEnumerable<VisorSincronizacionDTO> lista)
        {
            var visorDTOList = (from x in lista
                                select new
                                {
                                    x.Fecha,
                                    x.TipoSincronizacion,
                                    x.IdProceso,
                                    x.TipoProceso,
                                    x.Aplicacion,
                                    x.Estado,
                                    x.Observacion,
                                    x.Data
                                }).ToList();

            Dictionary<string, string> colNames = new Dictionary<string, string>();
            Dictionary<string, int> colWidth = new Dictionary<string, int>();

            colNames.Add("Fecha", "Fecha");
            colNames.Add("TipoSincronizacion", "Tipo Sincronizacion");
            colNames.Add("IdProceso", "Id Proceso");
            colNames.Add("TipoProceso", "Tipo Proceso");
            colNames.Add("Aplicacion", "Aplicacion");
            colNames.Add("Estado", "Estado");
            colNames.Add("Observacion", "Observacion");
            colNames.Add("Data", "Data");

            colWidth.Add("Fecha", 20);
            colWidth.Add("TipoSincronizacion", 20);
            colWidth.Add("IdProceso", 20);
            colWidth.Add("TipoProceso", 20);
            colWidth.Add("Aplicacion", 20);
            colWidth.Add("Estado", 20);
            colWidth.Add("Observacion", 60);
            colWidth.Add("Data", 60);

            var estiloCelda = new EstiloCelda()
            {
                ColorLetra = NPOI.HSSF.Util.HSSFColor.LightBlue.Index,
                ColorFondo = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            };

            var exportar = UtilExcel.ExportarArchivo(new PlantillaExcel()
            {
                CabeceraColumna = colNames,
                TituloExcel = "Reporte Visor Sincronizacion",
                Filtros = new List<string>(),
                Datos = visorDTOList,
                EstiloCeldaCabecera = estiloCelda,
                EstiloCelda = new EstiloCelda() { AjustarCelda = true },
                EstiloFiltro = new EstiloCelda() { EsNegrita = true },
                EstiloTitulo = new EstiloCelda() { Tamanio = 14, EsNegrita = true },
                TamanioColumna = colWidth
            });

            return exportar;
        }

        public Task PostAsync(string content)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
