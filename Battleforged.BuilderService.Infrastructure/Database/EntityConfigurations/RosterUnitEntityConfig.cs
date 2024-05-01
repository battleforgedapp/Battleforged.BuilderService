using Battleforged.BuilderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Infrastructure.Database.EntityConfigurations; 

public static class RosterUnitEntityConfig {

    public static void RegisterRosterUnitEntity(this ModelBuilder builder) {
        builder.Entity<RosterUnit>(cfg => {
            // configure the table properties
            cfg.ToTable("roster_units");
            cfg.HasKey(pk => pk.Id);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("roster_unit_id")
                .IsRequired();
            
            cfg.Property(p => p.RosterId)
                .HasColumnName("roster_id")
                .IsRequired();
            
            cfg.Property(p => p.UnitGroupingId)
                .HasColumnName("unit_grouping_id")
                .IsRequired();

            cfg.Property(p => p.IsWarlord)
                .HasColumnName("is_warlord")
                .HasDefaultValue(false)
                .IsRequired();
            
            cfg.Property(p => p.AddedOnDate)
                .HasColumnName("added_on_date")
                .IsRequired();
        });
    }
}