using Million.PropertyManagement.Domain.Entities;
namespace Million.PropertyManagement.Infrastructure.Interfaces
{
    public interface IPropertyTracesRepository
    {
        Task AddAsync(PropertyTrace entity);
        Task SaveAsync();
        Task<List<PropertyTrace>?> GetByPropertyAsync(int propertyId);
    }
}
