using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Models.Dtos.Request;
using SystemMonitor.Server.Services;

namespace SystemMonitor.Server.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = await authService.LoginUser(loginRequest);

        if (loginResponse == null)
            return Unauthorized();

        return Ok(loginResponse);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var registerResponse = await authService.RegisterUser(registerRequest);

        if (registerResponse == null)
            return BadRequest();

        return Ok(registerResponse);
    }
}