using AutoMapper;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public interface IModeloService : IServiceBase<ModeloDTO>
    {

    }
    public class ModeloService : IServiceBase<ModeloDTO>, IModeloService
    {
        private IModeloRepository modeloRepository;
        private IMapper mapper;
        public ModeloService(IModeloRepository modeloRepository,
            IMapper mapper)
        {
            this.modeloRepository = modeloRepository;
            this.mapper = mapper;
        }
        public Task Insert(ModeloDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ModeloDTO entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModeloDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ModeloDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }


    }
}
