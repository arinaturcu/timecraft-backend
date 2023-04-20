namespace TimeCraft.Core.DataTransferObjects;

public class SubtaskDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
}