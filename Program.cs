using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using project_cache.Data;
using project_cache.Services.Agenda;
using project_cache.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    })
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<AddAgendaValidator>();
    });


// Configuração do OpenAPI (Swagger)
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro do serviço de Agenda
builder.Services.AddScoped<IAgendaInterface, AgendaService>();

// Configuração do DbContext (banco de dados SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
