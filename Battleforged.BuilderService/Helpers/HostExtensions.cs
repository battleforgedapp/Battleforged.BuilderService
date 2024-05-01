using Battleforged.BuilderService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Helpers; 

public static class HostExtensions {

    public static IHost PreStartup(this IHost host) {
        // create a scope for the pre-startup (this gives us access to repos, etc)
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        // run any migrations and do any checks
        var factory = serviceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var ctx = factory.CreateDbContext();
        
        if (ctx!.Database!.ProviderName!.Contains("Sqlite")) {
            // this makes sure our test-db that is local is created with all latest changes
            ctx.Database.EnsureCreated();
        }
        else if (ctx.Database.GetPendingMigrations().Any()) {
            // this will make sure that when running in prod, that all the latest migrations are applied before
            // the application/microservice is started up fully.
            ctx.Database.Migrate();
        }
        
        return host;
    }
}