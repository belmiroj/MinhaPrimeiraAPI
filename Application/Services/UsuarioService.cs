using MinhaPrimeiraAPI.Application.DTOs;
using MinhaPrimeiraAPI.Application.Interfaces;
using MinhaPrimeiraAPI.Domain.Entities;

namespace MinhaPrimeiraAPI.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();
        return usuarios.Select(u => new UsuarioResponseDto(u.Id, u.Nome, u.Email));
    }

    public async Task<UsuarioResponseDto?> ObterPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario is null) return null;

        return new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email);
    }

    public async Task<UsuarioResponseDto> CriarAsync(CriarUsuarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new ArgumentException("Nome e E-mail são obrigatórios.");
        }

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email
        };

        await _usuarioRepository.AdicionarAsync(usuario);

        return new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email);
    }

    public async Task<UsuarioResponseDto?> AtualizarAsync(int id, CriarUsuarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new ArgumentException("Nome e E-mail são obrigatórios.");
        }

        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario is null) return null;

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;

        await _usuarioRepository.AtualizarAsync(usuario);

        return new UsuarioResponseDto(usuario.Id, usuario.Nome, usuario.Email);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuario is null) return false;

        await _usuarioRepository.ExcluirAsync(usuario);
        return true;
    }
}