namespace TimeCraft.Core.DataTransferObjects;

public class ProjectAddDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }
}