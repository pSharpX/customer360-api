using AutoMapper;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IProcesoConfiguracionService : IServiceBase<ProcesoConfiguracionDTO>
    {

    }
    public class ProcesoConfiguracionService:IServiceBase<ProcesoConfiguracionDTO>, IProcesoConfiguracionService
    {
        private IProcesoConfiguracionRepository procesoConfiguracionRepository;
        private IMapper mapper;

        public ProcesoConfiguracionService(IProcesoConfiguracionRepository procesoConfiguracionRepository,
            IMapper mapper)
        {
            this.procesoConfiguracionRepository = procesoConfiguracionRepository;
            this.mapper = mapper;
        }
        public Task Insert(ProcesoConfiguracionDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProcesoConfiguracionDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProcesoConfiguracionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProcesoConfiguracionDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

      
    }
}
