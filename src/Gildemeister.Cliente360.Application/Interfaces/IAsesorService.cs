using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Transport.SGAPROD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Application.Interfaces
{
    public interface IAsesorService: IServiceBase<PersonaAsesorDTO>
    {
        IEnumerable<PersonaAsesorDTO> ListarAsesorComercial();
        IEnumerable<PersonaAsesorDTO> ListarAsesorComercialPorPuntoVenta(int nid_punto_venta);
        IEnumerable<PersonaAsesorDTO> ListarAsesorServicio();
        IEnumerable<PersonaAsesorDTO> ListarAsesorVendedor();
    }
}
