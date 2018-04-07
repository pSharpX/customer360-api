using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gildemeister.Cliente360.Contracts.Repository.DWProd
{
    public interface IClienteDWProdRepository : IRepositoryBase<Cliente>
    {
        IEnumerable<Cliente> BuscarClienteAdvancedDWProd(string anioFabricacion, string anioModelo, string sucursal, string asesorComercial, string fechaEntregaDe, string fechaEntregaHasta,
            string asesorServicio, string fechaServicioDe, string fechaServicioHasta,
            string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin);
    }
}
