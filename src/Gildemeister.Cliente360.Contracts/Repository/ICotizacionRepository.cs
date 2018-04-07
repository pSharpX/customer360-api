using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface ICotizacionRepository : IRepositoryBase<Cotizacion>
    {        
        IEnumerable<Cotizacion> Listar(int page, int pageSize, out int pageCount);
        IEnumerable<Cotizacion> Buscar(int tipofiltro, string textofiltro);
        Task<Cotizacion> BuscarPorCodigoAsync(string clienteId, string cotizacionId);
        Task<IEnumerable<Cotizacion>> BuscarPorClienteAsync(string clienteId);
        Task Insertar(Dictionary<string, StoredProcedure> parameters);
        Task Actualizar(Dictionary<string, StoredProcedure> parameters);
    }
}
