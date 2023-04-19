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
public class ClientController : AuthorizedController
{
    private readonly IClientService _clientService;
    
    public ClientController(IUserService userService, IClientService clientService) : base(userService)
    {
        _clientService = clientService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ClientDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse( await _clientService.GetClient(id)) :
            this.ErrorMessageResult<ClientDTO>(currentUser.Error);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ClientDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await _clientService.GetClients(pagination)) :
            this.ErrorMessageResult<PagedResponse<ClientDTO>>(currentUser.Error);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse<ClientDTO>>> Add([FromBody] ClientAddDTO client)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await _clientService.AddClient(client, currentUser.Result)) :
            this.ErrorMessageResult<ClientDTO>(currentUser.Error);    
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ClientDTO client)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await _clientService.UpdateClient(client, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id) // The FromRoute attribute will bind the id from the route to this parameter.
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await _clientService.DeleteClient(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
