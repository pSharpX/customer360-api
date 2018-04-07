using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface ICotizacionService : IServiceBase<CotizacionDTO>
    {        
        IEnumerable<CotizacionDTO> Listar(int pageNo, int pageSize, out int pageCount);
        IEnumerable<CotizacionDTO> Buscar(int tipofiltro, string textofiltro);
        Task<CotizacionDTO> BuscarPorCodigoAsync(string clienteId, string codigoId);
        Task<IEnumerable<CotizacionDTO>> BuscarPorClienteAsync(string clienteId);       
        Task<ArchivoReporte> ExportarAsync(string clienteId);
    }
}
