using TimeCraft.Core.Enums;

namespace TimeCraft.Core.DataTransferObjects;

public class ClientDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}