using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gildemeister.DWProd.Persistence.Database;
using System.Linq;

namespace Gildemeister.DWProd.Persistence.Repository
{
    public class TestDriveRepository : RepositoryBase<TestDrive>, ITestDriveRepository
    {
        public TestDriveRepository(DWProdDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<TestDrive>> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestDrive>> BuscarPorClienteAsync(string clienteDNI)
        {
            IEnumerable<TestDrive> _testsDrive = null;
            await context.LoadStoredProc("sgsnet_sps_testdrive_por_cliente")
                 .WithSqlParam("numeroDocumento", clienteDNI)
                 .ExecuteStoredProcAsync((handler) =>
                 {
                     _testsDrive = handler.ReadToList<TestDrive>();
                     handler.NextResult();

                 });
            return _testsDrive;
        }

        public async Task<TestDrive> BuscarPorSolicitudAsync(string numeroDocumento, string numeroSolicitud)
        {
            IEnumerable<TestDrive> _testsDrive = null;
            await context.LoadStoredProc("sgsnet_sps_testdrive_por_solicitud")
                 .WithSqlParam("numeroDocumento", numeroDocumento)
                 .WithSqlParam("numeroCotizacion", numeroSolicitud)
                 .ExecuteStoredProcAsync(async (handler) =>
                 {
                     _testsDrive = handler.ReadToList<TestDrive>();
                     await handler.NextResultAsync();

                 });
            return _testsDrive.FirstOrDefault();
        }

        public Task<IEnumerable<TestDrive>> GetTestDrive(int page, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TestDrive>> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}
