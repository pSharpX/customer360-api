using System;
using System.Collections.Generic;
using System.Text;
using Gildemeister.DWProd.Persistence.Database;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Domain;
using System.Threading.Tasks;
using Gildemeister.Cliente360.Contracts.Repository.DWProd;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gildemeister.DWProd.Persistence.Repository
{
    public class ClienteDWProdRepository : RepositoryBase<Cliente>, IClienteDWProdRepository
    {
        public ClienteDWProdRepository(DWProdDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Cliente> BuscarClienteAdvancedDWProd(string anioFabricacion, string anioModelo,
            string sucursal, string asesorComercial, string fechaEntregaDe, string fechaEntregaHasta,
            string asesorServicio, string fechaServicioDe, string fechaServicioHasta,
            string asesorVendedor, string fechaVentaDe, string fechaVentaHasta, string vin)
        {
            IEnumerable<Cliente> clienteList = new List<Cliente>();
            try
            {

                context.LoadStoredProc("dw_sps_clienteadvanced")
                     .WithSqlParam("vi_tx_aniofab", anioFabricacion)
                     .WithSqlParam("vi_tx_aniomodelo", anioModelo)
                     .WithSqlParam("vi_tx_sucursal", sucursal)
                     .WithSqlParam("vi_tx_asesorcomercial", asesorComercial)
                     .WithSqlParam("vi_tx_fechaentregaDe", fechaEntregaDe)
                     .WithSqlParam("vi_tx_fechaentregaHasta", fechaEntregaHasta)
                     .WithSqlParam("vi_tx_asesorservicio", asesorServicio)
                     .WithSqlParam("vi_tx_fechaservicioDe", fechaServicioDe)
                     .WithSqlParam("vi_tx_fechaservicioHasta", fechaServicioHasta)
                     .WithSqlParam("vi_tx_asesorvendedor", asesorVendedor)
                     .WithSqlParam("vi_tx_fechaventaDe", fechaVentaDe)
                     .WithSqlParam("vi_tx_fechaventaHasta", fechaVentaHasta)
                     .WithSqlParam("vi_tx_vin", vin)
                     .ExecuteStoredProc((handler) =>
                     {
                         clienteList = handler.ReadToList<Cliente>();
                         handler.NextResult();
                     });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clienteList;
        }
    }
}
