using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Persistence;
using Million.PropertyManagement.Infrastructure.Repositories;
using NUnit.Framework;

[TestFixture]
public class AuthRepositoryTests
{
    private AppDbContext _dbContext;
    private AuthRepository _authRepository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new AppDbContext(options);

        // Seed a user
        _dbContext.Users.Add(new User
        {
            Id = 1,
            Username = "testuser",
            PasswordHash = "testpasswordhash",
            Role = "Admin"
        });
        _dbContext.SaveChanges();

        var config = new ConfigurationBuilder().Build();
        _authRepository = new AuthRepository(_dbContext, config);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task AuthenticateAsync_UserExists_ReturnsUser()
    {
        var user = await _authRepository.AuthenticateAsync("testuser", "anyPassword");
        Assert.IsNotNull(user);
        Assert.AreEqual("testuser", user.Username);
    }

    [Test]
    public async Task AuthenticateAsync_UserDoesNotExist_ReturnsNull()
    {
        var user = await _authRepository.AuthenticateAsync("nonexistent", "anyPassword");
        Assert.IsNull(user);
    }
}
