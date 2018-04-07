
namespace Gildemeister.Cliente360.Persistence.Repository
{
    using Gildemeister.Cliente360.Contracts.Repository;
    using Gildemeister.Cliente360.Domain;
    using Gildemeister.Cliente360.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TablaGeneralRepository: RepositoryBase<TablaGeneral>, ITablaGeneralRepository
    {
        public TablaGeneralRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }
    }
}
