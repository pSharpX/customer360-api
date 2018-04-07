using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Domain.SGAPROD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Contracts.Repository.SGAPROD
{
    public interface IAsesorRepository
    {
        IEnumerable<PersonaAsesor> ListarAsesorComercial();
        IEnumerable<PersonaAsesor> ListarAsesorComercialPorPuntoVenta(int nid_punto_venta);
        IEnumerable<PersonaAsesor> ListarAsesorServicio();
        IEnumerable<PersonaAsesor> ListarAsesorVendedor();
    }
}
