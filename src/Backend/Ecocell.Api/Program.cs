using Ecocell.Domain.Extension;
using Ecocell.Infrastructure;
using Ecocell.Infrastructure.Migrations;
using Ecocell.Infrastructure.Migrations.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

UpdateDatabase();

app.Run();

void UpdateDatabase()
{
    var connectionString = builder.Configuration.GetConnectionString();
    var databaseName = builder.Configuration.GetDatabaseName();
    
    Database.CreateDatabase(connectionString, databaseName);
    app.MigrateDatabase();
}