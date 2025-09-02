using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using AutoMapper;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using System;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private IMapper _mapper;
    private PropertyService _service;

    [SetUp]
    public void Setup()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PropertyCreateDto, Property>();
            cfg.CreateMap<Property, PropertyReadDto>();
            cfg.CreateMap<PropertyImageCreateDto, PropertyImage>();
            cfg.CreateMap<PropertyUpdateDto, Property>();
        });
        _mapper = config.CreateMapper();

        _service = new PropertyService(_mapper, _propertyRepositoryMock.Object);
    }

    [Test]
    public async Task ChangePriceAsync_PropertyExists_UpdatesPriceAndCreatesTrace()
    {
        // Arrange
        var propertyId = 1;
        var newPrice = 500000m;
        var property = new Property { Id = propertyId, Price = 400000m, City = "TestCity" };
        _propertyRepositoryMock.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(property);
        _propertyRepositoryMock.Setup(r => r.AddPropertyTracesAsync(It.IsAny<PropertyTrace>())).Returns(Task.CompletedTask);
        _propertyRepositoryMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.ChangePriceAsync(propertyId, newPrice);

        // Assert
        Assert.AreEqual(propertyId, result.PropertyId);
        Assert.AreEqual("Price change", result.Name);
        Assert.AreEqual(newPrice, result.Value);
        Assert.AreEqual(0, result.Tax);
        _propertyRepositoryMock.Verify(r => r.AddPropertyTracesAsync(It.IsAny<PropertyTrace>()), Times.Once);
        _propertyRepositoryMock.Verify(r => r.SaveAsync(), Times.Once);
        Assert.AreEqual(newPrice, property.Price);
    }

    [Test]
    public async Task ChangePriceAsync_PropertyDoesNotExist_ReturnsEmptyTrace()
    {
        // Arrange
        var propertyId = 99;
        _propertyRepositoryMock.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync((Property?)null);

        // Act
        var result = await _service.ChangePriceAsync(propertyId, 123m);

        // Assert
        Assert.AreEqual(0, result.PropertyId);
        Assert.AreEqual(0, result.Value);
        Assert.AreEqual(0, result.Tax);
        _propertyRepositoryMock.Verify(r => r.AddPropertyTracesAsync(It.IsAny<PropertyTrace>()), Times.Never);
        _propertyRepositoryMock.Verify(r => r.SaveAsync(), Times.Never);
    }
}