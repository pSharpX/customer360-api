using AutoMapper;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Domain;
using Gildemeister.Cliente360.Transport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public class ApplicacionLlaveService : IServiceBase<ApplicacionLlaveDTO>, IApplicacionLlaveService
    {
        private IApplicacionLlaveRepository applicacionLlaveRepository;
        private IMapper mapper;

        public ApplicacionLlaveService(IApplicacionLlaveRepository applicacionLlaveRepository,
            IMapper mapper)
        {
            this.applicacionLlaveRepository = applicacionLlaveRepository;
            this.mapper = mapper;
        }
        public async Task Insert(ApplicacionLlaveDTO entity)
        {
            try
            {
                ApplicacionLlave applicacionLlave = mapper.Map<ApplicacionLlave>(entity);
                await applicacionLlaveRepository.Insert(applicacionLlave);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(ApplicacionLlaveDTO entity)
        {
            try
            {
                ApplicacionLlave applicacionLlave = mapper.Map<ApplicacionLlave>(entity);
                await applicacionLlaveRepository.Update(applicacionLlave);
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

        public IEnumerable<ApplicacionLlaveDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicacionLlaveDTO> GetById(int ind)
        {
            try
            {
                ApplicacionLlave applicacionLlave = await applicacionLlaveRepository.GetById(ind);
                ApplicacionLlaveDTO applicacionLlaveDTO = mapper.Map<ApplicacionLlaveDTO>(applicacionLlave);
                return applicacionLlaveDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ApplicacionLlaveDTO LlaveApplicacion(string llave)
        {
            try
            {
                ApplicacionLlave model = applicacionLlaveRepository.LlaveApplicacion(llave);
                ApplicacionLlaveDTO applicacionLlaveDTO = mapper.Map<ApplicacionLlaveDTO>(model);
             
                return applicacionLlaveDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
