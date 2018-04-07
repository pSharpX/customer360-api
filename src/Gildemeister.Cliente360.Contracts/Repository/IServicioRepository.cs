using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IServicioRepository : IRepositoryBase<Servicio>
    {
        Task<IEnumerable<Servicio>> Listar(int pageNo, int pageSize, out int pageCount);
        Task<IEnumerable<Servicio>> Buscar(int tipofiltro, string textofiltro);
        Task<Servicio> BuscarPorCodigoAsync(string numeroDocumento, string codigo);
        Task<IEnumerable<Servicio>> BuscarPorClienteAsync(string clienteDNI);
    }
}
