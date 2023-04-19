using TimeCraft.Infrastructure.Services.Interfaces;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;
using TimeCraft.Core.Specifications;
using TimeCraft.Infrastructure.Database;
using TimeCraft.Infrastructure.Repositories.Interfaces;
using TimeCraft.Core.Errors;
using System.Net;
using TimeCraft.Core.Entities;

namespace TimeCraft.Infrastructure.Services.Implementations;

public class UserSettingsService : IUserSettingsService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public UserSettingsService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<UserSettingsDTO>> GetUserSettings(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserSettingsSpec(userId), cancellationToken);
        
        return result != null ? 
            ServiceResponse<UserSettingsDTO>.ForSuccess(new UserSettingsDTO
            {
                UserId = result.UserId,
                Theme = result.Theme,
                DateFormat = result.DateFormat,
                TimeFormat = result.TimeFormat,
                TimeZone = result.TimeZone
            }) : 
            ServiceResponse<UserSettingsDTO>.FromError(CommonErrors.UserSettingsNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<UserSettingsDTO>>> GetUsersSettings(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new UserSettingsProjectionSpec(), cancellationToken);
        
        return ServiceResponse<PagedResponse<UserSettingsDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddUserSettings(UserSettingsDTO userSettings, UserDTO? requestingUser,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || requestingUser.Id != userSettings.UserId)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "A user cannot add settings to another user.", ErrorCodes.CannotAdd));
        }
        
        var result = await _repository.GetAsync(new UserSettingsSpec(userSettings.UserId), cancellationToken);
        
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "User settings could not be changed.", ErrorCodes.CannotUpdate));
        }
        
        await _repository.AddAsync(new UserSettings
        {
            UserId = userSettings.UserId,
            Theme = userSettings.Theme,
            DateFormat = userSettings.DateFormat,
            TimeFormat = userSettings.TimeFormat,
            TimeZone = userSettings.TimeZone
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateUserSettings(UserSettingsDTO userSettings, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || requestingUser.Id != userSettings.UserId)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "A user cannot update settings of another user.", ErrorCodes.CannotUpdate));
        }
        
        var entity = await _repository.GetAsync(new UserSettingsSpec(userSettings.UserId), cancellationToken);
        
        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "User settings could not be found.", ErrorCodes.CannotUpdate));
        }

        entity.Theme = userSettings.Theme;
        entity.DateFormat = userSettings.DateFormat;
        entity.TimeFormat = userSettings.TimeFormat;
        entity.TimeZone = userSettings.TimeZone;
        
        await _repository.UpdateAsync(entity, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteUserSettings(Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || requestingUser.Id != id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "A user cannot delete settings of another user.", ErrorCodes.CannotDelete));
        }
        
        await _repository.DeleteAsync(new UserSettingsSpec(id), cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}
