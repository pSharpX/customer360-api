using AutoMapper;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IProcesoEjecucionService : IServiceBase<ProcesoEjecucionDTO>
    {

    }
    public class ProcesoEjecucionService : IServiceBase<ProcesoEjecucionDTO>, IProcesoEjecucionService
    {
        private IProcesoEjecucionRepository procesoEjecucionRepository;
        private IMapper mapper;

        public ProcesoEjecucionService(IProcesoEjecucionRepository procesoEjecucionRepository,
            IMapper mapper)
        {
            this.procesoEjecucionRepository = procesoEjecucionRepository;
            this.mapper = mapper;
        }
        public Task Insert(ProcesoEjecucionDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProcesoEjecucionDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProcesoEjecucionDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProcesoEjecucionDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

       
    }
}
