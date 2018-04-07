using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class PaisRepository : RepositoryBase<Pais>, IPaisRepository
    {
        public PaisRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<Pais> Listar()
        {
            IEnumerable<Pais> paisList = new List<Pais>();

            context.LoadStoredProc("sgsnet_sps_pais")
                 .ExecuteStoredProc((handler) =>
                 {
                     paisList = handler.ReadToList<Pais>();
                     handler.NextResult();

                 });

            return paisList;
        }
    }
}
