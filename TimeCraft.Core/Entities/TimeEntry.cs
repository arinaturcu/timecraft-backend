namespace TimeCraft.Core.Entities;

public class TimeEntry : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    /// <summary>
    /// This property is used as a foreign to the user table in the database and as a correlation key for the ORM.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// This is a navigation property for the ORM to correlate this entity with the entity that it references via the foreign key.
    /// </summary>
    public User User { get; set; } = default!;
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public Guid SubtaskId { get; set; }
    
    public Subtask Subtask { get; set; } = default!;
}
