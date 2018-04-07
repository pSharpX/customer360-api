using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IVehiculoService:IServiceBase<VehiculoDTO>
    {

    }
    public class VehiculoService : IServiceBase<VehiculoDTO>, IVehiculoService
    {
        private IVehiculoRepository vehiculoRepository;
        private IMapper mapper;

        public VehiculoService(IVehiculoRepository vehiculoRepository,
            IMapper mapper)
        {
            this.vehiculoRepository = vehiculoRepository;
            this.mapper = mapper;
        }
        public Task Insert(VehiculoDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(VehiculoDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehiculoDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<VehiculoDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }


    }
}
