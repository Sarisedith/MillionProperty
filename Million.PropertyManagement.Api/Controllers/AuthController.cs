using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
    {
        var token = await _auth.AuthenticateAsync(req.Username, req.Password);
        if (token == null) return Unauthorized(new { message = "Invalid credentials" });
        return Ok(new { access_token = token });
    }
}
