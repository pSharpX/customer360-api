using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IPersonaDireccionService: IServiceBase<PersonaDireccionDTO>
    {

    }
    public class PersonaDireccionService : IServiceBase<PersonaDireccionDTO>, IPersonaDireccionService
    {
        private IPersonaDireccionRepository personaDireccionRepository;

        public PersonaDireccionService(IPersonaDireccionRepository personaDireccionRepository)
        {
            this.personaDireccionRepository = personaDireccionRepository;
        }
        public Task Insert(PersonaDireccionDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(PersonaDireccionDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonaDireccionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PersonaDireccionDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

       
    }
}
