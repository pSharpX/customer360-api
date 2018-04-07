using AutoMapper;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Contracts.Repository.SGAPROD;
using Gildemeister.Cliente360.Domain.SGAPROD;
using Gildemeister.Cliente360.Transport;
using Gildemeister.Cliente360.Transport.SGAPROD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gildemeister.Cliente360.Application
{
    public class AsesorService : IServiceBase<PersonaAsesorDTO>, IAsesorService
    {
        private IAsesorRepository asesorRepository;
        private IMapper _mapper;

        public AsesorService(IAsesorRepository _asesorRepository, IMapper mapper)
        {
            this.asesorRepository = _asesorRepository;
            this._mapper = mapper;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonaAsesorDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PersonaAsesorDTO> GetById(int ind)
        {
            throw new NotImplementedException();
        }

        public Task Insert(PersonaAsesorDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonaAsesorDTO> ListarAsesorComercial()
        {
            try
            {
                IEnumerable<PersonaAsesor> _asesores = asesorRepository.ListarAsesorComercial();
                IEnumerable<PersonaAsesorDTO> _asesoresDTO = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(_asesores);
                return _asesoresDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonaAsesorDTO> ListarAsesorComercialPorPuntoVenta(int nid_punto_venta)
        {
            try
            {
                IEnumerable<PersonaAsesor> _asesores = asesorRepository.ListarAsesorComercialPorPuntoVenta(nid_punto_venta);
                IEnumerable<PersonaAsesorDTO> _asesoresDTO = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(_asesores);
                return _asesoresDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonaAsesorDTO> ListarAsesorServicio()
        {
            try
            {
                IEnumerable<PersonaAsesor> _asesores = asesorRepository.ListarAsesorServicio();
                IEnumerable<PersonaAsesorDTO> _asesoresDTO = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(_asesores);
                return _asesoresDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PersonaAsesorDTO> ListarAsesorVendedor()
        {
            try
            {
                IEnumerable<PersonaAsesor> _asesores = asesorRepository.ListarAsesorVendedor();
                IEnumerable<PersonaAsesorDTO> _asesoresDTO = _mapper.Map<IEnumerable<PersonaAsesor>, IEnumerable<PersonaAsesorDTO>>(_asesores);
                return _asesoresDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task Update(PersonaAsesorDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
