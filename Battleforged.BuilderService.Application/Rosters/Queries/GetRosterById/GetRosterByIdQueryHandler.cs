using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Exceptions;
using Battleforged.BuilderService.Domain.Repositories;
using MediatR;

namespace Battleforged.BuilderService.Application.Rosters.Queries.GetRosterById; 

public sealed class GetRosterByIdQueryHandler(IRosterRepository repo)
    : IRequestHandler<GetRosterByIdQuery, Roster> {

    public async Task<Roster> Handle(GetRosterByIdQuery request, CancellationToken cancellationToken) {
        var roster = await repo.GetRosterByIdAsync(request.RosterId, cancellationToken);
        
        // check the roster exists and that the user is the owner (who would be logged in) is the owner
        if (roster is null) {
            throw new EntityNotFoundException<Roster>(request.RosterId);
        }
        if (roster.UserId != request.UserId) {
            throw new UnauthorisedAccessToResourceException<Roster>(request.RosterId);
        }

        return roster;
    }
}