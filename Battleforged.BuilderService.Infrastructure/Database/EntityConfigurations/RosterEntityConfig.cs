using Battleforged.BuilderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Infrastructure.Database.EntityConfigurations; 

public static class RosterEntityConfig {

    public static void RegisterRosterEntity(this ModelBuilder builder) {
        builder.Entity<Roster>(cfg => {
            // configure the table properties
            cfg.ToTable("rosters");
            cfg.HasKey(pk => pk.Id);
            cfg.HasIndex(i => i.UserId);
            
            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("roster_id")
                .IsRequired();
            
            cfg.Property(p => p.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(128)
                .IsRequired();

            cfg.Property(p => p.RosterName)
                .HasColumnName("army_name")
                .HasMaxLength(256)
                .IsRequired();
            
            cfg.Property(p => p.ArmyId)
                .HasColumnName("army_id")
                .IsRequired();
            
            cfg.Property(p => p.BattleSizeId)
                .HasColumnName("battle_size_id")
                .HasColumnType("char(36)")
                .IsRequired();
            
            cfg.Property(p => p.DetachmentId)
                .HasColumnName("detachment_id")
                .HasColumnType("char(36)")
                .IsRequired();

            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime(6)")
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date")
                .HasColumnType("datetime(6)")
                .IsRequired(false);
            
            // configure the relationship with the unit collection 
            cfg.HasMany<RosterUnit>()
                .WithOne()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.RosterId);
        });
    }
}