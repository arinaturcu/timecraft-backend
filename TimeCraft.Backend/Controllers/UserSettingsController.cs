using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Requests;
using TimeCraft.Core.Responses;
using TimeCraft.Infrastructure.Authorization;
using TimeCraft.Infrastructure.Extensions;
using TimeCraft.Infrastructure.Services.Interfaces;

namespace TimeCraft.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserSettingsController : AuthorizedController
{
    private readonly IUserSettingsService _userSettingsService;
    
    public UserSettingsController(IUserService userService, IUserSettingsService userSettingsService) : base(userService)
    {
        _userSettingsService = userSettingsService;
    }
    
    [Authorize]
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<RequestResponse<UserSettingsDTO>>> GetById([FromRoute] Guid userId)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ? 
            this.FromServiceResponse(await _userSettingsService.GetUserSettings(userId)) : 
            this.ErrorMessageResult<UserSettingsDTO>(currentUser.Error);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<UserSettingsDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _userSettingsService.GetUsersSettings(pagination)) :
            this.ErrorMessageResult<PagedResponse<UserSettingsDTO>>(currentUser.Error);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] UserSettingsDTO userSettings)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _userSettingsService.AddUserSettings(userSettings, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);

    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] UserSettingsDTO userSettings)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _userSettingsService.UpdateUserSettings(userSettings, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid userId)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _userSettingsService.DeleteUserSettings(userId, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}