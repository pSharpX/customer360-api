using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class ModeloRepository : RepositoryBase<Modelo>, IModeloRepository
    {
        public ModeloRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<Modelo> Listar()
        {
            IEnumerable<Modelo> modeloList = new List<Modelo>();

            context.LoadStoredProc("sgsnet_sps_modelo")
                 .ExecuteStoredProc((handler) =>
                 {
                     modeloList = handler.ReadToList<Modelo>();
                     handler.NextResult();

                 });

            return modeloList;
        }
    }
}
