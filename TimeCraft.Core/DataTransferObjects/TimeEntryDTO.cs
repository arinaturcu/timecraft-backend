namespace TimeCraft.Core.DataTransferObjects;

public class TimeEntryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid SubtaskId { get; set; }
}