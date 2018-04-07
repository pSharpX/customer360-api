using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.SGAProd.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gildemeister.SGAProd.Persistence.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SGAProdDbContext sGAProdDbContext)
            :base(sGAProdDbContext)
        {

        }

        public Usuario BuscarUsuario(string usuario)
        {
            IEnumerable<Usuario> list = new List<Usuario>();

            context.LoadStoredProc("sganet_sps_usuario")
               .WithSqlParam("usuario", usuario)
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<Usuario>();
                  handler.NextResult();

              });

            return list.FirstOrDefault();
        }

        public Usuario UsuarioDatos(string usuario)
        {
            IEnumerable<Usuario> list = new List<Usuario>();

            context.LoadStoredProc("sganet_sps_usuario")
               .WithSqlParam("usuario", usuario)
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<Usuario>();
                  handler.NextResult();

              });

            return list.FirstOrDefault();
        }

        public IEnumerable<Usuario> UsuarioRol(string usuario)
        {
            IEnumerable<Usuario> list = new List<Usuario>();

            context.LoadStoredProc("sganet_sps_usuario_rol")
               .WithSqlParam("usuario", usuario)
              .ExecuteStoredProc((handler) =>
              {
                  list = handler.ReadToList<Usuario>();
                  handler.NextResult();

              });

            return list;
        }

       
    }
}
