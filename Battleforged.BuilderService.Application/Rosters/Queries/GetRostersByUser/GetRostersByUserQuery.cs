using Battleforged.BuilderService.Domain.Entities;
using MediatR;

namespace Battleforged.BuilderService.Application.Rosters.Queries.GetRostersByUser; 

public record GetRostersByUserQuery(string UserId) : IRequest<IQueryable<Roster>>;