using TravelSuitcase.Domain.Common.DTOs;

namespace TravelSuitcase.Domain.Common.Providers
{
    public interface IJWTConfigurationProvider
    {
        JWTOptionsDTO GetJWTOptions();
    }
}