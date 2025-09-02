using NUnit.Framework;
using Million.PropertyManagement.Infrastructure.Persistence;
using Million.PropertyManagement.Infrastructure.Repositories;
using Million.PropertyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[TestFixture]
public class OwnerRepositoryTests
{
    private AppDbContext _context;
    private OwnerRepository _repo;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _context = new AppDbContext(options);
        _repo = new OwnerRepository(_context);
    }

    [Test]
    public async Task AddAsync_AddsOwner()
    {
        var dto = new Owner { Name = "Test Owner" };
        await _repo.AddAsync(dto);
        await _repo.SaveAsync();

        var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Name == "Test Owner");
        Assert.IsNotNull(owner);
    }
}