using AutoMapper;
using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

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
            Validar(entidade);
            Areceber Areceber = _mapper.Map<Areceber>(entidade);

            Areceber.DataCadastro = DateTime.Now;
            Areceber.IdUsuario = idUsuario;

            Areceber = await _areceberRepository.Adicionar(Areceber);

            return _mapper.Map<AreceberResponseContract>(Areceber);
        }

        public async Task<AreceberResponseContract> Atualizar(
            long id,
             AreceberRequestContract entidade,
             long idUsuario)
        {
            Validar(entidade);
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
                throw new NotFoundException($"Não foi enconrada nenhum título Areceber pelo id {id}");
            }

            return areceber;
        }

        private void Validar(AreceberRequestContract entidade)
        {
            if (entidade.ValorOriginal < 0 || entidade.ValorRecebido < 0)
            {
                throw new BadRequestException("Os campos ValorOriginal e ValorRecebimento não podem ser negativos.");
            }
        }
    }
}