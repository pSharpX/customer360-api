using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IVentaRepository : IRepositoryBase<Venta>
    {        
        Task<IEnumerable<Venta>> Listar(int pageNo, int pageSize, out int pageCount);
        Task<IEnumerable<Venta>> Buscar(int tipofiltro, string textofiltro);
        Task<Venta> BuscarPorCodigoAsync(string numeroDocumento, string codigo);
        Task<IEnumerable<Venta>> BuscarPorClienteAsync(string clienteDNI);
    }
}
