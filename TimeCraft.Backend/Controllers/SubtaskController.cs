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
public class SubtaskController : AuthorizedController
{
    private readonly ISubtaskService _subtaskService;

    public SubtaskController(IUserService userService, ISubtaskService subtaskService) : base(userService)
    {
        _subtaskService = subtaskService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<SubtaskDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse( await _subtaskService.GetSubtask(id)) :
            this.ErrorMessageResult<SubtaskDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<SubtaskDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subtaskService.GetSubtasks(pagination)) :
            this.ErrorMessageResult<PagedResponse<SubtaskDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse<SubtaskDTO>>> Add([FromBody] SubtaskAddDTO subtask)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subtaskService.AddSubtask(subtask, currentUser.Result)) :
            this.ErrorMessageResult<SubtaskDTO>(currentUser.Error);    
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] SubtaskDTO subtask)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subtaskService.UpdateSubtask(subtask, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subtaskService.DeleteSubtask(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
