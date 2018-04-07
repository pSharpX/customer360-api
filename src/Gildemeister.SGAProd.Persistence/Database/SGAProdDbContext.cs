using Gildemeister.Cliente360.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.SGAProd.Persistence.Database
{
    public class SGAProdDbContext : DbContext
    {
        public SGAProdDbContext(DbContextOptions<SGAProdDbContext> options)
            : base(options)
        {
        }

 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
