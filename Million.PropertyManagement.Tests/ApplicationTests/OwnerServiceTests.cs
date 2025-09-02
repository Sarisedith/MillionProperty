using NUnit.Framework;
using Moq;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Million.PropertyManagement.Infrastructure.Interfaces;
using AutoMapper;
[TestFixture]
public class OwnerServiceTests
{
    private OwnerService _service;
    private Mock<IOwnerRepository> _repoMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IOwnerRepository>();
        _mapperMock = new Mock<IMapper>(); 
        _service = new OwnerService(_repoMock.Object, _mapperMock.Object); 
    }

    [Test]
    public async Task GetById_ReturnsOwner()
    {
        var owner = new Owner { Id = 1, Name = "Test" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(owner);

        var result = await _service.GetById(1);

        Assert.AreEqual(owner, result);
    }

    [Test]
    public void GetAll_ReturnsQueryableOwners()
    {
        var owners = new List<Owner> { new Owner { Id = 1, Name = "Test" } }.AsQueryable();
        _repoMock.Setup(r => r.GetAll()).Returns(owners);

        var result = _service.GetAll(null);

        Assert.AreEqual(owners, result);
    }

    [Test]
    public async Task AddAsync_ReturnsOwner()
    {
        var dto = new OwnerCreateDto("New Owner", null, null, null);
        var owner = new Owner { Id = 2, Name = "New Owner" };
        _mapperMock.Setup(m => m.Map<Owner>(dto)).Returns(owner); 
        _repoMock.Setup(r => r.AddAsync(owner)).Returns(Task.CompletedTask);

        var result = await _service.AddAsync(dto);

        Assert.AreEqual(owner, result);
    }
}