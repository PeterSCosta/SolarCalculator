using SolarCalculator.Models;

namespace SolarCalculator.Services
{
    public interface ITokenService
    {
        string GenerateToken(
            User user
        );
    }
}
