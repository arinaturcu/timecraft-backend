using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;

namespace TimeCraft.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange user information.
/// As most routes and business logic will need to know what user is currently using the backend this service will be the most used.
/// </summary>
public interface IUserSettingsService
{
    /// <summary>
    /// GetUserSettings will provide the information about a user's settings given its Id.
    /// </summary>
    public Task<ServiceResponse<UserSettingsDTO>> GetUserSettings(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetUsers returns page with user information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<UserSettingsDTO>>> GetUsersSettings(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddUser adds an user and verifies if requesting user has permissions to add one.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddUserSettings(UserSettingsDTO userSettings, UserDTO? requestingUser, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateUser updates an user and verifies if requesting user has permissions to update it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateUserSettings(UserSettingsDTO userSettings, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteUser deletes an user and verifies if requesting user has permissions to delete it, if the user is his own then that should be allowed.
    /// If the requesting user is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteUserSettings(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
