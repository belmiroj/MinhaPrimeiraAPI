namespace MinhaPrimeiraAPI.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    protected Usuario() { }

    public Usuario(string nome, string email)
    {
        Validar(nome, email);
        Nome = nome;
        Email = email;
    }

    public void Atualizar(string nome, string email)
    {
        Validar(nome, email);
        Nome = nome;
        Email = email;
    }

    private static void Validar(string nome, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("E-mail inválido.");
    }
}