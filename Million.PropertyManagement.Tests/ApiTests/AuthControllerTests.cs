using NUnit.Framework;
using Moq;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[TestFixture]
public class AuthControllerTests
{
    private AuthController _controller;
    private Mock<IAuthService> _authServiceMock;

    [SetUp]
    public void Setup()
    {
        _authServiceMock = new Mock<IAuthService>();
        _controller = new AuthController(_authServiceMock.Object);
    }

    [Test]
    public async Task Login_ReturnsOk_WhenTokenGenerated()
    {
        var dto = new LoginRequestDto("user", "pass");
        _authServiceMock.Setup(s => s.AuthenticateAsync(dto.Username, dto.Password)).ReturnsAsync("token");

        var result = await _controller.Login(dto);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task Login_ReturnsUnauthorized_WhenTokenNull()
    {
        var dto = new LoginRequestDto("user", "pass");
        _authServiceMock.Setup(s => s.AuthenticateAsync(dto.Username, dto.Password)).ReturnsAsync((string)null);

        var result = await _controller.Login(dto);

        Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
    }
}