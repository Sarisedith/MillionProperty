using Million.PropertyManagement.Domain.Entities;

namespace Million.PropertyManagement.Infrastructure.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner?> GetByIdAsync(int id);
        Task AddAsync(Owner owner);
        Task SaveAsync();
        IQueryable<Owner> GetAll();
    }
}
