using Gildemeister.Cliente360.Common;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class ApplicacionLlaveRepository : RepositoryBase<ApplicacionLlave>, IApplicacionLlaveRepository
    {
         
        public ApplicacionLlaveRepository(Cliente360DbContext cliente360DbContext)
            :base(cliente360DbContext)
        {

        }

        public ApplicacionLlave LlaveApplicacion(string llave)
        {
            var predicate = PredicateBuilder.True<ApplicacionLlave>();

            predicate = predicate.And(x => x.Llave == llave);

            ApplicacionLlave query = dbSet.Where(predicate).FirstOrDefault();

            return query;
        }

       
    }
}
