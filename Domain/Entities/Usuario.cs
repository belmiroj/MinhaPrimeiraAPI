namespace MinhaPrimeiraAPI.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Usuario() { }
    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}