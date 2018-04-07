using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Application
{
    public interface IUsuarioService : IServiceBase<UsuarioDTO>
    {
        UsuarioDTO BuscarUsuario(string usuario);

        IEnumerable<UsuarioDTO> UsuarioRol(string usuario);

        UsuarioDTO UsuarioDatos(string usuario);
    }

}
