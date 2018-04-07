using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Persistence.Repository
{
    public class UbigeoRepository : RepositoryBase<Ubigeo>, IUbigeoRepository
    {
        public UbigeoRepository(Cliente360DbContext cliente360DbContext)
            : base(cliente360DbContext)
        {

        }

        public IEnumerable<Ubigeo> ListarDepartamento()
        {
            IEnumerable<Ubigeo> listData = new List<Ubigeo>();

            context.LoadStoredProc("sgsnet_sps_departamento")
                 .ExecuteStoredProc((handler) =>
                 {
                     listData = handler.ReadToList<Ubigeo>();
                     handler.NextResult();
                 });

            return listData;
        }

        public IEnumerable<Ubigeo> ListarProvincia(string departamento)
        {
            IEnumerable<Ubigeo> listData = new List<Ubigeo>();

            context.LoadStoredProc("sgsnet_sps_provincia")
                 .WithSqlParam("departamento", departamento)
                 .ExecuteStoredProc((handler) =>
                 {
                     listData = handler.ReadToList<Ubigeo>();
                     handler.NextResult();
                 });

            return listData;
        }

        public IEnumerable<Ubigeo> ListarDistrito(string departamento,
            string provincia)
        {
            IEnumerable<Ubigeo> listData = new List<Ubigeo>();

            context.LoadStoredProc("sgsnet_sps_distrito")
                 .WithSqlParam("departamento", departamento)
                 .WithSqlParam("provincia", provincia)
                 .ExecuteStoredProc((handler) =>
                 {
                     listData = handler.ReadToList<Ubigeo>();
                     handler.NextResult();

                 });

            return listData;
        }


    }
}
