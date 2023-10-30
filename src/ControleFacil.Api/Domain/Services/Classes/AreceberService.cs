using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class AreceberService : IService<AreceberRequestContract, AreceberResponseContract, long>
    {

        private readonly IAreceberRepository _areceberRepository;
        private readonly IMapper _mapper;

        public AreceberService(
            IAreceberRepository areceberRepository,
            IMapper mapper
        )
        {
            this._areceberRepository = areceberRepository;
            this._mapper = mapper;
        }


        public async Task<AreceberResponseContract> Adicionar(
            AreceberRequestContract entidade,
            long idUsuario)
        {
            Areceber Areceber = _mapper.Map<Areceber>(entidade);

            Areceber.DataCadastro = DateTime.Now;
            Areceber.IdUsuario = idUsuario;

            // poderia ter uma validação aqui para checar
            //  se tem tudo que eu preciso esta no meu contrato

            Areceber = await _areceberRepository.Adicionar(Areceber);

            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(
            long id,
             AreceberRequestContract entidade,
             long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map<Areceber>(entidade);
            contrato.Id = areceber.Id;
            contrato.IdUsuario = areceber.IdUsuario;
            contrato.DataCadastro = areceber.DataCadastro;

            contrato = await _areceberRepository.Atualizar(contrato);

            return _mapper.Map<AreceberResponseContract>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoIdUsuario(id, idUsuario);

            await _areceberRepository.Deletar(areceber);
        }

        public async Task<IEnumerable<AreceberResponseContract>> Obter(long idUsuario)
        {
            var titulosAreceber = await _areceberRepository.ObterPeloIdUsuario(idUsuario);

            return titulosAreceber.Select(titulo => _mapper.Map<AreceberResponseContract>(titulo));
        }

        public async Task<AreceberResponseContract> Obter(long id, long idUsuario)
        {
            Areceber areceber = await ObterPorIdVinculadoIdUsuario(id, idUsuario);
            return _mapper.Map<AreceberResponseContract>(areceber);
        }

        private async Task<Areceber> ObterPorIdVinculadoIdUsuario(long id, long idUsuario)
        {

            var areceber = await _areceberRepository.Obter(id);
            if (areceber is null || areceber.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi enconrada nenhum título Areceber pelo id {id}");
            }

            return areceber;
        }
    }
}