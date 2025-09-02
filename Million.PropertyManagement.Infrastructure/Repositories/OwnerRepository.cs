using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Million.PropertyManagement.Infrastructure.Persistence;

namespace Million.PropertyManagement.Infrastructure.Repositories;
public class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _db;
    public OwnerRepository(AppDbContext db) => _db = db;

    public Task<Owner?> GetByIdAsync(int id)
        => (_db.Owners.AsNoTracking()
            .Include(o => o.Properties)
                .ThenInclude(p => p.Images)
             .Include(o => o.Properties)
                .ThenInclude(p => p.Traces)
        ).FirstOrDefaultAsync(o => o.Id == id);

    public Task AddAsync(Owner owner) => _db.Owners.AddAsync(owner).AsTask();
    public Task SaveAsync() => _db.SaveChangesAsync();
    public IQueryable<Owner> GetAll() => _db.Owners.AsNoTracking().Include(o => o.Properties)
                .ThenInclude(p => p.Images)
             .Include(o => o.Properties)
                .ThenInclude(p => p.Traces);
}
