using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Application.Interfaces;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;

namespace Million.PropertyManagement.Application.Services;
public class PropertyService : IPropertyService
{
    private readonly IMapper _mapper;
    private readonly IPropertyRepository _propertyRepository;
    public PropertyService(IMapper mapper, IPropertyRepository propertyRepository) { _mapper = mapper; _propertyRepository = propertyRepository; }

    public async Task<PropertyReadDto> CreateAsync(PropertyCreateDto dto)
    {
        var entity = _mapper.Map<Property>(dto);
        await _propertyRepository.AddAsync(entity);
        await _propertyRepository.SaveAsync();
        return _mapper.Map<PropertyReadDto>(entity);
    }

    public async Task<PropertyImage> AddImageAsync(int propertyId, PropertyImageCreateDto dto)
    {
        var prop = await _propertyRepository.GetByIdAsync(propertyId);
        if (prop == null) return new PropertyImage();
        var entity = _mapper.Map<PropertyImage>(dto);
        entity.PropertyId = propertyId;
        await _propertyRepository.AddImageAsync(entity);
        await _propertyRepository.SaveAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(int propertyId, PropertyUpdateDto dto)
    {
        var prop = await _propertyRepository.GetByIdAsync(propertyId);
        if (prop == null) return false;
        prop.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(dto, prop);
        await _propertyRepository.UpdateAsync(prop); return true;
    }

    public async Task<PropertyTrace> ChangePriceAsync(int propertyId, decimal newPrice)
    {
        var prop = await _propertyRepository.GetByIdAsync(propertyId);
        if (prop == null) return new PropertyTrace();
        prop.Price = newPrice; prop.UpdatedAt = DateTime.UtcNow;
        var entity = new PropertyTrace { PropertyId = prop.Id, DateSale = DateTime.UtcNow, Name = "Price change", Value = newPrice, Tax = 0 };
        await _propertyRepository.AddPropertyTracesAsync(entity);
        await _propertyRepository.SaveAsync(); 
        return entity;
    }

    public async Task<(IEnumerable<PropertyReadDto> items, int total)> ListAsync(PropertyListFilter filter)
    {

        var query = _propertyRepository.ListAsync();
        if (!string.IsNullOrWhiteSpace(filter.City)) query = query.Where(p => p.City == filter.City);
        if (filter.MinPrice.HasValue) query = query.Where(p => p.Price >= filter.MinPrice.Value);
        if (filter.MaxPrice.HasValue) query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        var total = await query.CountAsync();
        var items = await query.OrderBy(p => p.City).ThenBy(p => p.Price)
            .Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize)
            .ProjectTo<PropertyReadDto>(_mapper.ConfigurationProvider).ToListAsync();
        return (items, total);
    }

    public async Task<PropertyReadDto?> GetByIdAsync(int propertyId)
    {
        var entity = await _propertyRepository.GetByIdAsync(propertyId);
        return entity is null ? null : _mapper.Map<PropertyReadDto>(entity);
    }
}
