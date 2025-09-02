using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Persistence;
using Million.PropertyManagement.Infrastructure.Repositories;
using NUnit.Framework;

[TestFixture]
public class PropertyRepositoryTests
{
    private AppDbContext _dbContext;
    private PropertyRepository _repository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new AppDbContext(options);
        _repository = new PropertyRepository(_dbContext);
    }

    [Test]
    public async Task AddAsync_ShouldAddProperty()
    {
        var property = new Property { Address = "Test", City = "TestCity", Price = 100000, YearBuilt = 2020, CreatedAt = DateTime.UtcNow };
        await _repository.AddAsync(property);
        await _repository.SaveAsync();

        var result = await _dbContext.Properties.FirstOrDefaultAsync(p => p.Address == "Test");
        Assert.IsNotNull(result);
        Assert.AreEqual("TestCity", result.City);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnPropertyWithImagesAndTraces()
    {
        var property = new Property { Address = "Test", City = "TestCity", Price = 100000, YearBuilt = 2020, CreatedAt = DateTime.UtcNow };
        var image = new PropertyImage { Url = "http://img", IsPrimary = true };
        var trace = new PropertyTrace { Name = "Sale", Value = 100000, Tax = 1000, DateSale = DateTime.UtcNow };
        property.Images = new List<PropertyImage> { image };
        property.Traces = new List<PropertyTrace> { trace };

        await _repository.AddAsync(property);
        await _repository.SaveAsync();

        var result = await _repository.GetByIdAsync(property.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Images.Count);
        Assert.AreEqual(1, result.Traces.Count);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateProperty()
    {
        var property = new Property { Address = "Old", City = "City", Price = 100000, YearBuilt = 2020, CreatedAt = DateTime.UtcNow };
        await _repository.AddAsync(property);
        await _repository.SaveAsync();

        property.Address = "New";
        await _repository.UpdateAsync(property);

        var updated = await _dbContext.Properties.FindAsync(property.Id);
        Assert.AreEqual("New", updated.Address);
    }

    [Test]
    public async Task AddImageAsync_ShouldAddImage()
    {
        var property = new Property { Address = "Test", City = "TestCity", Price = 100000, YearBuilt = 2020, CreatedAt = DateTime.UtcNow };
        await _repository.AddAsync(property);
        await _repository.SaveAsync();

        var image = new PropertyImage { PropertyId = property.Id, Url = "http://img", IsPrimary = true };
        await _repository.AddImageAsync(image);
        await _repository.SaveAsync();

        var result = await _dbContext.PropertyImages.FirstOrDefaultAsync(i => i.PropertyId == property.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("http://img", result.Url);
    }

    [Test]
    public async Task AddPropertyTracesAsync_ShouldAddTrace()
    {
        var property = new Property { Address = "Test", City = "TestCity", Price = 100000, YearBuilt = 2020, CreatedAt = DateTime.UtcNow };
        await _repository.AddAsync(property);
        await _repository.SaveAsync();

        var trace = new PropertyTrace { PropertyId = property.Id, Name = "Sale", Value = 100000, Tax = 1000, DateSale = DateTime.UtcNow };
        await _repository.AddPropertyTracesAsync(trace);
        await _repository.SaveAsync();

        var result = await _dbContext.PropertyTraces.FirstOrDefaultAsync(t => t.PropertyId == property.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("Sale", result.Name);
    }

    [Test]
    public async Task ListAsync_ShouldReturnNonDeletedProperties()
    {
        var property1 = new Property { Address = "A", City = "C", Price = 1, YearBuilt = 2000, CreatedAt = DateTime.UtcNow, IsDeleted = false };
        var property2 = new Property { Address = "B", City = "D", Price = 2, YearBuilt = 2001, CreatedAt = DateTime.UtcNow, IsDeleted = true };
        await _repository.AddAsync(property1);
        await _repository.AddAsync(property2);
        await _repository.SaveAsync();

        var list = _repository.ListAsync().ToList();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("A", list[0].Address);
    }
}
