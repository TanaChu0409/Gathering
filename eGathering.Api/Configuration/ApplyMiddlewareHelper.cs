using eGathering.Api.Middlewares;

namespace eGathering.Api.Configuration;

internal static class ApplyMiddlewareHelper
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsFactoryMiddleware>();
        return app;
    }
}