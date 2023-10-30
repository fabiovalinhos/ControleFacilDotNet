using ControleFacil.Api.Data;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Api.Domain.Repository.Classes
{


    public class AreceberRepository : IAreceberRepository
    {
        private readonly ApplicationContext _contexto;

        public AreceberRepository(ApplicationContext contexto)
        {
            this._contexto = contexto;
        }


        public async Task<Areceber> Adicionar(Areceber entidade)
        {
            await _contexto.Areceber.AddAsync(entidade);
            await _contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task<Areceber> Atualizar(Areceber entidade)
        {
            Areceber entidadeBanco =
             await _contexto.Areceber
             .AsNoTracking()
             .Where(n => n.Id == entidade.Id)
             .FirstOrDefaultAsync();

            _contexto.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
            _contexto.Update<Areceber>(entidadeBanco);

            await _contexto.SaveChangesAsync();

            return entidadeBanco;
        }

        public async Task Deletar(Areceber entidade)
        {
            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<Areceber>> Obter()
        {
            return await _contexto.Areceber
            .AsNoTracking()
            .OrderBy(n => n.Id)
            .ToListAsync();
        }

        public async Task<Areceber?> Obter(long id)
        {
            return await _contexto.Areceber
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Areceber>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _contexto.Areceber
            .AsNoTracking()
            .Where(n => n.IdUsuario == idUsuario)
            .OrderBy(n => n.Id)
            .ToListAsync();
        }
    }
}