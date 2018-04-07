using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class PuntoVentaRepository : RepositoryBase<PuntoVenta>, IPuntoVentaRepository
    {
        public PuntoVentaRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<PuntoVenta> Listar()
        {
            IEnumerable<PuntoVenta> puntoventaList = new List<PuntoVenta>();

            context.LoadStoredProc("sgsnet_sps_puntoventa")
                 .ExecuteStoredProc((handler) =>
                 {
                     puntoventaList = handler.ReadToList<PuntoVenta>();
                     handler.NextResult();

                 });

            return puntoventaList;
        }
    }
}
