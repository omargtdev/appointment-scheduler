using System.Security.Authentication;
using AppointmentScheduler.API.DTO.Authentication;
using AppointmentScheduler.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.API;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{

    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        try
        {
            var authenticateResponse = await Task.Run(() => _userService.Authenticate(loginRequestDto.Email, loginRequestDto.Password));
            return Ok();
        }
        catch (AuthenticationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequestDto loginRequestDto)
    {
        return Ok("holi");
    }

}
