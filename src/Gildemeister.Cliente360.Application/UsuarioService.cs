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

    public class UsuarioService : IServiceBase<UsuarioDTO>, IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;
        private IMapper mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }

        public async Task Insert(UsuarioDTO entity)
        {
            try
            {
                Usuario cliente = mapper.Map<Usuario>(entity);
                await usuarioRepository.Insert(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(UsuarioDTO entity)
        {
            try
            {
                Usuario cliente = mapper.Map<Usuario>(entity);
                await usuarioRepository.Update(cliente);
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

        public IEnumerable<UsuarioDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDTO> GetById(int ind)
        {
            try
            {
                Usuario cliente = await usuarioRepository.GetById(ind);
                UsuarioDTO usuarioDTO = mapper.Map<UsuarioDTO>(cliente);
                return usuarioDTO;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UsuarioDTO BuscarUsuario(string usuario)
        {
            try
            {
                Usuario usuarioModel = usuarioRepository.BuscarUsuario(usuario);
                UsuarioDTO usuarioDTO = mapper.Map<UsuarioDTO>(usuarioModel);

                return usuarioDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UsuarioDTO> UsuarioRol(string usuario)
        {
            try
            {
                IEnumerable<Usuario> usuarioList = usuarioRepository.UsuarioRol(usuario);
                IEnumerable<UsuarioDTO> usuarioDTOList = mapper.
                    Map<IEnumerable<Usuario>, IEnumerable<UsuarioDTO>>(usuarioList);

                return usuarioDTOList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuarioDTO UsuarioDatos(string usuario)
        {
            try
            {
                Usuario usuarioModel = usuarioRepository.UsuarioDatos(usuario);
                UsuarioDTO usuarioDTO = mapper.Map<UsuarioDTO>(usuarioModel);

                return usuarioDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
