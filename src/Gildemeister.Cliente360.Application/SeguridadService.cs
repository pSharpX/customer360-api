using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface ISeguridadService : IServiceBase<SeguridadDTO>
    {
        IEnumerable<SeguridadDTO> AutenticationUser(string usuario);
    }

    public class SeguridadService : IServiceBase<SeguridadDTO>, ISeguridadService
    {
        public IEnumerable<SeguridadDTO> AutenticationUser(string usuario)
        {
            List<SeguridadDTO> list = new List<SeguridadDTO>();
            list.Add(item: new SeguridadDTO { NombreCompleto = "Juan Perez", Rol = "Asesor Comercial", Usuario = "jperez" });

            return list;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SeguridadDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SeguridadDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(SeguridadDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(SeguridadDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
