namespace TimeCraft.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about user settings within the application and to client application.
/// </summary>
public class UserSettingsDTO
{
    public Guid UserId { get; set; }
    public string DateFormat { get; set; } = default!;
    public string TimeFormat { get; set; } = default!;
    public string TimeZone { get; set; } = default!;
    public string Theme { get; set; } = default!;
}