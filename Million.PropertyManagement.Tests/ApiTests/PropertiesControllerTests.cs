using NUnit.Framework;
using Moq;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[TestFixture]
public class PropertiesControllerTests
{
    private PropertiesController _controller;
    private Mock<IPropertyService> _serviceMock;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IPropertyService>();
        _controller = new PropertiesController(_serviceMock.Object);
    }

    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var dto = new PropertyReadDto(
            Id: 1,
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000,
            Images: new List<PropertyImageReadDto>()
        );
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task GetById_ReturnsNotFound_WhenNotFound()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((PropertyReadDto)null);

        var result = await _controller.GetById(1);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Create_ReturnsOk_WhenCreated()
    {
        var dto = new PropertyCreateDto
        (
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000,
            OwnerId: null
        );
        var readDto = new PropertyReadDto(
            Id: 1,
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000,
            Images: new List<PropertyImageReadDto>()
        );
        _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(readDto);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task Create_ReturnsBadRequest_WhenNull()
    {
        var dto = new PropertyCreateDto
        (
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000,
            OwnerId: null
        );
        _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync((PropertyReadDto)null);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task AddImage_ReturnsOk_WhenCreated()
    {
        var imageDto = new PropertyImageCreateDto
        (
            Url: "http://example.com/image.jpg",
            IsPrimary: true
        );
        var image = new PropertyImage();
        _serviceMock.Setup(s => s.AddImageAsync(1, imageDto)).ReturnsAsync(image);

        var result = await _controller.AddImage(1, imageDto);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task AddImage_ReturnsBadRequest_WhenNull()
    {
        var imageDto = new PropertyImageCreateDto
        (
            Url: "http://example.com/image.jpg",
            IsPrimary: true
        );
        _serviceMock.Setup(s => s.AddImageAsync(1, imageDto)).ReturnsAsync((PropertyImage)null);

        var result = await _controller.AddImage(1, imageDto);

        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Update_ReturnsNoContent_WhenTrue()
    {
        var dto = new PropertyUpdateDto
        (
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000
        );
        _serviceMock.Setup(s => s.UpdateAsync(1, dto)).ReturnsAsync(true);

        var result = await _controller.Update(1, dto);

        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task Update_ReturnsNotFound_WhenFalse()
    {
        var dto = new PropertyUpdateDto
        (
            Address: "123 Main St",
            City: "Sample City",
            Price: 100000m,
            YearBuilt: 2000
        );
        _serviceMock.Setup(s => s.UpdateAsync(1, dto)).ReturnsAsync(false);

        var result = await _controller.Update(1, dto);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task ChangePrice_ReturnsOk_WhenTraceCreated()
    {
        var dto = new PropertyPriceUpdateDto(1000);
        var trace = new PropertyTrace();
        _serviceMock.Setup(s => s.ChangePriceAsync(1, dto.NewPrice)).ReturnsAsync(trace);

        var result = await _controller.ChangePrice(1, dto);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task ChangePrice_ReturnsNotFound_WhenNull()
    {
        var dto = new PropertyPriceUpdateDto(1000);
        _serviceMock.Setup(s => s.ChangePriceAsync(1, dto.NewPrice)).ReturnsAsync((PropertyTrace)null);

        var result = await _controller.ChangePrice(1, dto);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}