using System.Security.Claims;
using Battleforged.BuilderService.Application.Rosters.Queries.GetRosterById;
using Battleforged.BuilderService.Application.Rosters.Queries.GetRostersByUser;
using Battleforged.BuilderService.Domain.Entities;
using HotChocolate.Authorization;
using MediatR;

namespace Battleforged.BuilderService.Graph.Queries; 

[ExtendObjectType("Query")]
public class RosterQueries {

    [Authorize]
    public async Task<Roster> GetRosterByIdAsync(
        [Service] IMediator mediatr,
        Guid id,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken ct
    ) => await mediatr.Send(new GetRosterByIdQuery(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!, id), ct);
    
    [Authorize]
    [UsePaging(IncludeTotalCount = true)]
    [UseSorting]
    public async Task<IQueryable<Roster>> GetRostersAsync(
        [Service] IMediator mediatr,
        ClaimsPrincipal claimsPrincipal,
        CancellationToken ct
    ) => await mediatr.Send(new GetRostersByUserQuery(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!), ct);
}