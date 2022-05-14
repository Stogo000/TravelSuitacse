using System.IdentityModel.Tokens.Jwt;
using MediatR;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Repositories.Users;

namespace TravelSuitcase.Application.Commands.Auth.LoginUser
{
    public class LoginUserCommand : IRequest<TokenResponseDTO>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDTO>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            RefreshTokenDTO result = await _userRepository.LogInAsync(new LoginDTO(request.Login, request.Password),
                cancellationToken);
            return new TokenResponseDTO(new JwtSecurityTokenHandler().WriteToken(result.JwtSecurityToken),
                result.RefreshToken);
        }
    }
}