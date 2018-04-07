using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;

using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IPersonaService : IServiceBase<PersonaDTO>
    {

    }
    public class PersonaService:IServiceBase<PersonaDTO>, IPersonaService
    {
        private IPersonaRepository personaRepository;
        public PersonaService(IPersonaRepository personaRepository)
        {
            this.personaRepository = personaRepository;
        }
        public Task Insert(PersonaDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(PersonaDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonaDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PersonaDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

    
    }
}
