using Microsoft.EntityFrameworkCore;
using IntelTaskUCR.Domain.Interfaces;
using IntelTaskUCR.Infrastructure.Context;
using IntelTaskUCR.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IntelTaskDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnection")));
builder.Services.AddScoped<IDemoRepository,DemoRepository>();

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
