using Battleforged.BuilderService.Domain.Abstractions;
using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Models;
using Battleforged.BuilderService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.BuilderService.Infrastructure.Database.Repositories; 

public sealed class RosterUnitRepository(IDbContextFactory<AppDbContext> ctx) : IRosterUnitRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<RosterUnit> AddAsync(RosterUnit entity, CancellationToken ct = default) {
        await _ctx.RosterUnits.AddAsync(entity, ct);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }

    public IQueryable<RosterUnit> AsQueryable() => _ctx.RosterUnits.AsQueryable();
    
    public void Delete(RosterUnit entity) {
        _ctx.RosterUnits.Remove(entity);
        _ctx.SaveChanges();
    }
    
    public async Task<int> DeleteAllByRosterIdAsync(Guid rosterId, CancellationToken ct = default) {
        return await _ctx.RosterUnits
            .IgnoreQueryFilters()
            .Where(x => x.RosterId == rosterId)
            .ExecuteDeleteAsync(ct);
    }
    
    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }
    
    public void Update(RosterUnit entity) {
        _ctx.RosterUnits.Update(entity);
        _ctx.SaveChanges();
    }
}