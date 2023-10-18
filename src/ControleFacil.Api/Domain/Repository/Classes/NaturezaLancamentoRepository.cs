using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{


    public class NaturezaLancamentoRepository : INaturezaDeLancamentoRepository
    {
        private readonly ApplicationContext _contexto;

        public NaturezaLancamentoRepository(ApplicationContext contexto)
        {
            this._contexto = contexto;
        }


        public async Task<NaturezaDeLancamento> Adicionar(NaturezaDeLancamento entidade)
        {
            await _contexto.NaturezaDeLancamento.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<NaturezaDeLancamento> Atualizar(NaturezaDeLancamento entidade)
        {
            NaturezaDeLancamento entidadeBanco =
             await _contexto.NaturezaDeLancamento
             .AsNoTracking()
             .Where(n => n.Id == entidade.Id).FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<NaturezaDeLancamento>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(NaturezaDeLancamento entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> Obter()
        {
            return await _contexto.NaturezaDeLancamento
            .AsNoTracking()
            .OrderBy(n => n.Id)
            .ToListAsync();
        }

        public async Task<NaturezaDeLancamento?> Obter(long id)
        {
            return await _contexto.NaturezaDeLancamento
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _contexto.NaturezaDeLancamento
            .AsNoTracking()
            .Where(n => n.IdUsuario == idUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}