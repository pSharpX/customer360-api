using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;

using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IContactoService : IServiceBase<ContactoDTO>
    {

    }
    public class ContactoService:IServiceBase<ContactoDTO>, IContactoService
    {
        private IContactoRepository contactoRepository;
        private IMapper mapper;
        public ContactoService(IContactoRepository contactoRepository,
            IMapper mapper)
        {
            this.contactoRepository = contactoRepository;
            this.mapper = mapper;
        }
        public Task Insert(ContactoDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ContactoDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContactoDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ContactoDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

    }
}
