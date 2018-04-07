using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Contracts.Repository
{
    public interface IUbigeoRepository : IRepositoryBase<Ubigeo>
    {
        IEnumerable<Ubigeo> ListarDepartamento();

        IEnumerable<Ubigeo> ListarProvincia(string departamento);

        IEnumerable<Ubigeo> ListarDistrito(string departamento, string provincia);
    }
}
