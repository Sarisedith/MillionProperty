using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Million.PropertyManagement.Domain.Entities;
using Million.PropertyManagement.Infrastructure.Interfaces;
using Million.PropertyManagement.Infrastructure.Persistence;

namespace Million.PropertyManagement.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;
        public AuthRepository(AppDbContext db, IConfiguration cfg) { _db = db; }
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            return user;
        }
    }
}
