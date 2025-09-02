using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Million.PropertyManagement.Domain.Entities;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IAuthRepository> _authRepositoryMock;
    private Mock<IConfiguration> _configurationMock;
    private AuthService _authService;

    [SetUp]
    public void Setup()
    {
        _authRepositoryMock = new Mock<IAuthRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("supersecretkey1234567890");
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
        _configurationMock.Setup(c => c["Jwt:ExpireHours"]).Returns("4");
        _authService = new AuthService(_authRepositoryMock.Object, _configurationMock.Object);
    }

    [Test]
    public async Task AuthenticateAsync_ReturnsToken_WhenCredentialsAreValid()
    {
        var password = "password";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            PasswordHash = passwordHash,
            Role = "Admin"
        };

        _authRepositoryMock.Setup(r => r.AuthenticateAsync("testuser", password))
            .ReturnsAsync(user);

        var token = await _authService.AuthenticateAsync("testuser", password);

        Assert.IsNotNull(token);
        Assert.IsInstanceOf<string>(token);
    }

    [Test]
    public async Task AuthenticateAsync_ReturnsNull_WhenUserNotFound()
    {
        _authRepositoryMock.Setup(r => r.AuthenticateAsync("nouser", "password"))
            .ReturnsAsync((User?)null);

        var token = await _authService.AuthenticateAsync("nouser", "password");

        Assert.IsNull(token);
    }

    [Test]
    public async Task AuthenticateAsync_ReturnsNull_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("otherpassword"),
            Role = "Admin"
        };

        _authRepositoryMock.Setup(r => r.AuthenticateAsync("testuser", "password"))
            .ReturnsAsync(user);

        var token = await _authService.AuthenticateAsync("testuser", "password");

        Assert.IsNull(token);
    }
}
