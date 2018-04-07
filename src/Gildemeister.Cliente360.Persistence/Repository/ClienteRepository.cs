using Gildemeister.Cliente360.Contracts;
using Gildemeister.Cliente360.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Contracts.Repository;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(Cliente360DbContext context)
            : base(context)
        {
        }
        public IEnumerable<Cliente> BuscarCliente(int tipofiltro, string textofiltro)
        {
            IEnumerable<Cliente> clienteList = new List<Cliente>();

            context.LoadStoredProc("sganet_sps_cliente")
                 .WithSqlParam("vi_qt_tipofiltro", tipofiltro)
                 .WithSqlParam("vi_tx_desfiltro", textofiltro)
                 .ExecuteStoredProc((handler) =>
                 {
                     clienteList = handler.ReadToList<Cliente>();
                     handler.NextResult();

                 });

            return clienteList;
        }

        public IEnumerable<Cliente> GetCustomer(int page, int pageSize, out int pageCount)
        {
            try
            {
                pageCount = 0;
                return null;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Cliente> ListarCliente(int page, int pageSize, out int pageCount)
        {
            IEnumerable<Cliente> clienteList = new List<Cliente>();

            DbParameter outputParam = null;

            context.LoadStoredProc("sp_cliente_paging")
                 .WithSqlParam("PageNumber", page)
                 .WithSqlParam("RowspPage", pageSize)
                 .WithSqlParam("TotalRecords", (dbParam) =>
                 {
                     dbParam.Direction = ParameterDirection.Output;
                     dbParam.DbType = DbType.Int32;
                     outputParam = dbParam;
                 })
                 .ExecuteStoredProc((handler) =>
                 {
                     clienteList = handler.ReadToList<Cliente>();
                     handler.NextResult();

                 });

            pageCount = (int)outputParam?.Value;

            return clienteList;
        }

        public IEnumerable<Cliente> BuscarPorCodigo(int clienteId)
        {
            IEnumerable<Cliente> clienteList = new List<Cliente>();

            context.LoadStoredProc("sgsnet_sps_cliente_por_codigo")
                 .WithSqlParam("clienteId", clienteId)
                 .ExecuteStoredProc((handler) =>
                 {
                     clienteList = handler.ReadToList<Cliente>();
                     handler.NextResult();
                 });

            return clienteList;
        }

        public async Task ActualizarCliente(Dictionary<string, StoredProcedure> parameters)
        {
            await context.ExecuteNonQueryAsync("sgsnet_spu_cliente_actualizar", parameters);
        }

        public async Task InsertarCliente(Dictionary<string, StoredProcedure> parameters)
        {
            await context.ExecuteNonQueryAsync("", null);
        }

        public IEnumerable<Cliente> BuscarClienteAdvanced(DataTable tbClientes, string marca, string modelo, string departamento,
            string provincia, string distrito, bool usafiltro1, bool? porventavehiculo, bool? porservicio, bool? porrepuesto)
        {
            IEnumerable<Cliente> clienteList = new List<Cliente>();

            context.LoadStoredProc("sganet_sps_clienteadvanced")
                 .WithSqlParam("vi_tb_clientes", tbClientes)
                 .WithSqlParam("vi_tx_marca", marca)
                 .WithSqlParam("vi_tx_modelo", modelo)
                 .WithSqlParam("vi_tx_departamento", departamento)
                 .WithSqlParam("vi_tx_provincia", provincia)
                 .WithSqlParam("vi_tx_distrito", distrito)
                 .WithSqlParam("vi_tx_usefiltro1", usafiltro1)
                 .WithSqlParam("vi_tx_useventavehiculo", porventavehiculo)
                 .WithSqlParam("vi_tx_useservicio", porservicio)
                 .WithSqlParam("vi_tx_repuesto", porrepuesto)
                 .ExecuteStoredProc((handler) =>
                 {
                     clienteList = handler.ReadToList<Cliente>();
                     handler.NextResult();

                 });

            return clienteList;
        }       
    }
}
