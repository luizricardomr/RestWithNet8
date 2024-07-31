using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithNet8.Api.Business;
using RestWithNet8.Api.Business.Implementations;
using RestWithNet8.Api.Configurations;
using RestWithNet8.Api.Filters;
using RestWithNet8.Api.Hipermedia.Enricher;
using RestWithNet8.Api.Hipermedia.Filters;
using RestWithNet8.Api.Model.Context;
using RestWithNet8.Api.Repository;
using RestWithNet8.Api.Repository.Generic;
using RestWithNet8.Api.Repository.Implementations;
using RestWithNet8.Api.Services;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var tokenConfigurations = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>
    (builder.Configuration.GetSection("TokenConfigurations"))
    .Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser()
                            .Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 4, 0))));

//if (builder.Environment.IsDevelopment())
//{
//    MigrateDatabase(connection);
//}

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

builder.Services.AddApiVersioning();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{    
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "REST API's from 0 to Azure with NET 8 and Docker",
        Version = "v1",
        Description = "API Restful Desenvolvida no curso 'REST API's from 0 to Azure with NET 8 and Docker'",
        Contact = new OpenApiContact
        {
            Name = "Luiz Ricardo Marinho do Rêgo",
            Url = new Uri("https://github.com/luizricardomr")
        }
    });
    c.OperationFilter<FileUploadOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header - Utilizando com Bearer Authentication.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            }
                        },
                       Array.Empty<string>()
                    }
                });

});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IFileBusiness, FileBusiness>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API's from 0 to Azure with NET 8 and Docker");
});

var options = new RewriteOptions().AddRedirect("^$", "swagger");
app.UseRewriter(options);

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

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
