using MinhaPrimeiraAPI.Domain.Entities;

namespace MinhaPrimeiraAPI.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<Usuario?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task ExcluirAsync(Usuario usuario);
}