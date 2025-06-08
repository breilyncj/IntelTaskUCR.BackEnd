using Microsoft.EntityFrameworkCore;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Context;
using IntelTaskUCR.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IntelTaskDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnection")));
builder.Services.AddScoped<IDemoRepository,DemoRepository>();
builder.Services.AddScoped<IUsuariosRepository,UsuariosRepository>();
builder.Services.AddScoped<ITareasRepository,TareasRepository>();
builder.Services.AddScoped<IRolesRepository,RolesRepository>();
builder.Services.AddScoped<IOficinasRepository,OficinasRepository>();
builder.Services.AddScoped<IComplejidadesRepository, ComplejidadesRepository>();
builder.Services.AddScoped<IFrecuenciaRecordatorioRepository, FrecuenciaRecordatorioRepository>();
builder.Services.AddScoped<IEstadosRepository, EstadoRepository>();
builder.Services.AddScoped<IPrioridadesRepository, PrioridadesRepository>();
builder.Services.AddScoped<INotificacionesRepository, NotificacionesRepository>();
builder.Services.AddScoped<ITareasIncumplimientosRepository, TareasIncumplimientosRepository>();
builder.Services.AddScoped<ITareasJustificacionRechazoRepository, TareasJustificacionRechazoRepository>();
builder.Services.AddScoped<ITareasSeguimientoRepository, TareasSeguimientoRepository>();
builder.Services.AddScoped<INotificacionesXUsuariosRepository, NotificacionesXUsuariosRepository>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
