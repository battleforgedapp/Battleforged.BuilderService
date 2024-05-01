using Battleforged.BuilderService.Domain.Entities;

namespace Battleforged.BuilderService.Domain.Repositories; 

public interface IRosterUnitRepository : IAsyncDisposable {
    Task<RosterUnit> AddAsync(RosterUnit entity, CancellationToken ct = default);
    IQueryable<RosterUnit> AsQueryable();
    void Delete(RosterUnit entity);
    Task<int> DeleteAllByRosterIdAsync(Guid rosterId, CancellationToken ct = default);
    void Update(RosterUnit entity);
}