using Million.PropertyManagement.Application.DTOs;
using Million.PropertyManagement.Domain.Entities;

namespace Million.PropertyManagement.Application.Interfaces
{
    public interface IOwnerService
    {
        Task<Owner?> GetById(int id);
        IQueryable<Owner> GetAll(string? name);
        Task<Owner> AddAsync(OwnerCreateDto owner);
    }
}
