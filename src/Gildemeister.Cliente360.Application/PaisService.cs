using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IPaisService : IServiceBase<PaisDTO>
    {

    }
    public  class PaisService:IServiceBase<PaisDTO>, IPaisService
    {
        private IPaisRepository paisRepository;
        private IMapper mapper;
        public PaisService(IPaisRepository paisRepository,
            IMapper mapper)
        {
            this.paisRepository = paisRepository;
            this.mapper = mapper;
        }
        public Task Insert(PaisDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(PaisDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaisDTO> GetAll()
        {
            try
            {
                IEnumerable<Pais> paisList = paisRepository.Listar();
                IEnumerable<PaisDTO> paisDTOList = mapper.Map<IEnumerable<Pais>, IEnumerable<PaisDTO>>(paisList);

                return paisDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<PaisDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

       
    }
}
