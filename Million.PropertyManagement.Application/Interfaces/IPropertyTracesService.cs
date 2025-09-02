using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;
namespace Million.PropertyManagement.Application.Interfaces
{
    public interface IPropertyTracesService
    {
        Task<List<PropertyTrace>>? GetByProperty(int propertyId);
        Task<PropertyTrace> CreateAsync(PropertyTraceDto trace);
    }
}
