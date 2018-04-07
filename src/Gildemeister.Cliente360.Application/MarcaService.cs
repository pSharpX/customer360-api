using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IMarcaService : IServiceBase<MarcaDTO>
    {

    }
    public class MarcaService : IServiceBase<MarcaDTO>, IMarcaService
    {
        private IMarcaRepository marcaRepository;
        private IMapper mapper;
        public MarcaService(IMarcaRepository marcaRepository,
            IMapper mapper)
        {
            this.marcaRepository = marcaRepository;
            this.mapper = mapper;
        }
        public Task Insert(MarcaDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(MarcaDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MarcaDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MarcaDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

    
    }
}
