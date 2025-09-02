using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;
    public OwnersController(IOwnerService ownerService) => _ownerService = ownerService;

    [HttpGet("{idOwner:int}")]
    public async Task<IActionResult> GetById(int idOwner)
    {
        var owner = await _ownerService.GetById(idOwner);
        return owner == null ? NotFound() : Ok(owner);
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? name)
    {
        var query = _ownerService.GetAll(name);
        return Ok(query.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OwnerCreateDto dtoOwner)
    {
        Owner owner = await _ownerService.AddAsync(dtoOwner);
        return owner == null ? BadRequest() : CreatedAtAction(nameof(Create), owner);
    }
}
