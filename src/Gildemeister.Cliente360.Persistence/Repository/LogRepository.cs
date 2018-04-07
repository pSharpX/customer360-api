using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Gildemeister.Cliente360.Persistence;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    
    public class LogRepository : RepositoryBase<Log>
    {
        public LogRepository(Cliente360DbContext cliente360DbContext) 
            : base(cliente360DbContext)
        {
        }
    }
}
