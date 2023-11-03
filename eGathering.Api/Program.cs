#pragma warning disable CA1506 // 避免過度的類別結合

using eGathering.Api.Configuration;
using Microsoft.EntityFrameworkCore;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services
            .InstallServices(
                builder.Configuration,
                typeof(IServiceInstaller).Assembly);

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseExceptionMiddleware();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<eGathering.Persistence.GatheringContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }

    await app.RunAsync().ConfigureAwait(false);
}
catch (Exception ex)
{
    Log.Error(ex, "API host terminated unexpectedly. {ex}", ex.ToString());
    throw;
}
finally
{
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}
#pragma warning restore CA1506 // 避免過度的類別結合