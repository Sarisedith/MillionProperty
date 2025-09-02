using Microsoft.EntityFrameworkCore;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Million.PropertyManagement.Infrastructure.Persistence;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class PropertyTracesRepository : IPropertyTracesRepository
    {
        private readonly AppDbContext _db;
        public PropertyTracesRepository(AppDbContext db) => _db = db;
        public Task AddAsync(PropertyTrace entity) => _db.PropertyTraces.AddAsync(entity).AsTask();
        public Task SaveAsync() => _db.SaveChangesAsync();
        public async Task<List<PropertyTrace>?> GetByPropertyAsync(int propertyId)
        {
            var propertyTrace = await _db.PropertyTraces.Where(u => u.PropertyId == propertyId).ToListAsync();
            if (propertyTrace == null) return null;
            return propertyTrace;
        }
    }
}
