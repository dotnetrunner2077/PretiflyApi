using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PretiflyAPI.Infrastructure.Data;

public class PretiflyDbContextFactory : IDesignTimeDbContextFactory<PretiflyDbContext>
{
    public PretiflyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<PretiflyDbContext>();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

        optionsBuilder.UseMySql(connectionString, serverVersion);

        return new PretiflyDbContext(optionsBuilder.Options);
    }
}
