namespace MinhaPrimeiraAPI.Application.DTOs;

public record CriarUsuarioDto(string Nome, string Email);

public record UsuarioResponseDto(int Id, string Nome, string Email);