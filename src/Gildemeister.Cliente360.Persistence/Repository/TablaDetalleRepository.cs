using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class TablaDetalleRepository : RepositoryBase<TablaDetalle>, ITablaDetalleRepository
    {
        public TablaDetalleRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<TablaDetalle> ListarEstadoCivil()
        {
            IEnumerable<TablaDetalle> listData = new List<TablaDetalle>();

            context.LoadStoredProc("sgsnet_sps_estado_civil")
                 .ExecuteStoredProc((handler) =>
                 {
                     listData = handler.ReadToList<TablaDetalle>();
                     handler.NextResult();

                 });

            return listData;
        }

        public IEnumerable<TablaDetalle> ListarGenero()
        {
            IEnumerable<TablaDetalle> listData = new List<TablaDetalle>();

            context.LoadStoredProc("sgsnet_sps_estado_genero")
                 .ExecuteStoredProc((handler) =>
                 {
                     listData = handler.ReadToList<TablaDetalle>();
                     handler.NextResult();

                 });

            return listData;
        }
    }
}
