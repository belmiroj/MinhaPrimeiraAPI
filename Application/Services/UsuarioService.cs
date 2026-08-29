using MinhaPrimeiraAPI.Application.DTOs;
using MinhaPrimeiraAPI.Application.Interfaces;
using MinhaPrimeiraAPI.Domain.Entities;

namespace MinhaPrimeiraAPI.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync()
    {
        var usuarios = await _repository.ObterTodosAsync();
        
        return usuarios.Select(u => new UsuarioResponseDto(u.Id, u.Nome, u.Email));
    }

    public async Task<UsuarioResponseDto?> ObterPorIdAsync(int id)
    {
        var usuario = await _repository.ObterPorIdAsync(id);
        if (usuario is null) return null;

        return new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email);
    }

    public async Task<UsuarioResponseDto> CriarAsync(CriarUsuarioDto dto)
    {
        var usuario = new Usuario(dto.Nome, dto.Email);

        await _repository.AdicionarAsync(usuario);

        return new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email);
    }
}