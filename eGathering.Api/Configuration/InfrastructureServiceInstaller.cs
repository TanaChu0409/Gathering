using eGathering.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Scrutor;

namespace eGathering.Api.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(
                selector => selector
                            .FromAssemblies(
                                Persistence.AssemblyReference.Assembly,
                                eGathering.Infrastructure.AssemblyReference.Assembly)
                            .AddClasses(false)
                            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                            .AsMatchingInterface()
                            .WithTransientLifetime());

        static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
        {
            sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }

        services.AddDbContext<GatheringContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), ConfigureSqlOptions);
        });
    }
}