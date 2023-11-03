using eGathering.Api.Middlewares;

namespace eGathering.Api.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();

        services
            .AddTransient<ExceptionsFactoryMiddleware>();
    }
}