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
public class ProjectController : AuthorizedController
{
    private readonly IProjetcService _projectService;

    public ProjectController(IUserService userService, IProjetcService projectService) : base(userService)
    {
        _projectService = projectService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProjectDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse( await _projectService.GetProject(id)) :
            this.ErrorMessageResult<ProjectDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProjectDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _projectService.GetProjects(pagination)) :
            this.ErrorMessageResult<PagedResponse<ProjectDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse<ProjectDTO>>> Add([FromBody] ProjectAddDTO project)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _projectService.AddProject(project, currentUser.Result)) :
            this.ErrorMessageResult<ProjectDTO>(currentUser.Error);    
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ProjectDTO project)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _projectService.UpdateProject(project, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _projectService.DeleteProject(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}