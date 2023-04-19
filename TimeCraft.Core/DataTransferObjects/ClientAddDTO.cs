using TimeCraft.Core.Enums;

namespace TimeCraft.Core.DataTransferObjects;

public class ClientAddDTO
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}