using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraAPI.Application.DTOs;
using MinhaPrimeiraAPI.Application.Interfaces;

namespace MinhaPrimeiraAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario is null) return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioDto dto)
    {
        try
        {
            var response = await _usuarioService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CriarUsuarioDto dto)
    {
        try
        {
            var usuarioAtualizado = await _usuarioService.AtualizarAsync(id, dto);
            if (usuarioAtualizado is null) 
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return Ok(usuarioAtualizado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(int id)
    {
        var sucesso = await _usuarioService.ExcluirAsync(id);
        if (!sucesso) 
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return NoContent();
    }
}