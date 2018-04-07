using AutoMapper;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Contracts.Repository.DWProd;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public class TokenAccesoService : IServiceBase<TokenAccesoDTO>, ITokenAccesoService
    {
        private ITokenAccesoRepository tokenAccesoRepository;
        private IMapper mapper;

        public TokenAccesoService(ITokenAccesoRepository tokenAccesoRepository,
            IMapper mapper)
        {
            this.tokenAccesoRepository = tokenAccesoRepository;
            this.mapper = mapper;
        }

        public async Task Insert(TokenAccesoDTO entity)
        {
            try
            {
                TokenAcceso tokenAcceso = mapper.Map<TokenAcceso>(entity);
                await tokenAccesoRepository.Insert(tokenAcceso);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(TokenAccesoDTO entity)
        {
            try
            {
                TokenAcceso tokenAcceso = mapper.Map<TokenAcceso>(entity);
                await tokenAccesoRepository.Update(tokenAcceso);
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

        public IEnumerable<TokenAccesoDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TokenAccesoDTO> GetById(int ind)
        {
            try
            {
                TokenAcceso tokenAcceso = await tokenAccesoRepository.GetById(ind);
                TokenAccesoDTO tokenAccesoDTO = mapper.Map<TokenAccesoDTO>(tokenAcceso);
                return tokenAccesoDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      
    }
}
