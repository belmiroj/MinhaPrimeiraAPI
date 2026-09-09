using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Application.Interfaces;
using MinhaPrimeiraAPI.Application.Services;
using MinhaPrimeiraAPI.Infrastructure.Data;
using MinhaPrimeiraAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// ETAPA 1: INJEÇÃO DE SERVIÇOS (Inversion of Control - IoC)
// =========================================================================

// 1.0 Configuração do serviço de Logs HTTP da API
builder.Services.AddHttpLogging(logging =>
{
    // Define o que aparecerá no log automático do console
    logging.LoggingFields = HttpLoggingFields.RequestMethod 
                          | HttpLoggingFields.RequestPath 
                          | HttpLoggingFields.RequestBody      // Mostra o JSON enviado no POST/PUT
                          | HttpLoggingFields.ResponseStatusCode;
    
    // Limite de tamanho do JSON que o log vai ler (4KB é mais que suficiente para usuários)
    logging.RequestBodyLogLimit = 4096; 
});

// 1.1 Configurações de Framework e Infraestrutura de Terceiros
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1.2 Camada de Infraestrutura / Persistência (Banco de Dados)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=banco_teste.db"));

// 1.3 Camada de Infraestrutura (Repositórios / Acesso a Dados)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// 1.4 Camada de Aplicação (Regras de Negócio / Services / Casos de Uso)
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// 1.5 Configurações Transversais (CORS, Autenticação, Cross-Cutting)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


// =========================================================================
// O DIVISOR DE ÁGUAS
// =========================================================================
var app = builder.Build();


// Banco de dados em memória (SQLite) para testes
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// =========================================================================
// ETAPA 2: CONFIGURAÇÃO DO PIPELINE HTTP (Middlewares)
// A ordem AQUI IMPORTA MUITO para a execução da requisição!
// =========================================================================

// 2.0 ATIVE O MIDDLEWARE DE LOGS (Deve ficar no topo para capturar tudo)
app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS sempre ANTES de Autenticação e Mapeamentos
app.UseCors("PermitirTudo");

// Autenticação -> Autorização (Nessa ordem!)
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento das rotas
app.MapControllers();

app.Run();
