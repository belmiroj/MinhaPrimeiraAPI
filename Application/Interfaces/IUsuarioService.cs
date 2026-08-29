using MinhaPrimeiraAPI.Application.DTOs;

namespace MinhaPrimeiraAPI.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync();
    Task<UsuarioResponseDto?> ObterPorIdAsync(int id);
    Task<UsuarioResponseDto> CriarAsync(CriarUsuarioDto dto);
}