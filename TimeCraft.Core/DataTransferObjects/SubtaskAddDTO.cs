namespace TimeCraft.Core.DataTransferObjects;

public class SubtaskAddDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; } = default!;
}