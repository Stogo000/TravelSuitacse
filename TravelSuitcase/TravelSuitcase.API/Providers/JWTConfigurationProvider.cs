using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Providers;

namespace TravelSuitcase.API.Providers
{
    public class JWTConfigurationProvider : IJWTConfigurationProvider
    {
        private IConfiguration _configuration;

        public JWTConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JWTOptionsDTO GetJWTOptions()
        {
            return new JWTOptionsDTO(
                _configuration["issuer"],
                _configuration["audience"],
                _configuration["SecretKey"]);
        }
    }
}