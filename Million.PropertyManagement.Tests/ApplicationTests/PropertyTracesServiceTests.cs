using NUnit.Framework;
using NUnit.Framework;
using Moq;
using AutoMapper;
using Million.PropertyManagement.Application.Services;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestFixture]
public class PropertyTracesServiceTests
{
    private Mock<IMapper> _mapperMock;
    private Mock<IPropertyTracesRepository> _repositoryMock;
    private PropertyTracesService _service;

    [SetUp]
    public void SetUp()
    {
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IPropertyTracesRepository>();
        _service = new PropertyTracesService(_mapperMock.Object, _repositoryMock.Object);
    }

    [Test]
    public async Task GetByProperty_ReturnsTraces_WhenTracesExist()
    {
        // Arrange
        int propertyId = 1;
        var traces = new List<PropertyTrace>
            {
                new PropertyTrace { Id = 1, PropertyId = propertyId, Name = "Trace1", Value = 100, Tax = 10 },
                new PropertyTrace { Id = 2, PropertyId = propertyId, Name = "Trace2", Value = 200, Tax = 20 }
            };
        _repositoryMock.Setup(r => r.GetByPropertyAsync(propertyId)).ReturnsAsync(traces);

        // Act
        var result = await _service.GetByProperty(propertyId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(propertyId, result[0].PropertyId);
    }

    [Test]
    public async Task GetByProperty_ReturnsNull_WhenNoTracesExist()
    {
        // Arrange
        int propertyId = 2;
        _repositoryMock.Setup(r => r.GetByPropertyAsync(propertyId)).ReturnsAsync((List<PropertyTrace>?)null);

        // Act
        var result = await _service.GetByProperty(propertyId);

        // Assert
        Assert.IsNull(result);
    }
}