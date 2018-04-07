using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface IVentaService : IServiceBase<VentaDTO>
    {
        IEnumerable<VentaDTO> Listar(int pageNo, int pageSize, out int pageCount);
        IEnumerable<VentaDTO> Buscar(int tipofiltro, string textofiltro);
        Task<VentaDTO> BuscarPorCodigoAsync(string clienteId, string codigoId);
        Task<IEnumerable<VentaDTO>> BuscarPorClienteAsync(string clienteId);
        Task<ArchivoReporte> ExportarAsync(string clienteId);
    }
}
