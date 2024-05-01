using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Repositories;
using MediatR;

namespace Battleforged.BuilderService.Application.RosterUnits.Queries.GetUnitsByRoster; 

public sealed class GetUnitsByRosterQueryHandler(IRosterUnitRepository repo)
    : IRequestHandler<GetUnitsByRosterQuery, IQueryable<RosterUnit>> {

    public async Task<IQueryable<RosterUnit>> Handle(GetUnitsByRosterQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo.AsQueryable().OrderBy(x => x.Id), cancellationToken);
}