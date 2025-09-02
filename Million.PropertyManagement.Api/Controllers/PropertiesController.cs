using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _iPropertyService;
    public PropertiesController(IPropertyService iPropertyService) => _iPropertyService = iPropertyService;

    [HttpGet("{propertyId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int propertyId)
        => (await _iPropertyService.GetByIdAsync(propertyId)) is { } dto ? Ok(dto) : NotFound();

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> List([FromQuery] PropertyListFilter filter)
    {
        var (items, total) = await _iPropertyService.ListAsync(filter);
        Response.Headers["X-Total-Count"] = total.ToString();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PropertyCreateDto dto)

    {
        PropertyReadDto propertyReadDto = await _iPropertyService.CreateAsync(dto);
        return propertyReadDto == null ? BadRequest() : Ok(propertyReadDto);
    }

    [HttpPost("{propertyId:int}/images")]
    public async Task<IActionResult> AddImage(int propertyId, PropertyImageCreateDto dto)
    {

        PropertyImage propertyImagenReadDto = await _iPropertyService.AddImageAsync(propertyId, dto);
        return propertyImagenReadDto == null ? BadRequest() : Ok(propertyImagenReadDto);
    }

    [HttpPut("{propertyId:int}")]
    public async Task<IActionResult> Update(int propertyId, PropertyUpdateDto dto)
        => await _iPropertyService.UpdateAsync(propertyId, dto) ? NoContent() : NotFound();

    [HttpPut("{propertyId:int}/price")]
    public async Task<IActionResult> ChangePrice(int propertyId, PropertyPriceUpdateDto dto)
    {
        PropertyTrace propertyTrace = await _iPropertyService.ChangePriceAsync(propertyId, dto.NewPrice);
        return propertyTrace == null ? NotFound() : Ok(propertyTrace); 
    }
        
}
