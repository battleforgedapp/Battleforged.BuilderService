using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Models;

namespace Battleforged.BuilderService.Domain.Repositories; 

public interface IRosterRepository : IAsyncDisposable {
    Task<Roster> AddAsync(Roster entity, CancellationToken ct = default);
    IQueryable<Roster> AsQueryable();
    void Delete(Roster entity);
    Task<Roster?> GetRosterByIdAsync(Guid rosterId, CancellationToken ct = default);
    void Update(Roster entity);
}