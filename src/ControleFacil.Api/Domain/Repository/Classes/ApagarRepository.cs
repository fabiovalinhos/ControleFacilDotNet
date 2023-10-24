using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{


    public class ApagarRepository : IApagarRepository
    {
        private readonly ApplicationContext _contexto;

        public ApagarRepository(ApplicationContext contexto)
        {
            this._contexto = contexto;
        }


        public async Task<Apagar> Adicionar(Apagar entidade)
        {
            await _contexto.Apagar.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Apagar> Atualizar(Apagar entidade)
        {
            Apagar entidadeBanco =
             await _contexto.Apagar
             .AsNoTracking()
             .Where(n => n.Id == entidade.Id).FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Apagar>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(Apagar entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<Apagar>> Obter()
        {
            return await _contexto.Apagar
            .AsNoTracking()
            .OrderBy(n => n.Id)
            .ToListAsync();
        }

        public async Task<Apagar?> Obter(long id)
        {
            return await _contexto.Apagar
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Apagar>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _contexto.Apagar
            .AsNoTracking()
            .Where(n => n.IdUsuario == idUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}