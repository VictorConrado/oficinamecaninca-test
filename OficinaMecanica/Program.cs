using OficinaMecanica.Repositories;
using OficinaMecanica.Repositories.Interfaces;
using OficinaMecanica.Services;
using OficinaMecanica.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência — ordem importa conceitualmente
builder.Services.AddSingleton<IOrcamentoRepository, OrcamentoRepository>();
builder.Services.AddScoped<IOrcamentoService, OrcamentoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();