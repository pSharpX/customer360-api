using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Contracts.Repository.SGAPROD;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Domain.SGAPROD;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.SGAProd.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.SGAProd.Persistence.Repository
{
    public class AsesorRepository : RepositoryBase<PersonaAsesor>, IAsesorRepository
    {
        public AsesorRepository(SGAProdDbContext sGAProdDbContext)
            : base(sGAProdDbContext)
        {

        }

        public IEnumerable<PersonaAsesor> ListarAsesorComercial()
        {
            IEnumerable<PersonaAsesor> list = new List<PersonaAsesor>();

            context.LoadStoredProc("sganet_sps_asesorcomercial")
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<PersonaAsesor>();
                  handler.NextResult();

              });

            return list;
        }

        public IEnumerable<PersonaAsesor> ListarAsesorComercialPorPuntoVenta(int nid_punto_venta)
        {
            IEnumerable<PersonaAsesor> list = new List<PersonaAsesor>();

            context.LoadStoredProc("sganet_sps_asesorcomercial_por_punto_venta")
                .WithSqlParam("nid_punto_venta", nid_punto_venta)
                .ExecuteStoredProc((handler) =>
                  {
                      list = handler.ReadToList<PersonaAsesor>();
                      handler.NextResult();

                  });

            return list;
        }

        public IEnumerable<PersonaAsesor> ListarAsesorServicio()
        {
            IEnumerable<PersonaAsesor> list = new List<PersonaAsesor>();

            context.LoadStoredProc("sganet_sps_asesorservicio")
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<PersonaAsesor>();
                  handler.NextResult();

              });

            return list;
        }

        public IEnumerable<PersonaAsesor> ListarAsesorVendedor()
        {
            IEnumerable<PersonaAsesor> list = new List<PersonaAsesor>();

            context.LoadStoredProc("sganet_sps_asesorvendedor")
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<PersonaAsesor>();
                  handler.NextResult();

              });

            return list;
        }
    }
}
