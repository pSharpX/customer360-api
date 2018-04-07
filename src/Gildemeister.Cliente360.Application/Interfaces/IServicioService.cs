using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface IServicioService : IServiceBase<ServicioDTO>
    {
        IEnumerable<ServicioDTO> Listar(int pageNo, int pageSize, out int pageCount);
        IEnumerable<ServicioDTO> Buscar(int tipofiltro, string textofiltro);
        Task<ServicioDTO> BuscarPorCodigoAsync(string clienteId, string codigoId);
        Task<IEnumerable<ServicioDTO>> BuscarPorClienteAsync(string clienteId);
        Task<ArchivoReporte> ExportarAsync(string clienteId);
    }
}
