using System.Net;

namespace TimeCraft.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage UserSettingsNotFound => new(HttpStatusCode.NotFound, "User settings not found!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ClientNotFound => new(HttpStatusCode.NotFound, "Client doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage ProjectNotFound => new(HttpStatusCode.NotFound, "Project doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
