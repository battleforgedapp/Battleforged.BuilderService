using Battleforged.BuilderService.Domain.Entities;
using Battleforged.BuilderService.Domain.Repositories;
using MediatR;

namespace Battleforged.BuilderService.Application.Rosters.Queries.GetRostersByUser; 

public sealed class GetRostersByUserQueryHandler(IRosterRepository repo)
    : IRequestHandler<GetRostersByUserQuery, IQueryable<Roster>> {

    public async Task<IQueryable<Roster>> Handle(GetRostersByUserQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo
            .AsQueryable()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.RosterName)
            .ThenByDescending(x => x.CreatedDate)
            .AsQueryable(), 
            cancellationToken
        );
}