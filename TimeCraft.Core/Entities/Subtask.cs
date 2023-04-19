namespace TimeCraft.Core.Entities;

public class Subtask : BaseEntity
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
    
    /// <summary>
    /// This property is used as a foreign to the project table in the database and as a correlation key for the ORM.
    /// </summary>
    public Guid ProjectId { get; set; }
    
    /// <summary>
    /// This is a navigation property for the ORM to correlate this entity with the entity that it references via the foreign key.
    /// </summary>
    public Project Project { get; set; } = default!;
    
    public ICollection<TimeEntry> TimeEntries { get; set; } = default!;
}
