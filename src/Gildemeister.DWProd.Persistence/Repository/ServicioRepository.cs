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
    public class ServicioRepository : RepositoryBase<Servicio>, IServicioRepository
    {
        public ServicioRepository(DWProdDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Servicio>> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Servicio>> BuscarPorClienteAsync(string clienteDNI)
        {
            IEnumerable<Servicio> _servicios = null;
            await context.LoadStoredProc("sgsnet_sps_servicio_por_cliente")
                 .WithSqlParam("numeroDocumento", clienteDNI)
                 .ExecuteStoredProcAsync((handler) =>
                 {
                     _servicios = handler.ReadToList<Servicio>();
                     handler.NextResult();

                 });
            return _servicios;
        }

        public async Task<Servicio> BuscarPorCodigoAsync(string numeroDocumento, string codigo)
        {
            IEnumerable<Servicio> _servicio = null;
            await context.LoadStoredProc("sgsnet_sps_servicio_por_codigo")
                 .WithSqlParam("numeroDocumento", numeroDocumento)
                 .WithSqlParam("?", codigo)
                 .ExecuteStoredProcAsync(async (handler) =>
                 {
                     _servicio = handler.ReadToList<Servicio>();
                     await handler.NextResultAsync();

                 });
            return _servicio.FirstOrDefault();
        }

        public Task<IEnumerable<Servicio>> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}
