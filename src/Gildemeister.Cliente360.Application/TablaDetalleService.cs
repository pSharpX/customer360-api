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
    public interface ITablaDetalleService:IServiceBase<TablaDetalleDTO>
    {
        IEnumerable<TablaDetalleDTO> ListarEstadoCivil();

        IEnumerable<TablaDetalleDTO> ListarGenero();
    }
    public class TablaDetalleService : IServiceBase<TablaDetalleDTO>, ITablaDetalleService
    {
        private ITablaDetalleRepository tablaDetalleRepository;
        private IMapper mapper;

        public TablaDetalleService(ITablaDetalleRepository tablaDetalleRepository,
            IMapper mapper)
        {
            this.tablaDetalleRepository = tablaDetalleRepository;
            this.mapper = mapper;
        }
        public async Task Insert(TablaDetalleDTO entity)
        {
            try
            {
                TablaDetalle tablaDetalle = mapper.Map<TablaDetalle>(entity);
                await tablaDetalleRepository.Insert(tablaDetalle);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(TablaDetalleDTO entity)
        {
            try
            {
                TablaDetalle tablaDetalle = mapper.Map<TablaDetalle>(entity);
                await tablaDetalleRepository.Update(tablaDetalle);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<TablaDetalleDTO> GetById(int ind)
        {
            try
            {
                TablaDetalle tablaDetalle = await tablaDetalleRepository.GetById(ind);
                TablaDetalleDTO tablaDetalleDTO = mapper.Map<TablaDetalleDTO>(tablaDetalle);
                return tablaDetalleDTO;
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

        public IEnumerable<TablaDetalleDTO> GetAll()
        {
            try
            {
                IEnumerable<TablaDetalle> tablaDetalleList = tablaDetalleRepository.GetAll();
                IEnumerable<TablaDetalleDTO> tablaDetalleDTOList = mapper.Map<IEnumerable<TablaDetalle>, IEnumerable<TablaDetalleDTO>>(tablaDetalleList);

                return tablaDetalleDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<TablaDetalleDTO> ListarEstadoCivil()
        {
            try
            {
                IEnumerable<TablaDetalle> tablaDetalleList = tablaDetalleRepository.ListarEstadoCivil();
                IEnumerable<TablaDetalleDTO> tablaDetalleDTOList = mapper.Map<IEnumerable<TablaDetalle>, IEnumerable<TablaDetalleDTO>>(tablaDetalleList);

                return tablaDetalleDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<TablaDetalleDTO> ListarGenero()
        {
            try
            {
                IEnumerable<TablaDetalle> tablaDetalleList = tablaDetalleRepository.ListarGenero();
                IEnumerable<TablaDetalleDTO> tablaDetalleDTOList = mapper.Map<IEnumerable<TablaDetalle>, IEnumerable<TablaDetalleDTO>>(tablaDetalleList);

                return tablaDetalleDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
