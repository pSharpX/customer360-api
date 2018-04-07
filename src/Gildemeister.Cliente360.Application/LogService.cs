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
    public interface ILogService : IServiceBase<LogDTO>
    {

    }
    public class LogService : IServiceBase<LogDTO>, ILogService
    {
        private ILogRepository logRepository;
        private IMapper mapper;

        public LogService(ILogRepository logRepository,
            IMapper mapper)
        {
            this.logRepository = logRepository;
            this.mapper = mapper;
        }
        public async Task Insert(LogDTO entity)
        {
            try
            {
                Log log = mapper.Map<Log>(entity);
                await logRepository.Insert(log);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(LogDTO entity)
        {
            try
            {
                Log log = mapper.Map<Log>(entity);
                await logRepository.Update(log);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LogDTO> GetById(int ind)
        {
            try
            {
                Log log = await logRepository.GetById(ind);
                LogDTO logDTO = mapper.Map<LogDTO>(log);
                return logDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task Delete(int id)
        {
            try
            {
                await logRepository.Delete(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<LogDTO> GetAll()
        {
            throw new NotImplementedException();
        }

     
      
    }
}
