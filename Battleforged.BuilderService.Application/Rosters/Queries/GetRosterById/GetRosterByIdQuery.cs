using Battleforged.BuilderService.Domain.Entities;
using MediatR;

namespace Battleforged.BuilderService.Application.Rosters.Queries.GetRosterById; 

public record GetRosterByIdQuery(string UserId, Guid RosterId) : IRequest<Roster>;