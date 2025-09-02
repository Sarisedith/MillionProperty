using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using System.Diagnostics;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertyTracesController : ControllerBase
{
    private readonly IPropertyTracesService _ipropertyTracesService;
    public PropertyTracesController(IPropertyTracesService ipropertyTracesService) => _ipropertyTracesService = ipropertyTracesService;

    [HttpGet("by-property/{propertyId:int}")]
    public async Task<IActionResult> GetByProperty(int propertyId)
    {
        var traces = await _ipropertyTracesService.GetByProperty(propertyId);
        return traces.Count() == 0 ? NotFound() : Ok(traces);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PropertyTraceDto trace)
    {
        PropertyTrace propertyTrace = await _ipropertyTracesService.CreateAsync(trace);
        return propertyTrace == null ? NotFound() : Ok(propertyTrace);
    }
}
