using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario BuscarUsuario(string usuario);

        IEnumerable<Usuario> UsuarioRol(string usuario);

        Usuario UsuarioDatos(string usuario);
    }
}
