using Million.PropertyManagement.Domain.Entities;

namespace Million.PropertyManagement.Infrastructure.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(int id);
        Task AddAsync(Property entity);
        Task AddPropertyTracesAsync(PropertyTrace entity);
        Task AddImageAsync(PropertyImage entity);
        Task SaveAsync();
        Task UpdateAsync(Property entity);
        IQueryable<Property> ListAsync();
    }
}
