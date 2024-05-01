using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Models;
using Battleforged.BuilderService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Infrastructure.Database.Repositories; 

public sealed class RosterRepository(IDbContextFactory<AppDbContext> ctx) : IRosterRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<Roster> AddAsync(Roster entity, CancellationToken ct = default) {
        await _ctx.Rosters.AddAsync(entity, ct);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }

    public IQueryable<Roster> AsQueryable() => _ctx.Rosters.AsQueryable();

    public void Delete(Roster entity) {
        entity.DeletedDate = DateTime.UtcNow;
        _ctx.Rosters.Update(entity);
        _ctx.SaveChanges();
    }
    
    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }

    public async Task<Roster?> GetRosterByIdAsync(Guid rosterId, CancellationToken ct = default)
        => await _ctx.Rosters.FirstOrDefaultAsync(x => x.Id == rosterId, ct);

    public void Update(Roster entity) {
        _ctx.Rosters.Update(entity);
        _ctx.SaveChanges();
    }
}