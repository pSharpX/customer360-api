using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IClienteService : IServiceBase<ClienteDTO>
    {
        ClienteDTO ListarFilterAdvanced();

        IEnumerable<ClienteDTO> GetCustomer(int page, int pageSize, out int pageCount);

        IEnumerable<ClienteDTO> ListarCliente(int pageNo, int pageSize, out int pageCount);

        IEnumerable<ClienteDTO> BuscarCliente(int tipofiltro, string textofiltro);

        IEnumerable<ClienteDTO> BuscarClienteAdvanced(string aniofabricacion, string aniomodelo, string sucursal, string asesorcomercial,
            string fechaentregaDe, string fechaentregaHasta, string asesorservicio,
            string fechaservicioDe, string fechaservicioHasta, string marca, string modelo,
            string departamento, string provincia, string distrito, bool? porventavehiculo,
            bool? porservicio, bool? porrepuesto, string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin);

        IEnumerable<ClienteDTO> BuscarPorCodigo(int clienteId);

        Task ActualizarCliente(Dictionary<string, StoredProcedure> parameters);

        Task InsertarCliente(Dictionary<string, StoredProcedure> parameters);

        ArchivoReporte ExportarCliente(int tipofiltro, string textofiltro);

        ArchivoReporte ExportarClientesAdvanced(string aniofabricacion, string aniomodelo, string sucursal, string asesorcomercial,
            string fechaentregaDe, string fechaentregaHasta, string asesorservicio,
            string fechaservicioDe, string fechaservicioHasta, string marca, string modelo,
            string departamento, string provincia, string distrito, bool? porventavehiculo,
            bool? porservicio, bool? porrepuesto, string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin);
    }
}
