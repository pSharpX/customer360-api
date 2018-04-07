using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class MarcaRepository : RepositoryBase<Marca>, IMarcaRepository
    {
        public MarcaRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<Marca> Listar()
        {
            IEnumerable<Marca> marcaList = new List<Marca>();

            context.LoadStoredProc("sgsnet_sps_marca")
                 .ExecuteStoredProc((handler) =>
                 {
                     marcaList = handler.ReadToList<Marca>();
                     handler.NextResult();

                 });

            return marcaList;
        }
    }
}
