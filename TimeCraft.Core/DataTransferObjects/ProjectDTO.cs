namespace TimeCraft.Core.DataTransferObjects;

public class ProjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ClientId { get; set; }
}