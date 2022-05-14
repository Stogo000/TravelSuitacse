using MediatR;
using System.IdentityModel.Tokens.Jwt;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Repositories.Users;

namespace TravelSuitcase.Application.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenResponseDTO>
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponseDTO>
    {
        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.RefreshToken(request.RefreshToken);
            return new TokenResponseDTO(new JwtSecurityTokenHandler().WriteToken(result.JwtSecurityToken),
                result.RefreshToken);
        }
    }
}