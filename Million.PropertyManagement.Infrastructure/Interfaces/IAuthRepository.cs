using Million.PropertyManagement.Domain.Entities;
namespace Million.PropertyManagement.Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
