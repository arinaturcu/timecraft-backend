namespace TimeCraft.Core.DataTransferObjects;

public class TimeEntryAddDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; } = default!;
    public DateTime EndTime { get; set; } = default!;
    public Guid SubtaskId { get; set; } = default!;
}