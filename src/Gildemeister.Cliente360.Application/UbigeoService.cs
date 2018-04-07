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
    public interface IUbigeoService:IServiceBase<UbigeoDTO>
    {
        IEnumerable<UbigeoDTO> ListarDepartamento();

        IEnumerable<UbigeoDTO> ListarProvincia(string departamento);

        IEnumerable<UbigeoDTO> ListarDistrito(string departamento, string provincia);
    }
    public class UbigeoService : IServiceBase<UbigeoDTO>, IUbigeoService
    {
        private IUbigeoRepository ubigeoRepository;
        private IMapper mapper;

        public UbigeoService(IUbigeoRepository ubigeoRepository,
            IMapper mapper)
        {
            this.ubigeoRepository = ubigeoRepository;
            this.mapper = mapper;
        }
        public async Task Insert(UbigeoDTO entity)
        {
            try
            {
                Ubigeo ubigeo = mapper.Map<Ubigeo>(entity);
                await ubigeoRepository.Insert(ubigeo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(UbigeoDTO entity)
        {
            try
            {
                Ubigeo ubigeo = mapper.Map<Ubigeo>(entity);
                await ubigeoRepository.Update(ubigeo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UbigeoDTO> GetById(int ind)
        {
            try
            {
                Ubigeo ubigeo = await ubigeoRepository.GetById(ind);
                UbigeoDTO ubigeoDTO = mapper.Map<UbigeoDTO>(ubigeo);
                return ubigeoDTO;
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

        public IEnumerable<UbigeoDTO> GetAll()
        {
            try
            {
                IEnumerable<Ubigeo> ubigeoList = ubigeoRepository.GetAll();
                IEnumerable<UbigeoDTO> ubigeoDTOList = mapper.Map<IEnumerable<Ubigeo>, IEnumerable<UbigeoDTO>>(ubigeoList);

                return ubigeoDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<UbigeoDTO> ListarDepartamento()
        {
            try
            {
                IEnumerable<Ubigeo> ubigeoList = ubigeoRepository.ListarDepartamento();
                IEnumerable<UbigeoDTO> ubigeoDTOList = mapper.Map<IEnumerable<Ubigeo>, IEnumerable<UbigeoDTO>>(ubigeoList);

                return ubigeoDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<UbigeoDTO> ListarProvincia(string departamento)
        {
            try
            {
                IEnumerable<Ubigeo> ubigeoList = ubigeoRepository.ListarProvincia(departamento);
                IEnumerable<UbigeoDTO> ubigeoDTOList = mapper.Map<IEnumerable<Ubigeo>, IEnumerable<UbigeoDTO>>(ubigeoList);

                return ubigeoDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<UbigeoDTO> ListarDistrito(string departamento, string provincia)
        {
            try
            {
                IEnumerable<Ubigeo> ubigeoList = ubigeoRepository.ListarDistrito(departamento, provincia);
                IEnumerable<UbigeoDTO> ubigeoDTOList = mapper.Map<IEnumerable<Ubigeo>, IEnumerable<UbigeoDTO>>(ubigeoList);

                return ubigeoDTOList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
