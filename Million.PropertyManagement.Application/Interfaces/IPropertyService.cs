using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
namespace Million.PropertyManagement.Application.Interfaces;
public interface IPropertyService
{
    Task<PropertyReadDto> CreateAsync(PropertyCreateDto dto);
    Task<PropertyImage> AddImageAsync(int propertyId, PropertyImageCreateDto dto);
    Task<bool> UpdateAsync(int id, PropertyUpdateDto dto);
    Task<PropertyTrace> ChangePriceAsync(int id, decimal newPrice);
    Task<(IEnumerable<PropertyReadDto> items, int total)> ListAsync(PropertyListFilter filter);
    Task<PropertyReadDto?> GetByIdAsync(int id);
}
