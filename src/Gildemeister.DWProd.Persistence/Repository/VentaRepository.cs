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
    public class VentaRepository : RepositoryBase<Venta>, IVentaRepository
    {
        public VentaRepository(DWProdDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Venta>> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Venta>> BuscarPorClienteAsync(string clienteDNI)
        {
            IEnumerable<Venta> _ventas = null;
            await context.LoadStoredProc("sgsnet_sps_venta_por_cliente")
                 .WithSqlParam("numeroDocumento", clienteDNI)
                 .ExecuteStoredProcAsync((handler) =>
                 {
                     _ventas = handler.ReadToList<Venta>();
                     handler.NextResult();

                 });
            return _ventas;
        }

        public async Task<Venta> BuscarPorCodigoAsync(string numeroDocumento, string codigo)
        {
            IEnumerable<Venta> _venta = null;
            await context.LoadStoredProc("sgsnet_sps_venta_por_codigo")
                 .WithSqlParam("numeroDocumento", numeroDocumento)
                 .WithSqlParam("numeroNotaPedido", codigo)
                 .ExecuteStoredProcAsync(async (handler) =>
                 {
                     _venta = handler.ReadToList<Venta>();
                     await handler.NextResultAsync();

                 });
            return _venta.FirstOrDefault();
        }

        public Task<IEnumerable<Venta>> Listar(int pageNo, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}
