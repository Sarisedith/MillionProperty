using NUnit.Framework;
using Moq;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class OwnersControllerTests
{
    private OwnersController _controller;
    private Mock<IOwnerService> _ownerServiceMock;

    [SetUp]
    public void Setup()
    {
        _ownerServiceMock = new Mock<IOwnerService>();
        _controller = new OwnersController(_ownerServiceMock.Object);
    }

    [Test]
    public async Task GetById_ReturnsOk_WhenOwnerExists()
    {
        var owner = new Owner { Id = 1, Name = "Test Owner" };
        _ownerServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(owner);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task GetById_ReturnsNotFound_WhenOwnerDoesNotExist()
    {
        _ownerServiceMock.Setup(s => s.GetById(1)).ReturnsAsync((Owner)null);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public void GetAll_ReturnsOwnersList()
    {
        var owners = new List<Owner> { new Owner { Id = 1, Name = "Test" } }.AsQueryable();
        _ownerServiceMock.Setup(s => s.GetAll(null)).Returns(owners);

        var result = _controller.GetAll(null) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<Owner>>(result.Value);
    }

    [Test]
    public async Task Create_ReturnsCreated_WhenOwnerIsCreated()
    {
        var dto = new OwnerCreateDto("New Owner", null, null, null);
        var owner = new Owner { Id = 2, Name = "New Owner" };
        _ownerServiceMock.Setup(s => s.AddAsync(dto)).ReturnsAsync(owner);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<CreatedAtActionResult>(result);
    }

    [Test]
    public async Task Create_ReturnsBadRequest_WhenOwnerIsNull()
    {
        var dto = new OwnerCreateDto("New Owner", null, null, null);
        _ownerServiceMock.Setup(s => s.AddAsync(dto)).ReturnsAsync((Owner)null);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<BadRequestResult>(result);
    }
}