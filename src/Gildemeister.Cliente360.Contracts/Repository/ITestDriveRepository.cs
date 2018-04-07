using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface ITestDriveRepository : IRepositoryBase<TestDrive>
    {
        Task<IEnumerable<TestDrive>> GetTestDrive(int page, int pageSize, out int pageCount);
        Task<IEnumerable<TestDrive>> Listar(int pageNo, int pageSize, out int pageCount);
        Task<IEnumerable<TestDrive>> Buscar(int tipofiltro, string textofiltro);
        Task<TestDrive> BuscarPorSolicitudAsync(string numeroDocumento, string numeroSolicitud);
        Task<IEnumerable<TestDrive>> BuscarPorClienteAsync(string clienteDNI);        
    }
}
