using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RestWithNet8.Api.Business;
using RestWithNet8.Api.Business.Implementations;
using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Repository;
using RestWithNet8.Api.Repository.Implementations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8,4,0))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}


builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

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

void MigrateDatabase(string connection)
{
	try
	{
		var envolveConnection = new MySqlConnection(connection);
		var envolve = new Evolve(envolveConnection, Log.Information)
		{
			Locations = new List<string> { "db/migrations", "db/dataset" },
			IsEraseDisabled = false
		};
		envolve.Migrate();
	}
	catch (Exception ex)
	{
		Log.Error("Database migration failed", ex);
		throw;
	}
}
