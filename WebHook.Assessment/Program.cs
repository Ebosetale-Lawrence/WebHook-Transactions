using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Serilog;
using Serilog.Exceptions;
using System.Data;
using WebHook.Assessment.Application.Implimentation;
using WebHook.Assessment.Application.Interface;
using WebHook.Assessment.Application.Models;
using WebHook.Assessment.Persistence.DataContexts;

var builder = WebApplication.CreateBuilder(args);


// DbContext with PostgreSQL

builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<TransactionSettings>(
    builder.Configuration.GetSection("TransactionSettings"));
//Register service
builder.Services.AddScoped<ITransactionService, TransactionService>();
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



void ConfigureLogs()
{


    //Logger

    Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .Enrich.WithExceptionDetails()//add exception details
          .WriteTo.Debug()
          .WriteTo.Console()
          .WriteTo.File($"{builder.Environment.ContentRootPath}{Path.DirectorySeparatorChar}ServiceLogs/webHook-", rollingInterval: RollingInterval.Day)
          //.WriteTo.Elasticsearch(ConfigureElasticSearch(configuration, env))
          .CreateLogger();

}