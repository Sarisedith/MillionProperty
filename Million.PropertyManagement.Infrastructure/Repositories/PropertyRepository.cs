using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Million.PropertyManagement.Infrastructure.Persistence;

namespace Million.PropertyManagement.Infrastructure.Repositories;
public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _db;
    public PropertyRepository(AppDbContext db) => _db = db;

    public Task<Property?> GetByIdAsync(int propertyId)
        => (_db.Properties.AsNoTracking())
           .Include(p => p.Images).Include(p => p.Traces)
           .FirstOrDefaultAsync(p => p.Id == propertyId);

    public Task AddAsync(Property entity) => _db.Properties.AddAsync(entity).AsTask();
    public Task AddImageAsync(PropertyImage entity) => _db.PropertyImages.AddAsync(entity).AsTask();
    public Task AddPropertyTracesAsync(PropertyTrace entity) => _db.PropertyTraces.AddAsync(entity).AsTask();
    public Task SaveAsync() => _db.SaveChangesAsync();
    public async Task UpdateAsync(Property entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
    }
    public IQueryable<Property> ListAsync() => _db.Properties.AsNoTracking().Include(x => x.Images).Include(x => x.Traces).Where(p => !p.IsDeleted);
}
