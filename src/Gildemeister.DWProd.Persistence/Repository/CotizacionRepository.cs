using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.DWProd.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.DWProd.Persistence.Repository
{
    public class CotizacionRepository : RepositoryBase<Cotizacion>, ICotizacionRepository
    {
        private IServiceClient serviceClient;

        public CotizacionRepository(DWProdDbContext context) : base(context)
        {
        }

        public CotizacionRepository(DWProdDbContext context, IServiceClient _serviceClient)
            : base(context)
        {
            this.serviceClient = _serviceClient;
        }

        public Task Actualizar(Dictionary<string, StoredProcedure> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> Buscar(int tipofiltro, string textofiltro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cotizacion>> BuscarPorClienteAsync(string clienteId)
        {
            IEnumerable<Cotizacion> _cotizaciones = null;
            await context.LoadStoredProc("sgsnet_sps_cotizacion_por_cliente")
                 .WithSqlParam("numeroDocumento", clienteId)
                 .ExecuteStoredProcAsync((handler) =>
                 {
                     _cotizaciones = handler.ReadToList<Cotizacion>();
                     handler.NextResult();

                 });            
            return _cotizaciones;
        }

        public async Task<Cotizacion> BuscarPorCodigoAsync(string clienteId, string cotizacionId)
        {
            IEnumerable<Cotizacion> _cotizaciones = null;
            await context.LoadStoredProc("sgsnet_sps_cotizacion_por_numero")
                 .WithSqlParam("numeroDocumento", clienteId)
                 .WithSqlParam("numeroCotizacion", clienteId)
                 .ExecuteStoredProcAsync(async (handler) =>
                 {
                     _cotizaciones = handler.ReadToList<Cotizacion>();
                     await handler.NextResultAsync();

                 });            
            return _cotizaciones.FirstOrDefault();
        }

        public Task Insertar(Dictionary<string, StoredProcedure> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cotizacion> Listar(int page, int pageSize, out int pageCount)
        {
            throw new NotImplementedException();
        }
    }
}
