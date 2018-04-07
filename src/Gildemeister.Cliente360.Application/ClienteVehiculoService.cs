using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IClienteVehiculoService : IServiceBase<ClienteVehiculoDTO>
    {

    }
    public class ClienteVehiculoService:IServiceBase<ClienteVehiculoDTO>
    {
        private IClienteVehiculoRepository clienteVehiculoRepository;
        private IMapper mapper;
        public ClienteVehiculoService(IClienteVehiculoRepository clienteVehiculoRepository,
            IMapper mapper)
        {
            this.clienteVehiculoRepository = clienteVehiculoRepository;
            this.mapper = mapper;
        }
        public Task Insert(ClienteVehiculoDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ClienteVehiculoDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteVehiculoDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ClienteVehiculoDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

    }
}
