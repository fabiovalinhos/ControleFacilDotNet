using ControleFacil.Api.Domain.Models;

namespace ControleFacil.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario?> Obter(string email);
    }
}