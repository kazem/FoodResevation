using Entities;

namespace Services
{
    public interface IJwtService
    {
        string GenerateAsync(User user);
    }
}