using Battleforged.BuilderService.Domain.Entities;
using MediatR;

namespace Battleforged.BuilderService.Application.RosterUnits.Queries.GetUnitsByRoster; 

public record GetUnitsByRosterQuery(Guid RosterId) : IRequest<IQueryable<RosterUnit>>;