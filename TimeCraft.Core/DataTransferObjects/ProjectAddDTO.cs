namespace TimeCraft.Core.DataTransferObjects;

public class ProjectAddDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ClientId { get; set; } = default!;
}