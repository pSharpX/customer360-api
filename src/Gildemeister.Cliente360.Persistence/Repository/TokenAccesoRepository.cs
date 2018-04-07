using Gildemeister.Cliente360.Contracts.Repository.DWProd;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence
{
    public class TokenAccesoRepository:RepositoryBase<TokenAcceso>, ITokenAccesoRepository
    {

        public TokenAccesoRepository(Cliente360DbContext cliente360DbContext)
            :base(cliente360DbContext)
        {

        }



    }
}
