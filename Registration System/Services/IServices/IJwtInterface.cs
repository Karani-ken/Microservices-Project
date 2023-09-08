using Registration_System.Models;

namespace Registration_System.Services.IServices
{
    public interface IJwtInterface
    {
        string GenerateToken(User user);
    }
}
