namespace TimeCraft.Core.DataTransferObjects;

public class SubtaskAddDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
}