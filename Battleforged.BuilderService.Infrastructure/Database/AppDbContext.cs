using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Infrastructure.Database.EntityConfigurations;
using Battleforged.BuilderService.Infrastructure.Database.Functions;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Infrastructure.Database; 

public sealed class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts) {
    
    public DbSet<EventOutbox> Outbox { get; set; } = null!;

    public DbSet<Roster> Rosters { get; set; } = null!;

    public DbSet<RosterUnit> RosterUnits { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {
        // register the domain model table structures
        builder.RegisterEventOutboxEntity();
        builder.RegisterRosterEntity();
        builder.RegisterRosterUnitEntity();
        
        // register the functions that will provide help when querying our data
        GuidFunctions.Register(builder);
        base.OnModelCreating(builder);
    }
}