using Ecocell.Api;
using Ecocell.Api.Filters;
using Ecocell.Application;
using Ecocell.Application.Services.AutoMapper;
using Ecocell.Domain.Extension;
using Ecocell.Infrastructure;
using Ecocell.Infrastructure.Migrations;
using Ecocell.Infrastructure.Migrations.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls=true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddApi(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());

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

app.UsePathBase(new PathString("/api/v1"));

UpdateDatabase();

app.Run();

void UpdateDatabase()
{
    var connectionString = builder.Configuration.GetConnectionString();
    var databaseName = builder.Configuration.GetDatabaseName();

    Database.CreateDatabase(connectionString, databaseName);
    app.MigrateDatabase();
}
