using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;

using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IClienteContactoService : IServiceBase<ClienteContactoDTO>
    {

    }
    public class ClienteContactoService : IServiceBase<ClienteContactoDTO>, IClienteContactoService
    {

        private IClienteContactoRepository clienteContactoRepository;
        private IMapper mapper;

        public ClienteContactoService(IClienteContactoRepository clienteContactoRepository,
            IMapper mapper)
        {
            this.clienteContactoRepository = clienteContactoRepository;
            this.mapper = mapper;
        }
        public Task Insert(ClienteContactoDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ClienteContactoDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteContactoDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ClienteContactoDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

    
    }
}
