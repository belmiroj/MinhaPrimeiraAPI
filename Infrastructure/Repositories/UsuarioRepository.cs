using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Application.Interfaces;
using MinhaPrimeiraAPI.Domain.Entities;
using MinhaPrimeiraAPI.Infrastructure.Data;

namespace MinhaPrimeiraAPI.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario?> ObterPorIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}