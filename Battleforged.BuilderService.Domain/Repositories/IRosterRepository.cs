using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Models;

namespace Battleforged.BuilderService.Domain.Repositories; 

public interface IRosterRepository : IAsyncDisposable {
    Task<Roster> AddAsync(Roster entity, CancellationToken ct = default);
    IQueryable<Roster> AsQueryable();
    void Delete(Roster entity);
    void Update(Roster entity);
}