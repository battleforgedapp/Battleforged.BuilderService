namespace Battleforged.BuilderService.Domain.Entities; 

public class RosterUnit {

    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid RosterId { get; set; }
    
    public Guid UnitGroupingId { get; set; }
    
    public bool IsWarlord { get; set; }
    
    public DateTime AddedOnDate { get; set; } = DateTime.UtcNow;
}