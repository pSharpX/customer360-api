using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        IEnumerable<Cliente> GetCustomer(int page, int pageSize, out int pageCount);

        IEnumerable<Cliente> ListarCliente(int page, int pageSize, out int pageCount);

        IEnumerable<Cliente> BuscarCliente(int tipofiltro, string textofiltro);

        IEnumerable<Cliente> BuscarClienteAdvanced(DataTable tbClientes, string marca, string modelo, string departamento,
            string provincia, string distrito, bool usafiltro1, bool? porventavehiculo, bool? porservicio, bool? porrepuesto);

        IEnumerable<Cliente> BuscarPorCodigo(int clienteId);

        Task InsertarCliente(Dictionary<string, StoredProcedure> parameters);

        Task ActualizarCliente(Dictionary<string, StoredProcedure> parameters);

    }
}
