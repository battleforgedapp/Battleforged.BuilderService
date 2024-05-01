using Battleforged.BuilderService.Application.RosterUnits.Queries.GetUnitsByRoster;
using Battleforged.BuilderService.Domain.Entities;
using MediatR;

namespace Battleforged.BuilderService.Graph.Nodes; 

[Node]
[ExtendObjectType(typeof(Roster))]
public class RosterNodes {
    
    [UseSorting]
    public async Task<IQueryable<RosterUnit>> GetUnitsAsync(
        [Parent] Roster roster,
        [Service] IMediator mediatr,
        CancellationToken ct
    ) => await mediatr.Send(new GetUnitsByRosterQuery(roster.Id), ct);
}