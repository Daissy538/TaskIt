using Microsoft.EntityFrameworkCore;
using TaskIt.Adapter.SQL.Context;
using TaskIt.Adapter.SQL.Steps;
using TaskIt.Adapter.SQL.Tasks;
using TaskIt.Application;
using TaskIt.Application.Ports.RepositoryInterfaces;
using TaskIt.Core;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IStepRepository, StepsRepository>();

builder.Services.AddDbContext<TaskItSQLDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQL")));

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

public partial class Program { };
