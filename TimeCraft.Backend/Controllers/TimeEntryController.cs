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
public class TimeEntryController : AuthorizedController
{
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(IUserService userService, ITimeEntryService timeEntryService) : base(userService)
    {
        _timeEntryService = timeEntryService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TimeEntryDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse( await _timeEntryService.GetTimeEntry(id)) :
            this.ErrorMessageResult<TimeEntryDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<TimeEntryDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _timeEntryService.GetTimeEntries(pagination)) :
            this.ErrorMessageResult<PagedResponse<TimeEntryDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse<TimeEntryDTO>>> Add([FromBody] TimeEntryAddDTO timeEntry)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _timeEntryService.AddTimeEntry(timeEntry, currentUser.Result)) :
            this.ErrorMessageResult<TimeEntryDTO>(currentUser.Error);    
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TimeEntryDTO timeEntry)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _timeEntryService.UpdateTimeEntry(timeEntry, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _timeEntryService.DeleteTimeEntry(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}