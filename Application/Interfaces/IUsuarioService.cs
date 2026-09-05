namespace MinhaPrimeiraAPI.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync();
    Task<UsuarioResponseDto?> ObterPorIdAsync(int id);
    Task<UsuarioResponseDto> CriarAsync(CriarUsuarioDto dto);
    Task<UsuarioResponseDto?> AtualizarAsync(int id, CriarUsuarioDto dto); // ou AtualizarUsuarioDto
    Task<bool> ExcluirAsync(int id);
}