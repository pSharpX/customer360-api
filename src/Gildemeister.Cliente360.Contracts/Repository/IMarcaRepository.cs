using Gildemeister.Cliente360.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IMarcaRepository
    {
        IEnumerable<Marca> Listar();
    }
}
