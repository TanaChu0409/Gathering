namespace eGathering.Api.Configuration;

public class HealthChecksServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration.GetConnectionString("DefaultConnection"));
        var hcBuilder = services.AddHealthChecks();
        hcBuilder.AddSqlServer(
            _ => configuration.GetConnectionString("DefaultConnection")!,
            name: "GatheringDB-check",
            tags: new string[] { "ready" });
    }
}