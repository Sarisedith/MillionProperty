using NUnit.Framework;
using Moq;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

[TestFixture]
public class PropertyTracesControllerTests
{
    private PropertyTracesController _controller;
    private Mock<IPropertyTracesService> _serviceMock;

    [SetUp]
    public void Setup()
    {
        _serviceMock = new Mock<IPropertyTracesService>();
        _controller = new PropertyTracesController(_serviceMock.Object);
    }

    [Test]
    public async Task GetByProperty_ReturnsOk_WhenTracesExist()
    {
        var traces = new List<PropertyTrace> { new PropertyTrace() };
        _serviceMock.Setup(s => s.GetByProperty(1)).ReturnsAsync(traces);

        var result = await _controller.GetByProperty(1);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task GetByProperty_ReturnsNotFound_WhenNoTraces()
    {
        var traces = new List<PropertyTrace>();
        _serviceMock.Setup(s => s.GetByProperty(1)).ReturnsAsync(traces);

        var result = await _controller.GetByProperty(1);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Create_ReturnsOk_WhenCreated()
    {
        var dto = new PropertyTraceDto
        (
            PropertyId: 1,
            DateSale: DateTime.Now,
            Name: "Test",
            Value: 1000m,
            Tax: 100m
        );
        var trace = new PropertyTrace();
        _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(trace);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task Create_ReturnsNotFound_WhenNull()
    {
        var dto = new PropertyTraceDto
        (
            PropertyId: 1,
            DateSale: DateTime.Now,
            Name: "Test",
            Value: 1000m,
            Tax: 100m
        );
        _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync((PropertyTrace)null);

        var result = await _controller.Create(dto);

        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}