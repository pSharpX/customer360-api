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
    public interface ITablaGeneralService : IServiceBase<TablaGeneralDTO>
    {
        
    }
    public class TablaGeneralService : IServiceBase<TablaGeneralDTO>, ITablaGeneralService
    {
        private ITablaGeneralRepository tablaGeneralRepository;
        private IMapper _mapper;

        public TablaGeneralService(ITablaGeneralRepository tablaGenalRepository,
            IMapper mapper)
        {
            this.tablaGeneralRepository = tablaGenalRepository;
            this._mapper = mapper;
        }

        public async Task Insert(TablaGeneralDTO entity)
        {
            try
            {
                TablaGeneral tablaGeneral = _mapper.Map<TablaGeneral>(entity);
                await tablaGeneralRepository.Insert(tablaGeneral);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(TablaGeneralDTO entity)
        {
            try
            {
                TablaGeneral tablaGeneral = _mapper.Map<TablaGeneral>(entity);
                await tablaGeneralRepository.Update(tablaGeneral);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TablaGeneralDTO> GetAll()
        {
            try
            {
                IEnumerable<TablaGeneral> tablaGeneralList =  tablaGeneralRepository.GetAll();
                IEnumerable<TablaGeneralDTO> tablaGeneralDTOList = _mapper.Map<IEnumerable<TablaGeneral>, IEnumerable<TablaGeneralDTO>>(tablaGeneralList);

                return tablaGeneralDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TablaGeneralDTO> GetById(int ind)
        {
            try
            {
                TablaGeneral cliente = await tablaGeneralRepository.GetById(ind);
                TablaGeneralDTO tablaGeneralDTO = _mapper.Map<TablaGeneralDTO>(cliente);
                return tablaGeneralDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
    }
}
