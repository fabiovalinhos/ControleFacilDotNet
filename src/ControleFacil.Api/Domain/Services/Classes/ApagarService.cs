using AutoMapper;
using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class ApagarService : IService<ApagarRequestContract, ApagarResponseContract, long>
    {

        private readonly IApagarRepository _apagarRepository;
        private readonly IMapper _mapper;

        public ApagarService(
            IApagarRepository apagarRepository,
            IMapper mapper
        )
        {
            this._apagarRepository = apagarRepository;
            this._mapper = mapper;
        }


        public async Task<ApagarResponseContract> Adicionar(
            ApagarRequestContract entidade,
            long idUsuario)
        {
            Apagar Apagar = _mapper.Map<Apagar>(entidade);

            Apagar.DataCadastro = DateTime.Now;
            Apagar.IdUsuario = idUsuario;

            // poderia ter uma validação aqui para checar
            //  se tem tudo que eu preciso esta no meu contrato

            Apagar = await _apagarRepository.Adicionar(Apagar);

            return _mapper.Map<ApagarResponseContract>(Apagar);
        }

        public async Task<ApagarResponseContract> Atualizar(
            long id,
             ApagarRequestContract entidade,
             long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoIdUsuario(id, idUsuario);

            // o que está comentado é uma segunda forma de fazer
            // apagar.Descricao = entidade.Descricao;
            // apagar.Observacao = entidade.Observacao;
            // apagar.ValorOriginal = entidade.ValorOriginal;
            // apagar.ValorPago = entidade.ValorPago;
            // apagar.DataPagamento = entidade.DataPagamento;
            // apagar.DataReferencia = entidade.DataReferencia;
            // apagar.DataVencimento = entidade.DataVencimento;
            // apagar.IdNaturezaDeLancamento = entidade.IdNaturezaDeLancamento;

            // apagar = await _apagarRepository.Atualizar(apagar);

            // return _mapper.Map<ApagarResponseContract>(apagar);

            var contrato = _mapper.Map<Apagar>(entidade);
            contrato.Id = apagar.Id;
            contrato.IdUsuario = apagar.IdUsuario;
            contrato.DataCadastro = apagar.DataCadastro;

            contrato = await _apagarRepository.Atualizar(contrato);

            return _mapper.Map<ApagarResponseContract>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoIdUsuario(id, idUsuario);

            await _apagarRepository.Deletar(apagar);
        }

        public async Task<IEnumerable<ApagarResponseContract>> Obter(long idUsuario)
        {
            var titulosApagar = await _apagarRepository.ObterPeloIdUsuario(idUsuario);

            return titulosApagar.Select(titulo => _mapper.Map<ApagarResponseContract>(titulo));
        }

        public async Task<ApagarResponseContract> Obter(long id, long idUsuario)
        {
            Apagar apagar = await ObterPorIdVinculadoIdUsuario(id, idUsuario);
            return _mapper.Map<ApagarResponseContract>(apagar);
        }

        private async Task<Apagar> ObterPorIdVinculadoIdUsuario(long id, long idUsuario)
        {

            var apagar = await _apagarRepository.Obter(id);
            if (apagar is null || apagar.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi enconrada nenhum título apagar pelo id {id}");
            }

            return apagar;
        }
    }
}