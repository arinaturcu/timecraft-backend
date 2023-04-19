namespace TimeCraft.Core.Entities;

public class Project : BaseEntity
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
    /// This property is used as a foreign to the user table in the database and as a correlation key for the ORM.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// This is a navigation property for the ORM to correlate this entity with the entity that it references via the foreign key.
    /// </summary>
    public Client Client { get; set; } = default!;
    
    public ICollection<Subtask> Subtasks { get; set; } = default!;
}