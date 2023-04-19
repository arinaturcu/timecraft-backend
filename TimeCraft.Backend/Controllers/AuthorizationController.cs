using Microsoft.AspNetCore.Mvc;
using TimeCraft.Core.DataTransferObjects;
using TimeCraft.Core.Responses;
using TimeCraft.Infrastructure.Authorization;
using TimeCraft.Infrastructure.Extensions;
using TimeCraft.Infrastructure.Services.Interfaces;

namespace TimeCraft.Backend.Controllers;

/// <summary>
/// This is a controller to respond to authentication requests.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class AuthorizationController : ControllerBase // The controller must inherit ControllerBase or its derivations.
{
    private readonly IUserService _userService;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public AuthorizationController(IUserService userService) => _userService = userService;

    /// <summary>
    /// This method will respond to login requests.
    /// </summary>
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/Authorization/Login having a JSON body deserialized as a LoginDTO.
    public async Task<ActionResult<RequestResponse<LoginResponseDTO>>> Login([FromBody] LoginDTO login) // The FromBody attribute indicates that the parameter is deserialized from the JSON body.
    {
        return this.FromServiceResponse(await _userService.Login(login with { Password = PasswordUtils.HashPassword(login.Password)})); // The "with" keyword works only with records and it creates another object instance with the updated properties. 
    }
}
