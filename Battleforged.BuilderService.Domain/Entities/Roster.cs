namespace Battleforged.BuilderService.Domain.Entities; 

public sealed class Roster {

    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; } = string.Empty;

    public string RosterName { get; set; } = string.Empty;
    
    public Guid ArmyId { get; set; }
    
    public Guid BattleSizeId { get; set; }
    
    public Guid DetachmentId { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}