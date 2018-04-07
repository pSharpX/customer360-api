using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface ITestDriveService : IServiceBase<TestDriveDTO>
    {
        IEnumerable<TestDriveDTO> Listar(int pageNo, int pageSize, out int pageCount);
        IEnumerable<TestDriveDTO> Buscar(int tipofiltro, string textofiltro);
        Task<TestDriveDTO> BuscarPorSolicitudAsync(string clienteId, string solicitudId);
        Task<IEnumerable<TestDriveDTO>> BuscarPorClienteAsync(string clienteId);        
        Task<ArchivoReporte> ExportarAsync(string clienteId);
    }
}
