using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Context;

namespace TestTechniqueDotnet.Extensions.Configurations;

public static class DbConfigurationExtension
{
    
    /// <summary>
    /// Configure la connexion à la base de données Pet Store
    /// </summary>
    /// <param name="builder"></param>
    public static void SetPetStoreDbContext(this WebApplicationBuilder builder)
    {
        var host = builder.Configuration["MYSQL_HOST"] ?? builder.Configuration.GetConnectionString("MYSQL_HOST");
        var port = builder.Configuration["MYSQL_PORT"] ?? builder.Configuration.GetConnectionString("MYSQL_PORT");
        var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
        var userid = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
        var usersDataBase = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");
        
        var connectionString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";
        
        // var connectionString = builder.Configuration.GetConnectionString("JWTAUTHENTICATION");
        Console.WriteLine("connectionString");
        Console.WriteLine(connectionString);
        builder.Services.AddDbContext<PetStoreContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
    }
}